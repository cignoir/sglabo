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
                if(Battle.IsBattleArena())
                {
                    if(Battle.firstMatch)
                    {
                        Move(Direction.D8, Direction.D8, Direction.D8);
                        Look(Direction.D8, true);
                    }
                    else
                    {
                        Move(Direction.D8, Direction.D8);
                        Look(Direction.D8);
                    }
                }
                else
                {
                    switch(Battle.mapCode)
                    {
                        case 21448090:
                            // ナビアA
                            Move(Direction.D8, Direction.D4);
                            Look(Direction.D8);
                            break;
                        case 5409959:
                            // ナビアB
                            Move(Direction.D8, Direction.D8, Direction.D8);
                            Look(Direction.D4, true);
                            break;
                        case 9346174: // グランドロンA 池
                            Move(Direction.D8, Direction.D6);
                            Look(Direction.D8);
                            break;
                        case 266499533: // グランドロンB 枝
                            Move(Direction.D8, Direction.D6);
                            Look(Direction.D8);
                            break;
                        case 209593538: // グランドロンC
                            Move(Direction.D8, Direction.D8, Direction.D8);
                            Look(Direction.D8);
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
            if(cube.NPC888 || cube.NPC8888 || cube.NPC8886 || cube.NPC8884 || cube.NPC88) seal888 = false;
            if(cube.NPC884 || cube.NPC8884 || cube.NPC88 || cube.NPC8844 || cube.NPC84) seal884 = false;
            if(cube.NPC844 || cube.NPC8844 || cube.NPC84 /*|| cube.NPC8444*/ || cube.NPC44) seal844 = false;
            if(cube.NPC886 || cube.NPC8886 || cube.NPC88 || cube.NPC8866 || cube.NPC86) seal886 = false;
            if(cube.NPC866 || cube.NPC8866 || cube.NPC86 /*|| cube.NPC8666*/ || cube.NPC66) seal866 = false;
            if(cube.NPC88) seal88 = false;
            if(cube.NPC84) seal84 = false;
            if(cube.NPC44) seal44 = false;
            if(cube.NPC66) seal66 = false;

            if(Battle.mapCode != 209593538 && !(cube.PC8 || cube.PC4 || cube.PC6))
            {
                // イレギュラー回避
                Go();
                return;
            }

            #region ナビア
            if(Battle.mapCode == 21448090 || Battle.mapCode == 5409959)
            {

                if(Battle.turn == 2)
                {
                    if(direction == Direction.D8)
                    {
                        if(sg.ap >= 6 && !cube.NPC88 && !seal88)
                        {
                            sg.ap -= 6;
                            seal88 = true;
                            SelectSkill(SkillOrder.S2);
                            SelectTarget(Direction.D8, Direction.D8);
                            Go();
                            return;
                        }

                        if(sg.ap >= 6 && !cube.NPC84 && !seal84)
                        {
                            sg.ap -= 6;
                            seal84 = true;
                            SelectSkill(SkillOrder.S2);
                            SelectTarget(Direction.D8, Direction.D4);
                            Go();
                            return;
                        }
                    }
                    else if(direction == Direction.D4)
                    {
                        if(sg.ap >= 6 && !cube.NPC44 && !seal44)
                        {
                            sg.ap -= 6;
                            seal44 = true;
                            SelectSkill(SkillOrder.S2);
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
                        if(sg.ap >= 9 && !cube.NPC884 && !seal884)
                        {
                            sg.ap -= 9;
                            seal884 = true;
                            SelectSkill(SkillOrder.S1);
                            SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                            Go();
                            return;
                        }

                        if(sg.ap >= 9 && !cube.NPC888 && !seal888)
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
                        if(sg.ap >= 9 && !cube.NPC844 && !seal844)
                        {
                            sg.ap -= 9;
                            seal844 = true;
                            SelectSkill(SkillOrder.S1);
                            SelectTarget(Direction.D8, Direction.D4, Direction.D4);
                            Go();
                            return;
                        }

                        if(sg.ap >= 6 && !cube.NPC44 && !seal44)
                        {
                            sg.ap -= 6;
                            seal44 = true;
                            SelectSkill(SkillOrder.S2);
                            SelectTarget(Direction.D4, Direction.D4);
                            Go();
                            return;
                        }
                    }
                }
            }
            #endregion

            #region グランドロン
            if(Battle.IsGrandron())
            {
                if(Battle.mapCode == 9346174 || Battle.mapCode == 266499533)
                {
                    if(sg.ap >= 9 && !cube.NPC888 && !seal888)
                    {
                        sg.ap -= 9;
                        seal888 = true;
                        SelectSkill(SkillOrder.S1);
                        SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                        Go();
                        return;
                    }

                    if(sg.ap >= 16 && (cube.NPC888 || cube.NPC8884 || cube.NPC8886 || cube.NPC88844 || cube.NPC88866 || cube.NPC88 || cube.NPC884 || cube.NPC886))
                    {
                        sg.ap -= 16;
                        SelectSkill(SkillOrder.S4);
                        if(cube.NPC884) SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                        else if(cube.NPC8884) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4);
                        else if(cube.NPC88844) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4, Direction.D4);
                        else if(cube.NPC886) SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                        else if(cube.NPC8886) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6);
                        else if(cube.NPC88866) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6, Direction.D6);
                        else if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                        else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                        Go();
                        return;
                    }
                }

                if(Battle.mapCode == 209593538)
                {
                    if(sg.ap >= 9 && !cube.NPC86 && !seal86)
                    {
                        sg.ap -= 9;
                        seal86 = true;
                        SelectSkill(SkillOrder.S1);
                        SelectTarget(Direction.D8, Direction.D6);
                        Go();
                        return;
                    }

                    if(sg.ap >= 16 && (cube.NPC888 || cube.NPC8884 || cube.NPC8886 || cube.NPC88844 || cube.NPC88866 || cube.NPC88 || cube.NPC884 || cube.NPC886))
                    {
                        sg.ap -= 16;
                        SelectSkill(SkillOrder.S4);
                        if(cube.NPC884) SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                        else if(cube.NPC88844) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4, Direction.D4);
                        else if(cube.NPC8884) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4);
                        else if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                        else if(cube.NPC88866) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6, Direction.D6);
                        else if(cube.NPC8886) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6);
                        else if(cube.NPC886) SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                        else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                        Go();
                        return;
                    }
                }
            }
            #endregion

            #region 闘錬
            if(Battle.IsBattleArena())
            {
                if(Battle.firstMatch)
                {
                    if(sg.ap >= 9 && !cube.NPC88 && !seal88)
                    {
                        sg.ap -= 9;
                        seal888 = true;
                        SelectSkill(SkillOrder.S1);
                        SelectTarget(Direction.D8, Direction.D8);
                        Go();
                        return;
                    }
                }
                else
                {
                    if(sg.ap >= 9 && !cube.NPC888 && !seal888)
                    {
                        sg.ap -= 9;
                        seal888 = true;
                        SelectSkill(SkillOrder.S1);
                        SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                        Go();
                        return;
                    }
                }

                if(sg.ap >= 16 && (cube.NPC888 || cube.NPC8884 || cube.NPC8886 || cube.NPC88844 || cube.NPC88866 || cube.NPC88 || cube.NPC884 || cube.NPC886))
                {
                    sg.ap -= 16;
                    SelectSkill(SkillOrder.S4);
                    if(cube.NPC884) SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC88844) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4, Direction.D4);
                    else if(cube.NPC8884) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC88866) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6, Direction.D6);
                    else if(cube.NPC8886) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC886) SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    Go();
                    return;
                }
            }
            #endregion

            Go();
        }
    }
}
