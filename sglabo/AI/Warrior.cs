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

            if(direction == Direction.D8)
            {
                // スティンガー
                if(sg.ap >= 8 && CountTrue(cube.NPC8(), cube.NPC88()) >= 2)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S3);
                    SelectTarget(Direction.D8, Direction.D8);
                    Go();
                    return;
                }

                // ストラッシュ
                if(sg.ap >= 12 && CountTrue(cube.NPC8(), cube.NPC86(), cube.NPC84()) >= 2)
                {
                    sg.ap -= 12;
                    SelectSkill(SkillOrder.S4);
                    SelectTarget();
                    Go();
                    return;
                }

                // バーチカルウェブ
                if(sg.ap >= 6 && (cube.NPC8() || cube.NPC4() || cube.NPC6() || cube.NPC86() || cube.NPC84() || cube.NPC88()))
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC88())        SelectTarget(Direction.D8, Direction.D8);
                    else if(cube.NPC86())   SelectTarget(Direction.D8, Direction.D6);
                    else if(cube.NPC84())   SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC6())    SelectTarget(Direction.D6);
                    else if(cube.NPC4())    SelectTarget(Direction.D4);
                    else if(cube.NPC8())    SelectTarget(Direction.D8);
                    
                    Go();
                    return;
                }

                // サイドウェブ
                if(sg.ap >= 3 && (cube.NPC8() || cube.NPC4() || cube.NPC6() || cube.NPC86() || cube.NPC84() || cube.NPC44() || cube.NPC66()))
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S2);

                    if(cube.NPC66())        SelectTarget(Direction.D6, Direction.D6);
                    else if(cube.NPC44())   SelectTarget(Direction.D4, Direction.D4);
                    else if(cube.NPC86())   SelectTarget(Direction.D8, Direction.D6);
                    else if(cube.NPC84())   SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC6())    SelectTarget(Direction.D6);
                    else if(cube.NPC4())    SelectTarget(Direction.D4);
                    else if(cube.NPC8())    SelectTarget(Direction.D8);

                    Go();
                    return;
                }
            }
            else if(direction == Direction.D4)
            {
                // スティンガー
                if(sg.ap >= 8 && CountTrue(cube.NPC4(), cube.NPC44()) >= 2)
                {
                    sg.ap -= 8;
                    SelectSkill(SkillOrder.S3);
                    SelectTarget(Direction.D4, Direction.D4);
                    Go();
                    return;
                }

                // バーチカルウェブ
                if(sg.ap >= 6 && (cube.NPC8() || cube.NPC4() || cube.NPC84() || cube.NPC44()))
                {
                    sg.ap -= 6;
                    SelectSkill(SkillOrder.S1);

                    if(cube.NPC44()) SelectTarget(Direction.D4, Direction.D4);
                    else if(cube.NPC84()) SelectTarget(Direction.D8, Direction.D4);
                    else if(cube.NPC4()) SelectTarget(Direction.D4);
                    else if(cube.NPC8()) SelectTarget(Direction.D8);

                    Go();
                    return;
                }

                // サイドウェブ
                if(sg.ap >= 3 && (cube.NPC8() || cube.NPC4() || cube.NPC84()))
                {
                    sg.ap -= 3;
                    SelectSkill(SkillOrder.S2);

                    if(cube.NPC84()) SelectTarget(Direction.D8, Direction.D4);
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
