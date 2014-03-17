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

        public int width { get; set; }
        public int depth { get; set;}
        public int height { get; set; }

        public void BuildCells()
        {
            cells = new BattleFieldCell[depth, width];
        }

    }
}
