using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;

namespace sglabo.AI
{
    class ProtectionMage: JobAI
    {
        public ProtectionMage()
        {

        }

        public ProtectionMage(SGWindow sg, BattleCube cube)
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
                    if(Battle.area == Area.戦豹の試練)
                    {
                        if(Battle.firstMatch)
                        {
                            Move(Direction.D8, Direction.D8, Direction.D8);
                            Look(Direction.D8, true);
                        }
                        else
                        {
                            Move(Direction.D8, Direction.D8);
                            Look(Direction.D8, true);
                        }
                    }
                    else if(Battle.area == Area.猛虎の試練)
                    {
                        if(Battle.firstMatch)
                        {
                            Move(Direction.D8, Direction.D8, Direction.D8);
                            Look(Direction.D8, true);
                        }
                        else
                        {
                            Move(Direction.D8);
                            Look(Direction.D8, true);
                        }
                    }
                    else if(Battle.area == Area.荒獅子の試練)
                    {
                        if(Battle.firstMatch)
                        {
                            Move(Direction.D8, Direction.D8);
                            Look(Direction.D8, true);
                        }
                        else
                        {
                            Move(Direction.D8, Direction.D8);
                            Look(Direction.D8);
                        }
                    }
                }
                else
                {
                    switch(Battle.mapCode)
                    {
                        case 21448090:
                            // ナビアA
                            Move(Direction.D6);
                            Look(Direction.D8);
                            break;
                        case 5409959:
                            // ナビアB
                            Move(Direction.D8, Direction.D6);
                            Look(Direction.D8);
                            break;
                        case 9346174: // グランドロンA
                            Move(Direction.D4);
                            Look(Direction.D8);
                            break;
                        case 266499533: // グランドロンB
                            Move(Direction.D4);
                            Look(Direction.D8);
                            break;
                        case 209593538: // グランドロンC
                            Move(Direction.D8, Direction.D4, Direction.D4);
                            Look(Direction.D8, true);
                            break;
                        default:
                            // test
                            TopView();
                            Move(Direction.D5);
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
            /*
             * 1. プチリカバー
             * 2. リカバー
             * 3. ハイリカバー
             * 4. リカバーボール
             * 5. プチリカバースクエア
             * 6. リカバースクエア
             * 7. ガードアセント
             * 8. セントハリケーン
             */

            if(Battle.area == Area.荒獅子の試練)
            {
                // LB
                if(sg.ap >= 3 && (cube.NPC88 || cube.NPC8))
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S9);

                    if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC8) SelectTarget(Direction.D8);
                    Go();
                    return;
                }

                // SF
                if(sg.ap >= 12 && cube.NPC8888)
                {
                    sg.ap -= 12;
                    SelectSkill(SkillOrder.S10);
                    if(cube.NPC8888) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8);
                    Go();
                    return;
                }

                // SH
                if(sg.ap >= 16 && (cube.NPC888 || cube.NPC8884 || cube.NPC8886 || cube.NPC88844 || cube.NPC88866 || cube.NPC88 || cube.NPC884 || cube.NPC886))
                {
                    sg.ap -= 16;
                    SelectSkill(SkillOrder.S8);
                    if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC884) SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC886) SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC8884) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC8886) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC88866) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6, Direction.D6);
                    else if(cube.NPC88844) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4, Direction.D4);
                    Go();
                    return;
                }
            }
            else if(Battle.mapCode == 21448090)
            {
                if(Battle.turn == 1 || Battle.turn == 7)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S1);
                    SelectTarget(Direction.D8);
                    Go();
                    return;
                }
                else if(Battle.turn == 2 || Battle.turn == 3 || Battle.turn == 5)
                {
                    Go();
                    return;
                }
                else if(Battle.turn == 4)
                {
                    sg.ap -= 26;
                    SelectSkill(SkillOrder.S6);
                    SelectTarget(Direction.D8, Direction.D4, Direction.D4);
                    Go();
                    return;
                }
                else if(Battle.turn == 6)
                {
                    sg.ap -= 26;
                    SelectSkill(SkillOrder.S4);
                    SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                    Go();
                    return;
                }
                else if(Battle.turn >= 8)
                {
                    if(sg.ap >= 16 && (cube.NPC888 || cube.NPC8884 || cube.NPC8886 || cube.NPC88844 || cube.NPC88866 || cube.NPC88 || cube.NPC884 || cube.NPC886))
                    {
                        sg.ap -= 16;
                        SelectSkill(SkillOrder.S8);
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
            }
            else if(Battle.mapCode == 5409959)
            {
                if(Battle.turn == 1 || Battle.turn == 7)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S1);
                    SelectTarget(Direction.D8);
                    Go();
                    return;
                }
                else if(Battle.turn == 2 || Battle.turn == 3 || Battle.turn == 5)
                {
                    Go();
                    return;
                }
                else if(Battle.turn == 4)
                {
                    sg.ap -= 26;
                    SelectSkill(SkillOrder.S4);
                    SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                    Go();
                    return;
                }
                else if(Battle.turn == 6)
                {
                    sg.ap -= 26;
                    SelectSkill(SkillOrder.S4);
                    SelectTarget(Direction.D8, Direction.D4, Direction.D4);
                    Go();
                    return;
                }
                else if(Battle.turn >= 8)
                {
                    if(sg.ap >= 16 && (cube.NPC888 || cube.NPC8884 || cube.NPC8886 || cube.NPC88844 || cube.NPC88866 || cube.NPC88 || cube.NPC884 || cube.NPC886))
                    {
                        sg.ap -= 16;
                        SelectSkill(SkillOrder.S8);
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
            }
            else if(Battle.IsGrandron())
            {
                if(Battle.mapCode == 209593538)
                {
                    if(Battle.turn % 4 == 0)
                    {
                        sg.ap -= 20;
                        SelectSkill(SkillOrder.S5);
                        SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                        Go();
                        return;
                    }

                    if(Battle.turn % 2 == 0)
                    {
                        sg.ap -= 20;
                        SelectSkill(SkillOrder.S5);
                        SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                        Go();
                        return;
                    }
                }
                else
                {
                    if(Battle.turn % 4 == 0)
                    {
                        sg.ap -= 26;
                        SelectSkill(SkillOrder.S5);
                        SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                        Go();
                        return;
                    }

                    if(Battle.turn % 2 == 0)
                    {
                        sg.ap -= 26;
                        SelectSkill(SkillOrder.S5);
                        SelectTarget(Direction.D8, Direction.D4, Direction.D4);
                        Go();
                        return;
                    }
                }
            }
            else if(Battle.IsBattleArena())
            {
                if(sg.ap >= 16 && (cube.NPC888 || cube.NPC8884 || cube.NPC8886 || cube.NPC88844 || cube.NPC88866 || cube.NPC88 || cube.NPC884 || cube.NPC886))
                {
                    sg.ap -= 16;
                    SelectSkill(SkillOrder.S8);
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

            Go();
        }
    }
}
