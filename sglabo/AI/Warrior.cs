using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;
using WindowsInput.Native;

namespace sglabo.AI
{
    class Warrior: JobAI
    {
        public Warrior()
        {

        }

        public Warrior(SGWindow sg, BattleCube cube)
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
                        Move(Direction.D8, Direction.D8, Direction.D8);
                        Look(Direction.D8, true);
                    }
                }
                else
                {
                    switch(Battle.mapCode)
                    {
                        case 21448090: 
                            // ナビアA
                            Move(Direction.D8, Direction.D8);
                            Look(Direction.D8);
                            break;
                        case 5409959:
                            // ナビアB
                            Move(Direction.D8, Direction.D8, Direction.D8);
                            Look(Direction.D4, true);
                            break;
                        case 9346174: // グランドロンA
                            Move(Direction.D8, Direction.D8, Direction.D8);
                            Look(Direction.D8, true);
                            break;
                        case 266499533: // グランドロンB
                            Move(Direction.D8, Direction.D8, Direction.D8);
                            Look(Direction.D8, true);
                            break;
                        case 209593538: // グランドロンC
                            Move(Direction.D8, Direction.D8, Direction.D8);
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
                //if(Battle.IsGrandron() && ShouldStack(Direction.D8))
                //{
                //    Ready(false);
                //    Stack(Direction.D8);
                //}
            }

            Go();
        }

        override public void PlaySkill()
        {
            // 1. バーチカルウェブ 
            // 2. スティンガー
            // 3. ストラッシュ
            // 4. リボルバースティング
            
            /* 順番を組み替えやすいように、あえて else if を使っていない */

            if(Battle.mapCode == 21448090)
            {
                // スティンガー
                if(sg.ap >= 8 && CountTrue(cube.NPC8, cube.NPC88) >= 2)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S2);
                    SelectTarget(Direction.D8, Direction.D8);
                    Go();
                    return;
                }

                // ストラッシュ
                if(sg.ap >= 12 && CountTrue(cube.NPC8, cube.NPC86, cube.NPC84) >= 2)
                {
                    sg.ap -= 12;
                    SelectSkill(SkillOrder.S3);
                    SelectTarget(Direction.D5);
                    Go();
                    return;
                }

                // バーチカルウェブ
                if(sg.ap >= 6 && (cube.NPC8 || cube.NPC4 || cube.NPC6 || cube.NPC86 || cube.NPC84 || cube.NPC88))
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC88)        SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC86)   SelectTarget(Direction.D8, Direction.D6);
                    else if(cube.NPC84)   SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC6)    SelectTarget(Direction.D6);
                    else if(cube.NPC4)    SelectTarget(Direction.D4);
                    else if(cube.NPC8)    SelectTarget(Direction.D8);
                    
                    Go();
                    return;
                }
            }
            else if(Battle.mapCode == 5409959)
            {
                // スティンガー
                if(sg.ap >= 8 && CountTrue(cube.NPC4, cube.NPC44) >= 2)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S2);
                    SelectTarget(Direction.D4, Direction.D4);
                    Go();
                    return;
                }

                // バーチカルウェブ
                if(sg.ap >= 6 && (cube.NPC8 || cube.NPC4 || cube.NPC84 || cube.NPC44))
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC44) SelectTarget(Direction.D4, Direction.D4);
                    else if(cube.NPC84) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC4) SelectTarget(Direction.D4);
                    else if(cube.NPC8) SelectTarget(Direction.D8);

                    Go();
                    return;
                }
            }
            else if(Battle.IsGrandron())
            {
                // リボルバースティング
                if(sg.ap >= 25 && cube.NPC8888)
                {
                    sg.ap -= 25;
                    SelectSkill(SkillOrder.S4);
                    SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8);
                    Go();
                    return;
                }

                // スティンガー
                if(sg.ap >= 8 && CountTrue(cube.NPC8, cube.NPC88, cube.NPC888) >= 2)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S2);
                    if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC8) SelectTarget(Direction.D8);
                    Go();
                    return;
                }

                // ストラッシュ
                if(sg.ap >= 12 && CountTrue(cube.NPC8, cube.NPC86, cube.NPC84) >= 2)
                {
                    sg.ap -= 12;
                    SelectSkill(SkillOrder.S3);
                    SelectTarget(Direction.D5);
                    Go();
                    return;
                }

                // スワローカット
                if(sg.ap >= 12 && (cube.NPC4 || cube.NPC84 || cube.NPC8 || cube.NPC86 || cube.NPC6))
                {
                    sg.ap -= 12;
                    SelectSkill(SkillOrder.S6);

                    if(cube.NPC4) SelectTarget(Direction.D4);
                    else if(cube.NPC84) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC8) SelectTarget(Direction.D8);
                    else if(cube.NPC86) SelectTarget(Direction.D8, Direction.D6);
                    else if(cube.NPC6) SelectTarget(Direction.D6);

                    Go();
                    return;
                }

                // バーチカルウェブ
                if(sg.ap >= 6 && (cube.NPC8 || cube.NPC4 || cube.NPC6 || cube.NPC86 || cube.NPC84 || cube.NPC88))
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC86) SelectTarget(Direction.D8, Direction.D6);
                    else if(cube.NPC84) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC6) SelectTarget(Direction.D6);
                    else if(cube.NPC4) SelectTarget(Direction.D4);
                    else if(cube.NPC8) SelectTarget(Direction.D8);

                    Go();
                    return;
                }

                // スティンガー
                if(sg.ap >= 8 && CountTrue(cube.NPC8, cube.NPC88, cube.NPC888) >= 1)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S2);
                    if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC8) SelectTarget(Direction.D8);
                    Go();
                    return;
                }
            }
            else if(Battle.IsBattleArena())
            {
                // リボルバースティング
                if(sg.ap >= 25 && cube.NPC8888)
                {
                    sg.ap -= 25;
                    SelectSkill(SkillOrder.S4);
                    SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8);
                    Go();
                    return;
                }

                // スティンガー
                if(sg.ap >= 8 && CountTrue(cube.NPC8, cube.NPC88, cube.NPC888) >= 2)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S2);
                    if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC8) SelectTarget(Direction.D8);
                    Go();
                    return;
                }

                // ストラッシュ
                if(sg.ap >= 12 && CountTrue(cube.NPC8, cube.NPC86, cube.NPC84) >= 2)
                {
                    sg.ap -= 12;
                    SelectSkill(SkillOrder.S3);
                    SelectTarget(Direction.D5);
                    Go();
                    return;
                }

                // スワローカット
                if(sg.ap >= 12 && (cube.NPC4 || cube.NPC84 || cube.NPC8 || cube.NPC86 || cube.NPC6))
                {
                    sg.ap -= 12;
                    SelectSkill(SkillOrder.S6);

                    if(cube.NPC4) SelectTarget(Direction.D4);
                    else if(cube.NPC84) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC8) SelectTarget(Direction.D8);
                    else if(cube.NPC86) SelectTarget(Direction.D8, Direction.D6);
                    else if(cube.NPC6) SelectTarget(Direction.D6);

                    Go();
                    return;
                }

                // バーチカルウェブ
                if(sg.ap >= 6 && (cube.NPC8 || cube.NPC4 || cube.NPC6 || cube.NPC86 || cube.NPC84 || cube.NPC88))
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC86) SelectTarget(Direction.D8, Direction.D6);
                    else if(cube.NPC84) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC6) SelectTarget(Direction.D6);
                    else if(cube.NPC4) SelectTarget(Direction.D4);
                    else if(cube.NPC8) SelectTarget(Direction.D8);

                    Go();
                    return;
                }

                // スティンガー
                if(sg.ap >= 8 && CountTrue(cube.NPC8, cube.NPC88, cube.NPC888) >= 1)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S2);
                    if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC8) SelectTarget(Direction.D8);
                    Go();
                    return;
                }
            }

            Go();
        }
    }
}
