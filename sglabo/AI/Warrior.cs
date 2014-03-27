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
            Ready();

            if(Battle.turn == 1)
            {
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
                        Move(Direction.D8, false);
                        Look(Direction.D4);
                        break;
                    default:
                        // test
                        Move(Direction.D4);
                        Move(Direction.D2);
                        Look(Direction.D8);
                        break;
                }
            }
            else
            {
                if(ShouldStack(Direction.D8))
                {
                    Stack(Direction.D8);
                }
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

            if(direction == Direction.D8)
            {
                if(sg.ap >= 8 && ((cube.Exists8() && cube.Exists88()) || (cube.Exists88() && cube.Exists888()) || (cube.Exists8() && cube.Exists888())))
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S3);
                    SelectTarget();
                    Go();
                    return;
                }

                if(sg.ap >= 12 && ((cube.Exists8() && cube.Exists86()) || (cube.Exists8() && cube.Exists84()) || (cube.Exists84() && cube.Exists86())))
                {
                    sg.ap -= 12;
                    SelectSkill(SkillOrder.S4);
                    SelectTarget();
                    Go();
                    return;
                }

                if(sg.ap >= 6 && (cube.Exists8() || cube.Exists4() || cube.Exists6() || cube.Exists86() || cube.Exists84() || cube.Exists88()))
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S1);
                    SelectTarget();
                    Go();
                    return;
                }

                if(sg.ap >= 3 && (cube.Exists8() || cube.Exists4() || cube.Exists6() || cube.Exists86() || cube.Exists84()))
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S2);
                    SelectTarget();
                    Go();
                    return;
                }


            }
            else if(direction == Direction.D4)
            {
                if(sg.ap >= 8 && ((cube.Exists8() && cube.Exists88()) || (cube.Exists88() && cube.Exists888()) || (cube.Exists8() && cube.Exists888())))
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S3);
                    SelectTarget();
                    Go();
                    return;
                }

                if(sg.ap >= 3 && (cube.Exists8() || cube.Exists4() || cube.Exists6() || cube.Exists86() || cube.Exists84()))
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S2);
                    SelectTarget();
                    Go();
                    return;
                }

                if(sg.ap >= 6
                    && (
                        (direction == Direction.D8 && (cube.Exists8() || cube.Exists4() || cube.Exists6() || cube.Exists86() || cube.Exists84() || cube.Exists88()))
                        || (direction == Direction.D6 && (cube.Exists8() || cube.Exists6() || cube.Exists86() || cube.Exists66()))
                        || (direction == Direction.D4 && (cube.Exists8() || cube.Exists4() || cube.Exists84() || cube.Exists44()))
                        )
                  )
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S1);
                    SelectTarget();
                    Go();
                    return;
                }
            }

            Go();
        }
    }
}
