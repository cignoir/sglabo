using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;
using WindowsInput.Native;

namespace sglabo
{
    partial class SGWindow
    {
        public int globalSleep = 100;

        public void OpenItemWindow()
        {
            Activate();

            input.Keyboard
                .KeyDown(VirtualKeyCode.LCONTROL)
                .KeyDown(VirtualKeyCode.VK_I)
                .Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.VK_I)
                .KeyUp(VirtualKeyCode.LCONTROL)
                .Sleep(globalSleep);
            windowCount++;
        }

        public bool OrganizeItems()
        {
            OpenItemWindow();
            LeftClick(543, 534);
            CloseItemWindow();
            return true;
        }

        public bool UseItem(int no = 1)
        {
            OpenItemWindow();
            switch(no)
            {
                case 1:
                    LeftDoubleClick(539, 270);
                    break;
                case 2:
                    LeftDoubleClick(608, 270);
                    break;
                default:
                    break;
            }
            CloseItemWindow();
            return true;
        }

        public void CloseItemWindow()
        {
            OpenItemWindow();
            windowCount--;
        }

        public void OpenStatusWindow()
        {
            Activate();

            input.Keyboard
                .KeyDown(VirtualKeyCode.LCONTROL)
                .KeyDown(VirtualKeyCode.VK_S)
                .Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.VK_S)
                .KeyUp(VirtualKeyCode.LCONTROL)
                .Sleep(globalSleep);
            windowCount += 2;
        }

        public void CloseStatusWindow()
        {
            Activate();

            input.Keyboard
                .KeyPress(VirtualKeyCode.ESCAPE)
                .Sleep(globalSleep)
                .KeyPress(VirtualKeyCode.ESCAPE)
                .Sleep(globalSleep);
            windowCount -= 2;
        }

        public void CloseAllWindows()
        {
            Activate();

            for(int i = 0; i < windowCount; i++)
            {
                input.Keyboard.KeyPress(VirtualKeyCode.ESCAPE).Sleep(100);
            }
            windowCount = 0;
        }

        public void SendChat(string message)
        {
            Activate();

            input.Keyboard
                .TextEntry(message)
                .KeyPress(VirtualKeyCode.RETURN)
                .Sleep(globalSleep);
        }

        public void HideChatLog()
        {
            Activate();

            input.Keyboard
                .KeyDown(VirtualKeyCode.END).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.END).Sleep(globalSleep);
        }

        public void ItemLot()
        {
            Activate();

            input.Keyboard
                .KeyDown(VirtualKeyCode.RETURN).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.RETURN).Sleep(globalSleep)
                .KeyDown(VirtualKeyCode.RETURN).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.RETURN).Sleep(globalSleep);
        }

        public void ItemPass()
        {
            Activate();
            LeftClick(165, 206);
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
                .LeftButtonDown().Sleep(globalSleep * 2)
                .LeftButtonUp().Sleep(globalSleep);
        }

        public void LeftClick(int x, int y)
        {
            MoveMouseOnLocalTo(x, y);

            input.Mouse
                .LeftButtonDown().Sleep(globalSleep)
                .LeftButtonUp().Sleep(globalSleep);
        }

        public void LeftDoubleClick()
        {
            input.Mouse
                .LeftButtonDown().Sleep(100)
                .LeftButtonUp().Sleep(5)
                .LeftButtonDown().Sleep(80)
                .LeftButtonUp().Sleep(5);
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
