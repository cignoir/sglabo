using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    class BattleField
    {
        ScreenPosition N;
        ScreenPosition E;
        ScreenPosition W;
        ScreenPosition S;

        BattleFieldCell[,] cells;

        public int turn = 1;
        public int pcCount = 0;
        public int enemyCount = 0;

        public int rowCount { get; set; }
        public int colCount { get; set; }

        public BattleField(string map)
        {
            Load(map);
        }

        public void Load(string map){
            var lines = map.Split('\n');
            foreach(string line in lines)
            {
                if(line.Contains("x"))
                {
                    var elems = line.Split('x');
                    rowCount = int.Parse(elems[0]);
                    colCount = int.Parse(elems[1]);
                }
                else
                {
                    var elems = line.Split('\t');
                    if(elems.Length == 6)
                    {
                        var gPos = new GridPosition(int.Parse(elems[0]), int.Parse(elems[1]));
                        var sPos = new ScreenPosition(int.Parse(elems[3]), int.Parse(elems[4]));
                        var height = int.Parse(elems[2]);
                        var canMove = int.Parse(elems[5]) == 1;
                        var cell = new BattleFieldCell(gPos, sPos, height, canMove);
                        cells[gPos.x, gPos.y] = cell;
                    }
                }

            }
        }

        public void Scan()
        {
            for(int row = 0; row < rowCount; row++ )
            {
                for(int col = 0; col < colCount; col++ )
                {
                    cells[row, col].Scan();
                }
            }
        }
    }
}
