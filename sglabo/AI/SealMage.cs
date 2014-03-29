using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;

namespace sglabo.AI
{
    class SealMage: JobAI
    {
        public SealMage()
        {

        }

        public SealMage(SGWindow sg, BattleCube cube)
        {
            this.sg = sg;
            this.cube = cube;
        }

        override public void PlayMove()
        {
            if(Battle.turn == 1)
            {
                Ready();

                switch(Battle.mapCode)
                {
                    case 21448090:
                        // ナビアA
                        Move(Direction.D4);
                        Move(Direction.D8);
                        Look(Direction.D8);
                        break;
                    case 5409959:
                        // ナビアB
                        Move(Direction.D8);
                        Move(Direction.D8);
                        Move(Direction.D8, false);
                        Look(Direction.D4);
                        break;
                    default:
                        // test
                        Move(Direction.D5);
                        //Move(Direction.D2);
                        Look(Direction.D8);
                        break;
                }
            }
            else
            {
                //if(ShouldStack(Direction.D8))
                //{
                //    Stack(Direction.D8);
                //}
            }

            Go();
        }

        override public void PlaySkill()
        {
            if(Battle.turn == 2)
            {
                if(direction == Direction.D8)
                {
                    if(sg.ap >= 6 && !cube.NPC88())
                    {
                        sg.ap -= 6;
                        SelectSkill(Battle.turn < 4 ? SkillOrder.S2 : SkillOrder.S3);
                        SelectTarget(Direction.D8, Direction.D8);
                        Go();
                        return;
                    }

                    if(sg.ap >= 6 && !cube.NPC84())
                    {
                        sg.ap -= 6;
                        SelectSkill(Battle.turn < 4 ? SkillOrder.S2 : SkillOrder.S3);
                        SelectTarget(Direction.D8, Direction.D4);
                        Go();
                        return;
                    }
                }
                else if(direction == Direction.D4)
                {
                    if(sg.ap >= 6 && !cube.NPC44())
                    {
                        sg.ap -= 6;
                        SelectSkill(Battle.turn < 4 ? SkillOrder.S2 : SkillOrder.S3);
                        SelectTarget(Direction.D4, Direction.D4);
                        Go();
                        return;
                    }
                }
            }
            else if(Battle.turn > 2)
            {
                if(direction == Direction.D8)
                {
                    if(sg.ap >= 9 && !cube.NPC884() && !cube.NPC888())
                    {
                        sg.ap -= 9;
                        SelectSkill(SkillOrder.S1);
                        SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                        Go();
                        return;
                    }

                    if(Battle.turn % 2 == 1)
                    {
                        if(sg.ap >= 9 && !cube.NPC884())
                        {
                            sg.ap -= 9;
                            SelectSkill(SkillOrder.S1);
                            SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                            Go();
                            return;
                        }

                        if(sg.ap >= 9 && !cube.NPC888())
                        {
                            sg.ap -= 9;
                            SelectSkill(SkillOrder.S1);
                            SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                            Go();
                            return;
                        }
                    }

                    if(Battle.turn % 2 == 0)
                    {

                        if(sg.ap >= 9 && !cube.NPC888())
                        {
                            sg.ap -= 9;
                            SelectSkill(SkillOrder.S1);
                            SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                            Go();
                            return;
                        }

                        if(sg.ap >= 9 && !cube.NPC884())
                        {
                            sg.ap -= 9;
                            SelectSkill(SkillOrder.S1);
                            SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                            Go();
                            return;
                        }
                    }
                }
                else if(direction == Direction.D4)
                {
                    if(sg.ap >= 9 && !cube.NPC844())
                    {
                        sg.ap -= 9;
                        SelectSkill(SkillOrder.S1);
                        SelectTarget(Direction.D8, Direction.D4, Direction.D4);
                        Go();
                        return;
                    }

                    if(sg.ap >= 6 && !cube.NPC44())
                    {
                        sg.ap -= 6;
                        SelectSkill(Battle.turn < 4 ? SkillOrder.S2 : SkillOrder.S3);
                        SelectTarget(Direction.D4, Direction.D4);
                        Go();
                        return;
                    }
                }
            }

            Go();
        }
    }
}
