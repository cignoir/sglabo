using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace sglabo
{
    partial class SGWindow
    {
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
    }
}
