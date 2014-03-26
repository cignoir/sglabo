using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;

namespace sglabo.AI
{
    class SpiritMage: JobAI
    {
        public SpiritMage()
        {

        }

        public SpiritMage(SGWindow sg, BattleCube cube)
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
                        Look(Direction.D8);
                        break;
                    case 5409959:
                        // ナビアB
                        Move(Direction.D8);
                        Move(Direction.D8);
                        Look(Direction.D8);
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
            // 1. リトルフォース
            // 2. アクアボール
            // 3. ウィンドエッジ
            // 4. アクアサービスボール
            // 5. リーフブレード

            /* 順番を組み替えやすいように、あえて else if を使っていない */

            //int cnt = 0;
            //cnt += cube.Exists8() ? 1 : 0;
            //cnt += cube.Exists88() ? 1 : 0;
            //cnt += cube.Exists888() ? 1 : 0;
            //cnt += cube.Exists886() ? 1 : 0;
            //cnt += cube.Exists884() ? 1 : 0;
            //if(sg.ap >= 3 && direction == Direction.D8 && cnt >= 2)
            //{
            //    sg.ap -= 3;
            //    SelectSkill(SkillOrder.S1, Direction.D8);
            //    Go();
            //    return;
            //}

            if(sg.ap >= 6 && direction == Direction.D8
                && (cube.Exists88() || cube.Exists888() || cube.Exists8888() || cube.Exists8886() || cube.Exists8884() || cube.Exists886() || cube.Exists884() || cube.Exists86() || cube.Exists84()))
            {
                sg.ap -= 6;
                SelectSkill(SkillOrder.S2);
                Go();
                return;
            }

            Go();
        }
    }
}
