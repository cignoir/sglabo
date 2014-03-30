﻿using System;
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
            if(Battle.turn == 1)
            {
                Ready();

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
                        TopView();
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
            // 1. リトルフォース
            // 2. アクアボール
            // 3. ウィンドエッジ
            // 4. ロックミサイル
            // 5. リーフブレード

            /* 順番を組み替えやすいように、あえて else if を使っていない */

            if(direction == Direction.D8)
            {
                // リーフブレード888
                if(sg.ap >= 24 && cube.NPC888() && CountTrue(cube.NPC888(), cube.NPC8886(), cube.NPC8884(), cube.NPC8888(), cube.NPC88()) >= 2)
                {
                    sg.ap -= 24;
                    SelectSkill(SkillOrder.S5);
                    SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    Go();
                    return;
                }

                //// リトルフォース84
                if(Battle.mapCode == 5409959 && sg.ap >= 3 && CountTrue(cube.NPC8(), cube.NPC4(), cube.NPC84(), cube.NPC884(), cube.NPC844()) >= 2)
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S1);
                    SelectTarget(Direction.D8, Direction.D4);
                    Go();
                    return;
                }
                //// リトルフォース86
                //if(sg.ap >= 3 && CountTrue(cube.NPC8(), cube.NPC6(), cube.NPC86(), cube.NPC886(), cube.NPC866()) >= 2)
                //{
                //    sg.ap -= 3;
                //    SelectSkill(SkillOrder.S1);
                //    SelectTarget(Direction.D8, Direction.D6);
                //    Go();
                //    return;
                //}

                // リトルフォース88
                if(sg.ap >= 3 && CountTrue(cube.NPC8(), cube.NPC88(), cube.NPC888(), cube.NPC886(), cube.NPC884()) >= 2)
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S1);
                    SelectTarget(Direction.D8, Direction.D8);
                    Go();
                    return;
                }

                if(sg.ap >= 6 && (cube.NPC8888() || cube.NPC888() || cube.NPC88() || cube.NPC86() || cube.NPC84() || cube.NPC886() || cube.NPC884() || cube.NPC8884() || cube.NPC8886()))
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S2);

                    if(cube.NPC8884()) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC8886()) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC8888()) SelectTarget(Direction.D8, Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC884()) SelectTarget(Direction.D8, Direction.D8, Direction.D4);
                    else if(cube.NPC886()) SelectTarget(Direction.D8, Direction.D8, Direction.D6);
                    else if(cube.NPC888()) SelectTarget(Direction.D8, Direction.D8, Direction.D8);
                    else if(cube.NPC84()) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC86()) SelectTarget(Direction.D8, Direction.D6);
                    else if(cube.NPC88()) SelectTarget(Direction.D8, Direction.D8);

                    Go();
                    return;
                }

                if(sg.ap >= 3 && (cube.NPC888() || cube.NPC88() || cube.NPC8() || cube.NPC886() || cube.NPC884()))
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S1);
                    SelectTarget(Direction.D8, Direction.D8);
                    Go();
                    return;
                }

                if(Battle.mapCode == 5409959 && sg.ap >= 3 && (cube.NPC84() || cube.NPC884() || cube.NPC844() || cube.NPC8()))
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S1);
                    SelectTarget(Direction.D8, Direction.D4);
                    Go();
                    return;
                }

                if(sg.ap >= 18 && (cube.NPC844() || cube.NPC8844() || cube.NPC866() || cube.NPC8866()))
                {
                    sg.ap -= 18;
                    SelectSkill(SkillOrder.S4);
                    if(cube.NPC844()) SelectTarget(Direction.D8, Direction.D4, Direction.D4);
                    else if(cube.NPC8844()) SelectTarget(Direction.D8, Direction.D8, Direction.D4, Direction.D4);
                    else if(cube.NPC866()) SelectTarget(Direction.D8, Direction.D6, Direction.D6);
                    else if(cube.NPC8866()) SelectTarget(Direction.D8, Direction.D8, Direction.D6, Direction.D6);
                    Go();
                    return;
                }
            }

            Go();
        }
    }
}
