using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;

namespace sglabo.AI
{
    class Thief: JobAI
    {
        public Thief()
        {

        }

        public Thief(SGWindow sg, BattleCube cube)
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
                            Move(Direction.D8, Direction.D8, Direction.D8);
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
                            Move(Direction.D8, Direction.D8, Direction.D8);
                            Look(Direction.D8, true);
                        }
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
                            Move(Direction.D8, Direction.D8);
                            Look(Direction.D8);
                            break;
                        case 9346174: // グランドロンA
                            Move(Direction.D8, Direction.D8);
                            Look(Direction.D8);
                            break;
                        case 266499533: // グランドロンB
                            Move(Direction.D8, Direction.D8);
                            Look(Direction.D8);
                            break;
                        case 209593538: // グランドロンC
                            Move(Direction.D8, Direction.D8);
                            Look(Direction.D8);
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
            // 1. ダイアゴナル 
            // 2. ライトニングディルク

            /* 順番を組み替えやすいように、あえて else if を使っていない */
            if(Battle.mapCode == 21448090 || Battle.mapCode == 5409959)
            {
                // ライトニングディルク
                if(sg.ap >= 9 && (cube.NPC8 || cube.NPC4 || cube.NPC6))
                {
                    sg.ap -= 9;
                    SelectSkill(SkillOrder.S2);

                    if(cube.NPC6) SelectTarget(Direction.D6);
                    else if(cube.NPC4) SelectTarget(Direction.D4);
                    else if(cube.NPC8) SelectTarget(Direction.D8);

                    Go();
                    return;
                }

                // ダイアゴナル
                if(sg.ap >= 4 && (cube.NPC8 || cube.NPC86 || cube.NPC84 || cube.NPC88))
                {
                    sg.ap -= 4;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC8) SelectTarget(Direction.D8);
                    else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC84) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC86) SelectTarget(Direction.D8, Direction.D6);

                    Go();
                    return;
                }

            }
            else if(Battle.IsGrandron())
            {
                // ライトニングディルク
                if(sg.ap >= 9 && (cube.NPC8 || cube.NPC4 || cube.NPC6))
                {
                    sg.ap -= 9;
                    SelectSkill(SkillOrder.S2);

                    if(cube.NPC6) SelectTarget(Direction.D6);
                    else if(cube.NPC4) SelectTarget(Direction.D4);
                    else if(cube.NPC8) SelectTarget(Direction.D8);

                    Go();
                    return;
                }

                // ダイアゴナル
                if(sg.ap >= 4 && (cube.NPC8 || cube.NPC86 || cube.NPC84 || cube.NPC88))
                {
                    sg.ap -= 4;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC8) SelectTarget(Direction.D8);
                    else if(cube.NPC84) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC86) SelectTarget(Direction.D8, Direction.D6);

                    Go();
                    return;
                }
            } 
            else if(Battle.IsBattleArena())
            {
                // 影弓
                // 卍
                // ボディビート
                // ロングレンジ
                // リーチ
                if(sg.ap >= 8 && (cube.NPC8888 || cube.NPC888 || cube.NPC8886 || cube.NPC8884 || cube.NPC884 || cube.NPC886))
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC8888) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC8884) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC8886) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC884) SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC886) SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);

                    Go();
                    return;
                }

                if(sg.ap >= 6 && (cube.NPC8888 || cube.NPC88884 || cube.NPC888844 || cube.NPC88886 || cube.NPC888866
                    || cube.NPC888 || cube.NPC884 || cube.NPC88844 || cube.NPC8886 || cube.NPC88866
                    || cube.NPC884 || cube.NPC886 || cube.NPC88 || cube.NPC8))
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S2);

                    if(cube.NPC8888) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC88884) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC88886) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC888844) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8, Direction.D4, Direction.D4);
                    else if(cube.NPC888866) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8, Direction.D6, Direction.D6);
                    else if(cube.NPC88844) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4, Direction.D4);
                    else if(cube.NPC88866) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6, Direction.D6);
                    else if(cube.NPC8884) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC8886) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC888) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC884) SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC886) SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC88) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC8) SelectTarget(Direction.D8);

                    Go();
                    return;
                }

                if(sg.ap >= 3 && (cube.NPC84 || cube.NPC86))
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S3);

                    if(cube.NPC86) SelectTarget(Direction.D8, Direction.D6);
                    else if(cube.NPC84) SelectTarget(Direction.D8, Direction.D4);

                    Go();
                    return;
                }
            }

            Go();
        }
    }
}
