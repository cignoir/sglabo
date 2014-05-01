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

        protected bool seal888 = false;
        protected bool seal886 = false;
        protected bool seal884 = false;
        protected bool seal866 = false;
        protected bool seal88 = false;
        protected bool seal84 = false;
        protected bool seal86 = false;
        protected bool seal44 = false;
        protected bool seal66 = false;
        protected bool seal844 = false;

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

        public void Ready(bool withKeyboardInput = true)
        {
            input.Keyboard
                .KeyUp(VirtualKeyCode.UP)
                .KeyUp(VirtualKeyCode.DOWN)
                .KeyUp(VirtualKeyCode.LEFT)
                .KeyUp(VirtualKeyCode.RIGHT);

            sg.MoveMouseOnLocalTo(cube.core.x, cube.core.y);
            input.Mouse
                .LeftButtonClick().Sleep(globalSleep)
                .RightButtonClick().Sleep(globalSleep)
                .LeftButtonClick().Sleep(globalSleep)
                .RightButtonClick().Sleep(globalSleep);

            if(withKeyboardInput)
            {
                input.Keyboard
                    .KeyDown(VirtualKeyCode.UP).Sleep(globalSleep * 2)
                    .KeyUp(VirtualKeyCode.UP).Sleep(globalSleep);
            }
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

        public void Look(Direction direction, bool maxMoved = false)
        {
            if(!maxMoved)
            {
                sg.LeftClick();
            }

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
                Press(vk);
            }

            Press(VirtualKeyCode.RETURN);
            this.direction = direction;
        }

        public bool ShouldStack(Direction d)
        {
            bool shouldStack = false;
            switch(d)
            {
                case Direction.D8:
                    shouldStack = direction == Direction.D8 && cube.NPC8 && !cube.NPC88;
                    break;
                case Direction.D6:
                    shouldStack = direction == Direction.D6 && cube.NPC6 && !cube.NPC66;
                    break;
                case Direction.D4:
                    shouldStack = direction == Direction.D4 && cube.NPC4 && !cube.NPC44;
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
                    Move(Direction.D5);
                    Look(Direction.D8, true); // 3歩前提
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
                case SkillOrder.S7:
                    vk = VirtualKeyCode.VK_7;
                    break;
                case SkillOrder.S8:
                    vk = VirtualKeyCode.VK_8;
                    break;
                case SkillOrder.S9:
                    vk = VirtualKeyCode.VK_9;
                    break;
                case SkillOrder.S10:
                    vk = VirtualKeyCode.VK_0;
                    break;
                default:
                    vk = VirtualKeyCode.VK_1;
                    break;
            }

            input.Keyboard
                .KeyUp(vk)
                .KeyUp(VirtualKeyCode.LCONTROL)
                .KeyDown(VirtualKeyCode.LCONTROL).Sleep(globalSleep)
                .KeyDown(vk).Sleep(globalSleep)
                .KeyUp(vk)
                .KeyUp(VirtualKeyCode.LCONTROL);
            Thread.Sleep(globalSleep);
        }

        public void InitSeal()
        {
            seal888 = false;
            seal886 = false;
            seal884 = false;
            seal88 = false;
            seal84 = false;
            seal86 = false;
            seal44 = false;
            seal66 = false;
            seal844 = false;
            seal866 = false;
        }

        public int SealCost()
        {
            var sealedList = new List<bool> { seal888, seal886, seal884, seal866, seal88, seal84, seal86, seal44, seal844 }.Where(x => x);
            var sealedCount = sealedList != null ? sealedList.Count() : 0;
            return sg != null && (sg.job == Job.黒印 || sg.job == Job.錬金) ? sealedCount * 2 : 0;
        }

        public void Move(params Direction[] inputQueue)
        {
            if(inputQueue.Length > 0)
            {
                int gx = 2;
                int gy = 4;

                foreach(Direction direction in inputQueue)
                {
                    switch(direction)
                    {
                        case Direction.D8:
                            gy--;
                            break;
                        case Direction.D6:
                            gx++;
                            break;
                        case Direction.D4:
                            gx--;
                            break;
                        default:
                            break;
                    }
                }

                var sPos = cube.cells[gx, gy].sPos;
                sg.MoveMouseOnLocalTo(sPos.x, sPos.y);
                sg.LeftClick();
            }
        }

        public void Fix()
        {
            sg.LeftClick();
        }

        public void SelectTarget(params Direction[] inputQueue)
        {
            if(inputQueue.Length > 0)
            {
                int gx = 2;
                int gy = 4;

                foreach(Direction direction in inputQueue)
                {
                    switch(direction)
                    {
                        case Direction.D8:
                            gy--;
                            break;
                        case Direction.D6:
                            gx++;
                            break;
                        case Direction.D4:
                            gx--;
                            break;
                        default:
                            break;
                    }
                }

                var sPos = cube.cells[gx, gy].sPos;
                sg.MoveMouseOnLocalTo(sPos.x + 3, sPos.y + 10);

                sg.LeftClick();
                sg.RightClick();

                sg.LeftDoubleClick();
                sg.LeftDoubleClick();

                //sg.LeftClick();
                //sg.RightClick();

                //sg.LeftDoubleClick();
                //sg.LeftDoubleClick();
            }

            //Enter();
            //Enter();
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
