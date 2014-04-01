﻿using System;
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
                        TopView();
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
            if(cube.NPC888() || cube.NPC8888() || cube.NPC8886() || cube.NPC8884() || cube.NPC88()) seal888 = false;
            if(cube.NPC884() || cube.NPC8884() || cube.NPC88() || cube.NPC8844() || cube.NPC84()) seal884 = false;
            if(cube.NPC844() || cube.NPC8844() || cube.NPC84() /*|| cube.NPC8444()*/ || cube.NPC44()) seal844 = false;
            if(cube.NPC886() || cube.NPC8886() || cube.NPC88() || cube.NPC8866() || cube.NPC86()) seal886 = false;
            if(cube.NPC88()) seal88 = false;
            if(cube.NPC84()) seal84 = false;
            if(cube.NPC44()) seal44 = false;

            if(!cube.PC8() && !cube.PC4())
            {
                // イレギュラー回避
                Go();
                return;
            }


            if(Battle.turn == 2)
            {
                if(direction == Direction.D8)
                {
                    if(sg.ap >= 6 && !cube.NPC88() && !seal88)
                    {
                        sg.ap -= 6;
                        seal88 = true;
                        SelectSkill(Battle.turn < 4 ? SkillOrder.S2 : SkillOrder.S3);
                        SelectTarget(Direction.D8, Direction.D8);
                        Go();
                        return;
                    }

                    if(sg.ap >= 6 && !cube.NPC84() && !seal84)
                    {
                        sg.ap -= 6;
                        seal84 = true;
                        SelectSkill(Battle.turn < 4 ? SkillOrder.S2 : SkillOrder.S3);
                        SelectTarget(Direction.D8, Direction.D4);
                        Go();
                        return;
                    }
                }
                else if(direction == Direction.D4)
                {
                    if(sg.ap >= 6 && !cube.NPC44() && !seal44)
                    {
                        sg.ap -= 6;
                        seal44 = true;
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
                    if(sg.ap >= 9 && !cube.NPC884() && !seal884)
                    {
                        sg.ap -= 9;
                        seal884 = true;
                        SelectSkill(SkillOrder.S1);
                        SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                        Go();
                        return;
                    }

                    if(sg.ap >= 9 && !cube.NPC888() && !seal888)
                    {
                        sg.ap -= 9;
                        seal888 = true;
                        SelectSkill(SkillOrder.S1);
                        SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                        Go();
                        return;
                    }

                }
                else if(direction == Direction.D4)
                {
                    if(sg.ap >= 9 && !cube.NPC844() && !seal844)
                    {
                        sg.ap -= 9;
                        seal844 = true;
                        SelectSkill(SkillOrder.S1);
                        SelectTarget(Direction.D8, Direction.D4, Direction.D4);
                        Go();
                        return;
                    }

                    if(sg.ap >= 6 && !cube.NPC44() && !seal44)
                    {
                        sg.ap -= 6;
                        seal44 = true;
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
