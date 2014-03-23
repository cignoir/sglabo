using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace sglabo.entities
{
    class 戦士
    {
        public static void PlaySkill(BattleField bf, SGWindow sg)
        {
            // 1. サイドウェブ
            // 2. バーチカルウェブ 
            // 3. スティンガー
            // 4. ストラッシュ
            // 5. クレセント
            // 6. リボルバースティング

            bool exists8 = bf.cells[sg.gPos.x, sg.gPos.y - 1].existsNPC && !bf.cells[sg.gPos.x, sg.gPos.y - 1].existsPC;
            bool exists88 = bf.cells[sg.gPos.x, sg.gPos.y - 2].existsNPC && !bf.cells[sg.gPos.x, sg.gPos.y - 2].existsPC;
            bool exists888 = bf.cells[sg.gPos.x, sg.gPos.y - 3].existsNPC && !bf.cells[sg.gPos.x, sg.gPos.y - 3].existsPC;
            bool exists8888 = bf.cells[sg.gPos.x, sg.gPos.y - 4].existsNPC && !bf.cells[sg.gPos.x, sg.gPos.y - 4].existsPC;
            bool exists4 = bf.cells[sg.gPos.x - 1, sg.gPos.y].existsNPC && !bf.cells[sg.gPos.x - 1, sg.gPos.y].existsPC;
            bool exists44 = bf.cells[sg.gPos.x - 2, sg.gPos.y].existsNPC && !bf.cells[sg.gPos.x - 2, sg.gPos.y].existsPC;
            bool exists6 = bf.cells[sg.gPos.x + 1, sg.gPos.y].existsNPC && !bf.cells[sg.gPos.x + 1, sg.gPos.y].existsPC;
            bool exists66 = bf.cells[sg.gPos.x + 2, sg.gPos.y].existsNPC && !bf.cells[sg.gPos.x + 2, sg.gPos.y].existsPC;
            bool exists86 = bf.cells[sg.gPos.x + 1, sg.gPos.y - 1].existsNPC && !bf.cells[sg.gPos.x + 1, sg.gPos.y - 1].existsPC;
            bool exists886 = bf.cells[sg.gPos.x + 1, sg.gPos.y - 2].existsNPC && !bf.cells[sg.gPos.x + 1, sg.gPos.y - 2].existsPC;
            bool exists84 = bf.cells[sg.gPos.x - 1, sg.gPos.y - 1].existsNPC && !bf.cells[sg.gPos.x - 1, sg.gPos.y - 1].existsPC;
            bool exists884 = bf.cells[sg.gPos.x - 1, sg.gPos.y - 2].existsNPC && !bf.cells[sg.gPos.x - 1, sg.gPos.y - 2].existsPC;


            if(
                exists8 || exists4 || exists6
                || exists66 || exists44 || exists86 || exists84
                )
            {
                sg.SelectSkill(1);
            }
            else if(
                exists8 || exists4 || exists6
                || exists66 || exists44 || exists86 || exists84 || exists88
                )
            {
                sg.SelectSkill(2);
            }
            else if(
                exists8 || exists4 || exists6
                || exists66 || exists44 || exists86 || exists84 || exists88
                || exists888
                )
            {
                sg.SelectSkill(3);
            }
        }
    }
}
