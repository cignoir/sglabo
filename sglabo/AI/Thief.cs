﻿using System;
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
                // ダイアゴナル
                if(sg.ap >= 4 && (cube.NPC8() || cube.NPC86() || cube.NPC84() || cube.NPC88()))
                {
                    sg.ap -= 4;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC8()) SelectTarget(Direction.D8);
                    else if(cube.NPC84()) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC88()) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC86()) SelectTarget(Direction.D8, Direction.D6);

                    Go();
                    return;
                }

                // ライトニングディルク
                if(sg.ap >= 9 && (cube.NPC8() || cube.NPC4() || cube.NPC6()))
                {
                    sg.ap -= 9;
                    SelectSkill(SkillOrder.S2);

                    if(cube.NPC6()) SelectTarget(Direction.D6);
                    else if(cube.NPC4()) SelectTarget(Direction.D4);
                    else if(cube.NPC8()) SelectTarget(Direction.D8);

                    Go();
                    return;
                }
            }
            else if(Battle.IsGrandron())
            {
                // ダイアゴナル
                if(sg.ap >= 4 && (cube.NPC8() || cube.NPC86() || cube.NPC84() || cube.NPC88()))
                {
                    sg.ap -= 4;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC8()) SelectTarget(Direction.D8);
                    else if(cube.NPC84()) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC88()) SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC86()) SelectTarget(Direction.D8, Direction.D6);

                    Go();
                    return;
                }

                // ライトニングディルク
                if(sg.ap >= 9 && (cube.NPC8() || cube.NPC4() || cube.NPC6()))
                {
                    sg.ap -= 9;
                    SelectSkill(SkillOrder.S2);

                    if(cube.NPC6()) SelectTarget(Direction.D6);
                    else if(cube.NPC4()) SelectTarget(Direction.D4);
                    else if(cube.NPC8()) SelectTarget(Direction.D8);

                    Go();
                    return;
                }
            }

            Go();
        }
    }
}
