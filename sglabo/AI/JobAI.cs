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
        public BattleField bf;
        public SGWindow sg;

        InputSimulator input = new InputSimulator();
        int globalSleep = 200;

        abstract public void SetGoal();
        abstract public void PlayMove();
        abstract public void PlaySkill();

        public JobAI()
        {

        }

        public JobAI(BattleField bf, SGWindow sg)
        {
            this.bf = bf;
            this.sg = sg;
        }

        public void UpdateSituation(BattleField bf, SGWindow sg)
        {
            sg.ai.SetGoal();
            this.bf = bf;
            this.sg = sg;
        }

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

        public void Move(Direction direction, bool withEnter = false)
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

            if(withEnter)
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
            Move(direction, true);
        }

        public bool ShouldBeStack(Direction d)
        {
            bool shouldBeStack = false;
            switch(d)
            {
                case Direction.D8:
                    shouldBeStack = bf.Cell(sg.gPos.x, sg.gPos.y - 1).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 2).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 3).existsNPC;
                    break;
                case Direction.D6:
                    shouldBeStack = bf.Cell(sg.gPos.x + 1, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x + 2, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x + 3, sg.gPos.y).existsNPC;
                    break;
                case Direction.D4:
                    shouldBeStack = bf.Cell(sg.gPos.x - 1, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x - 2, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x -3, sg.gPos.y).existsNPC;
                    break;
                case Direction.D2:
                    shouldBeStack = bf.Cell(sg.gPos.x, sg.gPos.y + 1).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y + 2).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y + 3).existsNPC;
                    break;
                default:
                    break;
            }
            return shouldBeStack;
        }

        public void Stack(Direction d)
        {
            switch(d)
            {
                case Direction.D8:
                    Move(Direction.D8, true);
                    Move(Direction.D2, true);
                    Look(Direction.D8);
                    break;
                case Direction.D6:
                    Move(Direction.D6, true);
                    Move(Direction.D4, true);
                    Look(Direction.D6);
                    break;
                case Direction.D4:
                    Move(Direction.D4, true);
                    Move(Direction.D6, true);
                    Look(Direction.D4);
                    break;
                case Direction.D2:
                    Move(Direction.D2, true);
                    Move(Direction.D8, true);
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

        #region ExistsXXX
        public bool Exists8()
        {
            return bf.Cell(sg.gPos.x, sg.gPos.y - 1).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 1).existsPC;
        }

        public bool Exists88()
        {
            return bf.Cell(sg.gPos.x, sg.gPos.y - 2).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 2).existsPC;
        }

        public bool Exists888()
        {
            return bf.Cell(sg.gPos.x, sg.gPos.y - 3).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 3).existsPC;
        }

        public bool Exists8888()
        {
            return bf.Cell(sg.gPos.x, sg.gPos.y - 4).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 4).existsPC;
        }

        public bool Exists88888()
        {
            return bf.Cell(sg.gPos.x, sg.gPos.y - 5).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 5).existsPC;
        }

        public bool Exists6()
        {
            return bf.Cell(sg.gPos.x + 1, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x + 1, sg.gPos.y).existsPC;
        }

        public bool Exists66()
        {
            return bf.Cell(sg.gPos.x + 2, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x + 2, sg.gPos.y).existsPC;
        }

        public bool Exists666()
        {
            return bf.Cell(sg.gPos.x + 3, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x + 3, sg.gPos.y).existsPC;
        }

        public bool Exists4()
        {
            return bf.Cell(sg.gPos.x - 1, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x - 1, sg.gPos.y).existsPC;
        }

        public bool Exists44()
        {
            return bf.Cell(sg.gPos.x - 2, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x - 2, sg.gPos.y).existsPC;
        }

        public bool Exists444()
        {
            return bf.Cell(sg.gPos.x - 3, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x - 3, sg.gPos.y).existsPC;
        }

        public bool Exists86()
        {
            return bf.Cell(sg.gPos.x + 1, sg.gPos.y - 1).existsNPC && !bf.Cell(sg.gPos.x + 1, sg.gPos.y - 1).existsPC;
        }

        public bool Exists886()
        {
            return bf.Cell(sg.gPos.x + 1, sg.gPos.y - 2).existsNPC && !bf.Cell(sg.gPos.x + 1, sg.gPos.y - 2).existsPC;
        }

        public bool Exists84()
        {
            return bf.Cell(sg.gPos.x - 1, sg.gPos.y - 1).existsNPC && !bf.Cell(sg.gPos.x - 1, sg.gPos.y - 1).existsPC;
        }

        public bool Exists884()
        {
            return bf.Cell(sg.gPos.x - 1, sg.gPos.y - 2).existsNPC && !bf.Cell(sg.gPos.x - 1, sg.gPos.y - 2).existsPC;
        }
        #endregion
    }
}
