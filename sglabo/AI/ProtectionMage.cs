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
            Ready();

            if(Battle.turn == 1)
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
                        Move(Direction.D6);
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
            if(Battle.turn % 2 == 1)
            {
                if(sg.ap >= 8)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S1);
                    SelectTarget(Direction.D8);
                    Go();
                    return;
                }
            }
            else
            {
                if(sg.ap >= 16)
                {
                    sg.ap -= 16;
                    SelectSkill(SkillOrder.S10);
                    if(cube.NPC888()) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC8884()) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC8886()) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC88844()) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4, Direction.D4);
                    else if(cube.NPC88866()) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6, Direction.D6);
                    else if(cube.NPC88()) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC884()) SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC8844()) SelectTarget(Direction.D8, Direction.D8, Direction.D4, Direction.D4);
                    else if(cube.NPC8866()) SelectTarget(Direction.D8, Direction.D8, Direction.D6, Direction.D6);
                    Go();
                    return;
                }

                if(sg.ap >= 3 && cube.NPC88())
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S3);
                    SelectTarget(Direction.D8, Direction.D8);
                    Go();
                    return;
                }
            }

            Go();
        }
    }
}
