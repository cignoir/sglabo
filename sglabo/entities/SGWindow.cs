using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using sglabo.entities;
using WindowsInput;
using WindowsInput.Native;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace sglabo
{
    class SGWindow
    {
        Process proc;
        IntPtr hWnd;
        ScreenPosition sPos;
        PC pc;
        BattleField battleField;

        bool isMain = true;

        long x;
        long y;
        long width;
        long height;

        InputSimulator input = new InputSimulator();

        int windowCount;

        public SGWindow(Process proc)
        {
            this.proc = proc;
            this.hWnd = proc.MainWindowHandle;
            SetScreenPosition();

            battleField = new BattleField(Properties.Resources.ルデンヌA);
        }

        public void Activate()
        {
            if(hWnd == IntPtr.Zero)
            {
                return;
            }

            if(Win32API.IsIconic(hWnd))
            {
                //SendMessage(hWnd, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
                //ShowWindowAsync(hWnd, SW_RESTORE);
                //SetFocus(hWnd);
                Win32API.SwitchToThisWindow(hWnd, true);
            }

            IntPtr forehWnd = Win32API.GetForegroundWindow();
            if(forehWnd == hWnd)
            {
                return;
            }
            uint foreThread = Win32API.GetWindowThreadProcessId(forehWnd, IntPtr.Zero);
            uint thisThread = Win32API.GetCurrentThreadId();
            uint timeout = 200000;
            if(foreThread != thisThread)
            {
                Win32API.SystemParametersInfoGet(Win32API.SPI_GETFOREGROUNDLOCKTIMEOUT, 0, ref timeout, 0);
                Win32API.SystemParametersInfoSet(Win32API.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, 0, 0);

                Win32API.AttachThreadInput(thisThread, foreThread, true);
            }

            Win32API.SetForegroundWindow(hWnd);
            Win32API.SetWindowPos(hWnd, Win32API.HWND_TOP, 0, 0, 0, 0,
                Win32API.SWP_NOMOVE | Win32API.SWP_NOSIZE | Win32API.SWP_SHOWWINDOW | Win32API.SWP_ASYNCWINDOWPOS);
            Win32API.BringWindowToTop(hWnd);
            //ShowWindowAsync(hWnd, SW_SHOW);
            Win32API.SetFocus(hWnd);
            Win32API.SwitchToThisWindow(hWnd, true);

            if(foreThread != thisThread)
            {
                Win32API.SystemParametersInfoSet(Win32API.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, timeout, 0);
                Win32API.AttachThreadInput(thisThread, foreThread, false);
            }
        }

        public Bitmap Capture()
        {
            if(!IsActive()) return null;

            input.Keyboard.KeyDown(VirtualKeyCode.END).Sleep(100);
            Bitmap bmp = GraphicUtils.CaptureActiveWindow();
            input.Keyboard.KeyUp(VirtualKeyCode.END).Sleep(100);
            return bmp;
        }

        public bool IsActive()
        {
            return Win32API.GetForegroundWindow() == hWnd;
        }

        private void SetScreenPosition()
        {
            sglabo.Win32API.RECT rect;
            Win32API.GetWindowRect(hWnd, out rect);
            this.sPos = new ScreenPosition(rect.left, rect.top);
            this.x = rect.left;
            this.y = rect.top;
            this.width = Math.Abs(rect.left - rect.right);
            this.height = Math.Abs(rect.top - rect.bottom);
        }

        public void OpenItemWindow()
        {
            if(!IsActive()) return;

            input.Keyboard
                .KeyDown(VirtualKeyCode.LCONTROL)
                .KeyDown(VirtualKeyCode.VK_I)
                .Sleep(100)
                .KeyUp(VirtualKeyCode.VK_I)
                .KeyUp(VirtualKeyCode.LCONTROL)
                .Sleep(100);
            windowCount++;
        }

        public void CloseItemWindow()
        {
            if(!IsActive()) return;

            input.Keyboard
                .KeyPress(VirtualKeyCode.ESCAPE)
                .Sleep(100);
            windowCount--;
        }

        public void OpenStatusWindow()
        {
            if(!IsActive()) return;

            input.Keyboard
                .KeyDown(VirtualKeyCode.LCONTROL)
                .KeyDown(VirtualKeyCode.VK_S)
                .Sleep(100)
                .KeyUp(VirtualKeyCode.VK_S)
                .KeyUp(VirtualKeyCode.LCONTROL)
                .Sleep(100);
            windowCount += 2;
        }

        public void CloseStatusWindow()
        {
            if(!IsActive()) return;

            input.Keyboard
                .KeyPress(VirtualKeyCode.ESCAPE)
                .Sleep(100)
                .KeyPress(VirtualKeyCode.ESCAPE)
                .Sleep(100);
            windowCount -= 2;
        }

        public void CloseAllWindows()
        {
            if(!IsActive()) return;

            for(int i = 0; i < windowCount; i++)
            {
                input.Keyboard.KeyPress(VirtualKeyCode.ESCAPE).Sleep(100);
            }
            windowCount = 0;
        }

        public void SendChat(string message)
        {
            if(!IsActive()) return;

            input.Keyboard
                .TextEntry(message)
                .KeyPress(VirtualKeyCode.RETURN)
                .Sleep(100);
        }

        public void HideChatLog()
        {
            if(!IsActive()) return;

            input.Keyboard
                .KeyDown(VirtualKeyCode.END)
                .Sleep(100);
        }

        public Bitmap CapturePCNameFromStatus()
        {
            if(!IsActive()) return null;

            CloseStatusWindow();
            OpenStatusWindow();
            var bmp = Capture();
            CloseStatusWindow();

            Rectangle rect = new Rectangle(543, 65, 180, 16);
            return bmp.Clone(rect, PixelFormat.Format32bppArgb);
        }

        public Bitmap CaptureBattleStatus()
        {
            if(!IsActive()) return null;

            var bmp = Capture();
            Rectangle rect = new Rectangle(0, 28, 67, 86);
            return bmp.Clone(rect, PixelFormat.Format32bppArgb);
        }

        public Bitmap CaptureFieldStatus()
        {
            if(!IsActive()) return null;

            var bmp = Capture();
            Rectangle rect = new Rectangle(2, 20, 28, 50);
            return bmp.Clone(rect, PixelFormat.Format32bppArgb);
        }

        public SGColor DetectColor(Bitmap bmp)
        {
            BitmapData bmpdata = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb
            );

            byte[] ba = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(bmpdata.Scan0, ba, 0, ba.Length);

            int whiteCount = 0;
            int yellowCount = 0;
            int brownCount = 0;
            int pinkCount = 0;
            int greenCount = 0;

            int pixsize = bmp.Width * bmp.Height * 4;
            for(int i = 0; i < pixsize; i += 4)
            {
                var b = ba[i + 0];
                var g = ba[i + 1];
                var r = ba[i + 2];
                var a = ba[i + 3];

                if(r == 255 && g == 255 && b == 255) whiteCount++;
                if(r == 255 && g == 255 && b == 0) yellowCount++;
                if(r == 102 && g == 34 && b == 0) brownCount++;
                if(r == 255 && g == 120 && b == 255) pinkCount++;
                if(r == 102 && g == 221 && b == 204) greenCount++;
            }
            Marshal.Copy(ba, 0, bmpdata.Scan0, ba.Length);
            bmp.UnlockBits(bmpdata);

            return new SGColor(whiteCount, yellowCount, brownCount, pinkCount, greenCount);
        }

        public bool IsBattle()
        {
            return DetectColor(CaptureBattleStatus()).pink > 0;
        }

        public bool IsField()
        {
            return DetectColor(CaptureFieldStatus()).green > 0;
        }
    }
}
