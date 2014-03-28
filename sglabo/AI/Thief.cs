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
                && (direction == Direction.D8 && (cube.NPC8() || cube.NPC86() || cube.NPC84() || cube.NPC88())
                    || direction == Direction.D4 && (cube.NPC4() || cube.NPC44() || cube.NPC84())
                    || direction == Direction.D6 && (cube.NPC6() || cube.NPC66() || cube.NPC86())
                   )
                )
            {
                sg.ap -= 4;
                SelectSkill(SkillOrder.S1);
                SelectTarget();
                Go();
                return;
            }

            if(sg.ap >= 9 
                && (direction == Direction.D8 && (cube.NPC8() || cube.NPC4() || cube.NPC6())
                    || direction == Direction.D6 && (cube.NPC8() || cube.NPC6())
                    || direction == Direction.D4 && (cube.NPC8() || cube.NPC4())
                   )
                )
            {
                sg.ap -= 9;
                SelectSkill(SkillOrder.S2);
                SelectTarget();
                Go();
                return;
            }


            Go();
        }
    }
}
