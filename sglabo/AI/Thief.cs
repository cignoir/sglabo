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
            Ready();

            if(Battle.turn == 1)
            {
                switch(Battle.mapCode)
                {
                    case 21448090:
                        // ナビアA
                        Move(Direction.D8);
                        Move(Direction.D8);
                        Move(Direction.D8);
                        Look(Direction.D6);
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
            // 1. ダイアゴナル 
            // 2. ライトニングディルク

            /* 順番を組み替えやすいように、あえて else if を使っていない */

            if(sg.ap >= 4 
                && (direction == Direction.D8 && (cube.Exists8() || cube.Exists4() || cube.Exists6() || cube.Exists86() || cube.Exists84() || cube.Exists88()))
              )
            {
                sg.ap -= 6;
                SelectSkill(SkillOrder.S1);
                Go();
                return;
            }

            if(sg.ap >= 3 && (cube.Exists8() || cube.Exists4() || cube.Exists6() || cube.Exists86() || cube.Exists84()))
            {
                sg.ap -= 3;
                SelectSkill(SkillOrder.S2);
                Go();
                return;
            }


            Go();
        }
    }
}
