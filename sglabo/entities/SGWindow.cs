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

        Process proc;
        public IntPtr hWnd;
        public ScreenPosition sPos;

        public Bitmap pcName;
        public int pcCode;
        public Job job;
        public JobAI ai;
        public int ap;
        public bool auto = true;

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
            GraphicUtils.Activate(this.hWnd);
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

        public void MoveMouseOnLocalTo(int x, int y)
        {
            GraphicUtils.Activate(Win32API.GetDesktopWindow());
            input.Mouse.MoveMouseTo((sPos.x + x) * 65535 / 1920, (sPos.y + y) * 65535 / 1080);
            Activate();
        }

        public void LeftClick()
        {
            Activate();

            input.Mouse
                .LeftButtonDown().Sleep(globalSleep)
                .LeftButtonUp().Sleep(globalSleep);
        }

        public void LeftClick(int x, int y)
        {
            MoveMouseOnLocalTo(x, y);

            input.Mouse
                .LeftButtonDown().Sleep(globalSleep)
                .LeftButtonUp().Sleep(globalSleep);
        }

        public void LeftDoubleClick(int x, int y)
        {
            MoveMouseOnLocalTo(x, y);

            input.Mouse
                .LeftButtonDown().Sleep(50)
                .LeftButtonUp()
                .LeftButtonDown().Sleep(50)
                .LeftButtonUp();
        }

        public void RightClick()
        {
            Activate();

            input.Mouse
                .RightButtonDown().Sleep(globalSleep)
                .RightButtonUp().Sleep(globalSleep);
        }

        public void RightClick(int x, int y)
        {
            MoveMouseOnLocalTo(x, y);

            input.Mouse
                .RightButtonDown().Sleep(globalSleep)
                .RightButtonUp().Sleep(globalSleep);
        }
    }
}
