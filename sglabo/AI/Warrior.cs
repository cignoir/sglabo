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
        public GridPosition goal = new GridPosition(0, 1);

        public Warrior()
        {

        }

        public Warrior(BattleField bf, SGWindow sg)
        {
            this.bf = bf;
            this.sg = sg;
        }

        override public void SetGoal()
        {
            switch(BattleField.mapCode)
            {
                default:
                    goal = new GridPosition(1, 3);
                    break;
            }
        }

        override public void PlayMove()
        {
            Ready();

            if(ShouldBeStack(Direction.D8))
            {
                Stack(Direction.D8);
            }
            else
            {
                MoveTo(goal);
                Look(Direction.D8);
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

            if(sg.ap >= 8 && ((Exists8() && Exists88()) || (Exists88() && Exists888()) || (Exists8() && Exists888())))
            {
                sg.ap -= 8;
                SelectSkill(SkillOrder.S3);
                Go();
                return;
            }

            if(sg.ap >= 12 && ((Exists8() && Exists86()) || (Exists8() && Exists84()) || (Exists84() && Exists86())))
            {
                sg.ap -= 12;
                SelectSkill(SkillOrder.S4);
                Go();
                return;
            }

            if(sg.ap >= 3 && (Exists8() || Exists4() || Exists6() || Exists66() || Exists44() || Exists86() || Exists84()))
            {
                sg.ap -= 3;
                SelectSkill(SkillOrder.S2);
                Go();
                return;
            }

            if(sg.ap >= 6 && (Exists8() || Exists4() || Exists6() || Exists66() || Exists44() || Exists86() || Exists84() || Exists88()))
            {
                sg.ap -= 6;
                SelectSkill(SkillOrder.S1);
                Go();
                return;
            }

            Go();
        }
    }
}
