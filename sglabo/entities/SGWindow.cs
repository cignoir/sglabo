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
using System.Windows.Forms;
using sglabo.AI;

namespace sglabo
{
    partial class SGWindow
    {
        public static List<SGWindow> sgList = new List<SGWindow>();
        public static BattleField battleField;

        Process proc;
        public IntPtr hWnd;
        public ScreenPosition sPos;

        public Bitmap pcName;
        public int pcCode;
        public Job job;
        public JobAI ai;
        public int ap;
        public bool auto = true;
        public GridPosition gPos;

        long x;
        long y;
        long width;
        long height;
        public bool IsCenter = false;

        public InputSimulator input = new InputSimulator();

        int windowCount;

        public SGWindow(Process proc, bool isCenter)
        {
            this.proc = proc;
            this.hWnd = proc.MainWindowHandle;
            SetScreenPosition();
            this.IsCenter = isCenter;

            if(IsField())
            {
                pcName = CapturePCNameFromStatus();
                pcCode = DetectColor(pcName, false).brown;
            }
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

            Thread.Sleep(1000);
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

        public bool IsWaitingForBattleInput()
        {
            return DetectColor(CaptureBattleStatus()).pink > 10;
        }

        public bool IsWaitingForLot()
        {
            return DetectColor(CaptureRectangle(new Rectangle(0, 300, 800, 300))).red > 1000;
        }

        public bool IsField()
        {
            return DetectColor(CaptureFieldStatus()).green > 10;
        }

        public static SGWindow MainPC()
        {
            var centerList = sgList.Where(x => x.IsCenter);
            return centerList.Count() > 0 ? centerList.First() : sgList.First();
        }

        public static PTSize GetPTSize(){
            PTSize size = PTSize.SMALL;

            if(sgList.Count == 3 || sgList.Count == 4) size = PTSize.MEDIUM;
            else if(sgList.Count == 5) size = PTSize.LARGE;

            return size;
        }
    }
}
