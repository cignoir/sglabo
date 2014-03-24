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
        public int globalSleep = 200;

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

        public void CloseItemWindow()
        {
            Activate();

            input.Keyboard
                .KeyPress(VirtualKeyCode.ESCAPE)
                .Sleep(globalSleep);
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
                .KeyDown(VirtualKeyCode.END)
                .Sleep(globalSleep);
        }

        public void Go()
        {
            // Ctrl + B
            input.Keyboard
                .KeyDown(VirtualKeyCode.LCONTROL).Sleep(globalSleep)
                .KeyDown(VirtualKeyCode.VK_B).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.VK_B).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.LCONTROL).Sleep(globalSleep);
        }

        public void ItemLot()
        {
            Activate();

            input.Keyboard
                .KeyPress(VirtualKeyCode.RETURN).Sleep(globalSleep);
        }

        public void SelectSkill(int index)
        {
            VirtualKeyCode vk = VirtualKeyCode.SPACE;
            switch(index)
            {
                case 1:
                    vk = VirtualKeyCode.VK_1;
                    break;
                case 2:
                    vk = VirtualKeyCode.VK_2;
                    break;
                case 3:
                    vk = VirtualKeyCode.VK_2;
                    break;
                case 4:
                    vk = VirtualKeyCode.VK_2;
                    break;
                case 5:
                    vk = VirtualKeyCode.VK_2;
                    break;
                case 6:
                    vk = VirtualKeyCode.VK_2;
                    break;
                default:
                    vk = VirtualKeyCode.VK_1;
                    break;
            }

            input.Keyboard
                .KeyDown(VirtualKeyCode.LCONTROL).Sleep(globalSleep)
                .KeyDown(vk).Sleep(globalSleep)
                .KeyUp(vk).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.LCONTROL).Sleep(globalSleep);
            Enter();
            Enter();
        }
    }
}
