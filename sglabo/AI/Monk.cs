using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;

namespace sglabo.AI
{
    class Monk: JobAI
    {
        public Monk()
        {

        }

        public Monk(SGWindow sg, BattleCube cube)
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
                        Move(Direction.D8);
                        Move(Direction.D8);
                        Look(Direction.D8);
                        break;
                    case 5409959:
                        // ナビアB
                        Move(Direction.D8);
                        Move(Direction.D8);
                        Move(Direction.D8);
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
            // 1. バーチカルウェブ 
            // 2. サイドウェブ
            // 3. スティンガー
            // 4. ストラッシュ
            // 5. クレセント
            // 6. リボルバースティング

            /* 順番を組み替えやすいように、あえて else if を使っていない */

            //if(sg.ap >= 8 && ((cube.Exists8() && cube.Exists88()) || (cube.Exists88() && cube.Exists888()) || (cube.Exists8() && cube.Exists888())))
            //{
            //    sg.ap -= 8;
            //    SelectSkill(SkillOrder.S3);

            //    Go();
            //    return;
            //}

            //if(sg.ap >= 12 && ((cube.Exists8() && cube.Exists86()) || (cube.Exists8() && cube.Exists84()) || (cube.Exists84() && cube.Exists86())))
            //{
            //    sg.ap -= 12;
            //    SelectSkill(SkillOrder.S4);
            //    Go();
            //    return;
            //}

            //if(sg.ap >= 3 && (cube.Exists8() || cube.Exists4() || cube.Exists6() || cube.Exists86() || cube.Exists84()))
            //{
            //    sg.ap -= 3;
            //    SelectSkill(SkillOrder.S2);
            //    Go();
            //    return;
            //}

            //if(sg.ap >= 6 && (cube.Exists8() || cube.Exists4() || cube.Exists6() || cube.Exists86() || cube.Exists84() || cube.Exists88()))
            //{
            //    sg.ap -= 6;
            //    SelectSkill(SkillOrder.S1);
            //    Go();
            //    return;
            //}

            Go();
        }
    }
}
