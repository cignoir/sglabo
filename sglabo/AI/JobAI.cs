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
        public GridPosition goal = new GridPosition(1, 3);

        public SGWindow sg;
        public BattleCube cube;

        InputSimulator input = new InputSimulator();
        int globalSleep = 200;

        abstract public void PlayMove();
        abstract public void PlaySkill();

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
            // win7
            var os = Environment.OSVersion.Version;
            if(os.Major <= 7)
            {
                input.Keyboard
                        .KeyDown(VirtualKeyCode.UP).Sleep(globalSleep)
                        .KeyUp(VirtualKeyCode.UP).Sleep(globalSleep);
            }
            else if(os.Major > 7)
            {
                input.Keyboard
                        .KeyDown(VirtualKeyCode.DOWN).Sleep(globalSleep)
                        .KeyUp(VirtualKeyCode.DOWN).Sleep(globalSleep)
                        .KeyDown(VirtualKeyCode.UP).Sleep(globalSleep)
                        .KeyUp(VirtualKeyCode.UP).Sleep(globalSleep);
            }
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

        public void Move(Direction direction, bool max = false)
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
                    vk = VirtualKeyCode.RETURN;
                    break;
                default:
                    break;
            }

            if(vk != VirtualKeyCode.SPACE)
            {
                input.Keyboard
                    .KeyDown(vk).Sleep(globalSleep)
                    .KeyUp(vk).Sleep(globalSleep);
            }

            if(!max)
            {
                input.Keyboard
                    .KeyDown(VirtualKeyCode.RETURN).Sleep(globalSleep)
                    .KeyUp(VirtualKeyCode.RETURN).Sleep(globalSleep);
            }
        }

        public void MoveTo(GridPosition g)
        {
            if(g.x < sg.gPos.x)
            {
                for(int i = sg.gPos.x; i > g.x; i--)
                {
                    Move(Direction.D4);
                }
            }
            else if(g.x > sg.gPos.x)
            {
                for(int i = sg.gPos.x; i < g.x; i++)
                {
                    Move(Direction.D6);
                }
            }

            if(g.y < sg.gPos.y)
            {
                for(int i = sg.gPos.y; i > g.y; i--)
                {
                    Move(Direction.D8);
                }
            }
            else if(g.y > sg.gPos.y)
            {
                for(int i = sg.gPos.y; i < g.y; i++)
                {
                    Move(Direction.D2);
                }
            }

            Enter();
        }

        public void Look(Direction direction)
        {
            Enter();
            Move(direction);
        }

        public bool ShouldStack(Direction d)
        {
            //FIXME 向き考慮
            bool shouldStack = false;
            switch(d)
            {
                case Direction.D8:
                    shouldStack = cube.Exists8() && !cube.Exists88() && !cube.Exists888();
                    break;
                case Direction.D6:
                    shouldStack = cube.Exists6(); 
                    break;
                case Direction.D4:
                    shouldStack = cube.Exists4();
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
            // TODO: ターゲット選択
            Enter();
            Enter();
        }
    }
}
