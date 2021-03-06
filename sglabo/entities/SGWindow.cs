﻿using System;
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
using memory4cs;
using System.Globalization;

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
        public int currentHP = 0;
        public Memory memory;

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

            this.memory = new Memory(proc.Id);

            if(IsField())
            {
                pcName = CapturePCNameFromStatus();
                pcCode = DetectColor(pcName, false).brown;
            }
        }

        public int ReadHP()
        {
            return memory.ReadInt(SGAddress.STATUS_CURRENT_HP, 2);
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
            return DetectColor(CaptureRectangle(new Rectangle(65, 477, 200, 20))).red > 100;
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
