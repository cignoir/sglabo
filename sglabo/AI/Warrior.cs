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
        override public void PlayMove(BattleField bf, SGWindow sg)
        {
            if(Battle.turn == 1)
            {
                Ready();
                Move(Direction.D8);
                Move(Direction.D8);
                Move(Direction.D8);
            }

            Go();
        }

        override public void PlaySkill(BattleField bf, SGWindow sg)
        {
            // 1. バーチカルウェブ 
            // 2. サイドウェブ
            // 3. スティンガー
            // 4. ストラッシュ
            // 5. クレセント
            // 6. リボルバースティング
            
            bool exists8 = bf.Cell(sg.gPos.x, sg.gPos.y - 1).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 1).existsPC;
            bool exists88 = bf.Cell(sg.gPos.x, sg.gPos.y - 2).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 2).existsPC;
            bool exists888 = bf.Cell(sg.gPos.x, sg.gPos.y - 3).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 3).existsPC;
            bool exists8888 = bf.Cell(sg.gPos.x, sg.gPos.y - 4).existsNPC && !bf.Cell(sg.gPos.x, sg.gPos.y - 4).existsPC;
            bool exists4 = bf.Cell(sg.gPos.x - 1, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x - 1, sg.gPos.y).existsPC;
            bool exists44 = bf.Cell(sg.gPos.x - 2, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x - 2, sg.gPos.y).existsPC;
            bool exists6 = bf.Cell(sg.gPos.x + 1, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x + 1, sg.gPos.y).existsPC;
            bool exists66 = bf.Cell(sg.gPos.x + 2, sg.gPos.y).existsNPC && !bf.Cell(sg.gPos.x + 2, sg.gPos.y).existsPC;
            bool exists86 = bf.Cell(sg.gPos.x + 1, sg.gPos.y - 1).existsNPC && !bf.Cell(sg.gPos.x + 1, sg.gPos.y - 1).existsPC;
            bool exists886 = bf.Cell(sg.gPos.x + 1, sg.gPos.y - 2).existsNPC && !bf.Cell(sg.gPos.x + 1, sg.gPos.y - 2).existsPC;
            bool exists84 = bf.Cell(sg.gPos.x - 1, sg.gPos.y - 1).existsNPC && !bf.Cell(sg.gPos.x - 1, sg.gPos.y - 1).existsPC;
            bool exists884 = bf.Cell(sg.gPos.x - 1, sg.gPos.y - 2).existsNPC && !bf.Cell(sg.gPos.x - 1, sg.gPos.y - 2).existsPC;

            /* 順番を組み替えやすいように、あえて else if を使っていない */

            if(sg.ap >= 8 && ((exists8 && exists88) || (exists88 && exists888) || (exists8 && exists888)))
            {
                sg.ap -= 8;
                SelectSkill(SkillOrder.S3);
                Go();
                return;
            }

            if(sg.ap >= 12 && ((exists8 && exists86) || (exists8 && exists84) || (exists84 && exists86)))
            {
                sg.ap -= 12;
                SelectSkill(SkillOrder.S4);
                Go();
                return;
            }

            if(sg.ap >= 3 && (exists8 || exists4 || exists6 || exists66 || exists44 || exists86 || exists84))
            {
                sg.ap -= 3;
                SelectSkill(SkillOrder.S2);
                Go();
                return;
            }

            if(sg.ap >= 6 && (exists8 || exists4 || exists6 || exists66 || exists44 || exists86 || exists84 || exists88))
            {
                sg.ap -= 6;
                SelectSkill(SkillOrder.S1);
                Go();
                return;
            }
        }
    }
}
