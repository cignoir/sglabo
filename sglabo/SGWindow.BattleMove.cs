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
        public void BattleMove(BattleField battleField)
        {
            if(battleField.turn == 1)
            {
                Move(Direction.D8);
                Move(Direction.D8);
                Move(Direction.D8);
                Look(Direction.D8);
                Enter();
            }
            else
            {
            }

            Go();
        }

        public void Move(Direction direction){
            var keyboard = input.Keyboard;

            VirtualKeyCode vk = VirtualKeyCode.SPACE;
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
                    break;
                default:
                    vk = VirtualKeyCode.RETURN;
                    break;
            }

            if(vk != VirtualKeyCode.SPACE){
                keyboard
                    .KeyDown(vk).Sleep(globalSleep)
                    .KeyUp(vk);
            }

            keyboard
                .KeyDown(VirtualKeyCode.RETURN).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.RETURN).Sleep(globalSleep);
        }

        public void Look(Direction direction)
        {
            Move(direction);
        }

        public void Enter()
        {
            input.Keyboard
                .KeyDown(VirtualKeyCode.RETURN).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.RETURN).Sleep(globalSleep);
        }
    }
}
