using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using sglabo.entities;
using WindowsInput;
using WindowsInput.Native;

namespace sglabo.AI
{
    abstract class JobAI
    {
        public GridPosition goal = new GridPosition(1, 3);

        public SGWindow sg;
        public BattleCube cube;

        InputSimulator input = new InputSimulator();
        int globalSleep = 100;

        abstract public void PlayMove();
        abstract public void PlaySkill();

        protected Direction direction = Direction.D8;

        public JobAI()
        {

        }

        public JobAI(SGWindow sg, BattleCube cube)
        {
            this.sg = sg;
            this.cube = cube;
        }

        public void UpdateSituation(SGWindow sg, BattleCube cube)
        {
            this.sg = sg;
            this.cube = cube;
        }

        public void Ready()
        {
            sg.MoveMouseOnLocalTo(cube.core.x, cube.core.y);
            input.Mouse
                .LeftButtonClick().Sleep(globalSleep)
                .RightButtonClick().Sleep(globalSleep)
                .RightButtonClick().Sleep(globalSleep);

            Press(VirtualKeyCode.UP);
        }

        public void Press(VirtualKeyCode vk)
        {
            input.Keyboard
                .KeyDown(vk).Sleep(globalSleep)
                .KeyUp(vk).Sleep(globalSleep);
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

        public void Move(Direction direction, bool withEnter = true)
        {
            VirtualKeyCode vk = VirtualKeyCode.SPACE;
            switch(direction)
            {
                case Direction.D8:
                    vk = VirtualKeyCode.DOWN;
                    break;
                case Direction.D4:
                    vk = VirtualKeyCode.RIGHT;
                    break;
                case Direction.D6:
                    vk = VirtualKeyCode.LEFT;
                    break;
                case Direction.D2:
                    vk = VirtualKeyCode.UP;
                    break;
                case Direction.D5:
                    vk = VirtualKeyCode.RETURN;
                    break;
                default:
                    break;
            }

            if(vk != VirtualKeyCode.SPACE)
            {
                Press(vk);
            }

            if(withEnter)
            {
                Press(VirtualKeyCode.RETURN);
            }
        }

        public void Look(Direction direction)
        {
            Enter();
            Move(direction);
            this.direction = direction;
        }

        public bool ShouldStack(Direction d)
        {
            bool shouldStack = false;
            switch(d)
            {
                case Direction.D8:
                    shouldStack = direction == Direction.D8 && cube.NPC8() && !cube.NPC88() && !cube.NPC888();
                    break;
                case Direction.D6:
                    shouldStack = direction == Direction.D6 && cube.NPC6() && !cube.NPC66();
                    break;
                case Direction.D4:
                    shouldStack = direction == Direction.D4 && cube.NPC4() && !cube.NPC44();
                    break;
                default:
                    break;
            }
            return shouldStack;
        }

        public void Stack(Direction d)
        {
            switch(d)
            {
                case Direction.D8:
                    Move(Direction.D8);
                    Move(Direction.D2);
                    Look(Direction.D8);
                    break;
                case Direction.D6:
                    Move(Direction.D6);
                    Move(Direction.D4);
                    Look(Direction.D6);
                    break;
                case Direction.D4:
                    Move(Direction.D4);
                    Move(Direction.D6);
                    Look(Direction.D4);
                    break;
                case Direction.D2:
                    Move(Direction.D2);
                    Move(Direction.D8);
                    Look(Direction.D2);
                    break;
                default:
                    break;
            }
        }

        public void TopView()
        {
            Press(VirtualKeyCode.HOME);
        }

        public void Enter()
        {
            Press(VirtualKeyCode.RETURN);
        }

        public void ESC()
        {
            Press(VirtualKeyCode.ESCAPE);
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
                    vk = VirtualKeyCode.VK_3;
                    break;
                case SkillOrder.S4:
                    vk = VirtualKeyCode.VK_4;
                    break;
                case SkillOrder.S5:
                    vk = VirtualKeyCode.VK_5;
                    break;
                case SkillOrder.S6:
                    vk = VirtualKeyCode.VK_6;
                    break;
                case SkillOrder.S10:
                    vk = VirtualKeyCode.VK_0;
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
        }

        public void SelectTarget(params Direction[] inputQueue)
        {
            if(inputQueue.Length > 0)
            {
                Ready();

                foreach(Direction direction in inputQueue)
                {
                    Move(direction, true);
                }
            }

            Enter();
            Enter();
        }

        public void SelectTarget(LinkedList<Direction> inputQueue = null)
        {
            if(inputQueue != null)
            {
                Ready();

                foreach(Direction direction in inputQueue)
                {
                    Move(direction, true);
                }
            }

            Enter();
            Enter();
        }

        public int CountTrue(params bool[] bools)
        {
            int cnt = 0;
            foreach(bool b in bools)
            {
                cnt += b ? 1 : 0;
            }
            return cnt;
        }
    }
}
