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
        public void BattleAction()
        {
            input.Keyboard
                .KeyDown(VirtualKeyCode.LCONTROL).Sleep(globalSleep)
                .KeyDown(VirtualKeyCode.VK_1).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.VK_1).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.LCONTROL).Sleep(globalSleep);
            Enter();
            Enter();    
            Go();
        }
    }
}
