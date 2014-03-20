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
        public void BattleMove()
        {
            Move(Direction.D8);
            Move(Direction.D8);
            Move(Direction.D8);
            Look(Direction.D8);
            Enter();
            Go();
        }

        private void Move(Direction direction){
            var keyboard = input.Keyboard;

            VirtualKeyCode vk;
            switch(direction)
            {
                case Direction.D8:
                    vk = VirtualKeyCode.UP;
                    break;
                case Direction.D4:
                    vk = VirtualKeyCode.LEFT;
                    break;
                case Direction.D6:
                    vk = VirtualKeyCode.RIGHT;
                    break;
                case Direction.D2:
                    vk = VirtualKeyCode.DOWN;
                    break;
                case Direction.D5:
                    return;
                default:
                    vk = VirtualKeyCode.RETURN;
                    break;
            }

            keyboard.KeyPress(vk).Sleep(globalSleep)
                .KeyPress(VirtualKeyCode.RETURN).Sleep(globalSleep);
        }

        private void Look(Direction direction)
        {
            Move(direction);
        }

        private void Enter()
        {
            input.Keyboard.KeyPress(VirtualKeyCode.RETURN).Sleep(globalSleep);
        }
    }
}
