using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;
using WindowsInput;
using WindowsInput.Native;

namespace sglabo.AI
{
    abstract class JobAI
    {
        InputSimulator input = new InputSimulator();
        int globalSleep = 200;

        abstract public void PlayMove(BattleField bf, SGWindow sg);
        abstract public void PlaySkill(BattleField bf, SGWindow sg);

        public void Ready()
        {
            input.Keyboard
                    .KeyDown(VirtualKeyCode.UP).Sleep(globalSleep)
                    .KeyUp(VirtualKeyCode.UP).Sleep(globalSleep)
                    .KeyDown(VirtualKeyCode.DOWN).Sleep(globalSleep)
                    .KeyUp(VirtualKeyCode.DOWN).Sleep(globalSleep);
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

        public void Move(Direction direction)
        {
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

            if(vk != VirtualKeyCode.SPACE)
            {
                input.Keyboard
                    .KeyDown(vk).Sleep(globalSleep)
                    .KeyUp(vk);
            }

            input.Keyboard
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

        public void ESC()
        {
            input.Keyboard
                .KeyDown(VirtualKeyCode.ESCAPE).Sleep(globalSleep)
                .KeyUp(VirtualKeyCode.ESCAPE).Sleep(globalSleep);
        }

        public void SelectSkill(SkillOrder skill)
        {
            VirtualKeyCode vk = VirtualKeyCode.SPACE;
            switch(skill)
            {
                case SkillOrder.S1:
                    vk = VirtualKeyCode.VK_1;
                    break;
                case SkillOrder.S2:
                    vk = VirtualKeyCode.VK_2;
                    break;
                case SkillOrder.S3:
                    vk = VirtualKeyCode.VK_2;
                    break;
                case SkillOrder.S4:
                    vk = VirtualKeyCode.VK_2;
                    break;
                case SkillOrder.S5:
                    vk = VirtualKeyCode.VK_2;
                    break;
                case SkillOrder.S6:
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
