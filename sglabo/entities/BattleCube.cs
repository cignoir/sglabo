using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sglabo.entities
{
    class BattleCube
    {
        public ScreenPosition core = new ScreenPosition(400, 320);
        public BattleCubeCell[,,] cells = new BattleCubeCell[5, 5, 3];
        
        public const int SCAN_RANGE_X_MIN = 0;
        public const int SCAN_RANGE_X_MAX = 4;

        public const int SCAN_RANGE_Y_MIN = 0;
        public const int SCAN_RANGE_Y_MAX = 4;

        public const int SCAN_RANGE_Z_MIN = 0;
        public const int SCAN_RANGE_Z_MAX = 2;

        public const int CORE_X = 2;
        public const int CORE_Y = 4;
        public const int CORE_Z = 1;

        public BattleCube()
        {
            cells = new BattleCubeCell[5, 5, 3];
            for(int x = 0; x < 5; x++)
            {
                for(int y = 0; y < 5; y++)
                {
                    for(int z = 0; z < 3; z++ )
                    {
                        cells[x, y, z] = new BattleCubeCell(new GridPosition(x, y, z));
                    }
                }
            }

            cells[0, 0, 1].sPos = new ScreenPosition(466, 141);
            cells[1, 0, 1].sPos = new ScreenPosition(528, 161);
            cells[2, 0, 1].sPos = new ScreenPosition(583, 189);
            cells[3, 0, 1].sPos = new ScreenPosition(648, 209);
            cells[4, 0, 1].sPos = new ScreenPosition(710, 236);

            cells[0, 1, 1].sPos = new ScreenPosition(426, 166);
            cells[1, 1, 1].sPos = new ScreenPosition(484, 191);
            cells[2, 1, 1].sPos = new ScreenPosition(539, 219);
            cells[3, 1, 1].sPos = new ScreenPosition(604, 242);
            cells[4, 1, 1].sPos = new ScreenPosition(667, 268);

            cells[0, 2, 1].sPos = new ScreenPosition(377, 200);
            cells[1, 2, 1].sPos = new ScreenPosition(435, 225);
            cells[2, 2, 1].sPos = new ScreenPosition(493, 252);
            cells[3, 2, 1].sPos = new ScreenPosition(557, 278);
            cells[4, 2, 1].sPos = new ScreenPosition(620, 305);

            cells[0, 3, 1].sPos = new ScreenPosition(329, 231);
            cells[1, 3, 1].sPos = new ScreenPosition(389, 259);
            cells[2, 3, 1].sPos = new ScreenPosition(449, 284);
            cells[3, 3, 1].sPos = new ScreenPosition(510, 313);
            cells[4, 3, 1].sPos = new ScreenPosition(574, 341);

            cells[0, 4, 1].sPos = new ScreenPosition(281, 264);
            cells[1, 4, 1].sPos = new ScreenPosition(340, 291);
            cells[2, 4, 1].sPos = new ScreenPosition(400, 320);
            cells[3, 4, 1].sPos = new ScreenPosition(463, 348);
            cells[4, 4, 1].sPos = new ScreenPosition(526, 377);

            for(int x = 0; x <= 4; x++)
            {
                for(int y = 0; y <= 4; y++)
                {
                    cells[x, y, 0].sPos = new ScreenPosition(cells[x, y, 1].sPos.x, cells[x, y, 1].sPos.y - 30);
                    cells[x, y, 2].sPos = new ScreenPosition(cells[x, y, 1].sPos.x, cells[x, y, 1].sPos.y + 30);
                }
            }
        }

        public BattleCube Scan()
        {
            for(int x = SCAN_RANGE_X_MIN; x <= SCAN_RANGE_X_MAX; x++)
            {
                for(int y = SCAN_RANGE_Y_MIN; y <= SCAN_RANGE_Y_MAX; y++)
                {
                    for(int z = SCAN_RANGE_Z_MIN; z <= SCAN_RANGE_Z_MAX; z++)
                    {
                        cells[x, y, z].Scan();
                    }
                }
            }

            return this;
        }

        public bool Exists(int diffX, int diffY){
            bool result = false;

            result = cells[CORE_X + diffX, CORE_Y + diffY, CORE_Z].existsNPC;

            //if(!result)
            //{
            //    for(int z = SCAN_RANGE_Z_MIN; z <= SCAN_RANGE_Z_MAX; z++)
            //    {
            //        if(z == CORE_Z) continue;
            //        result = cells[CORE_X + diffX, CORE_Y + diffY, z].existsNPC;
            //        if(result) break;
            //    }
            //}

            return result;
        }

        public bool Exists8()
        {
            return Exists(0, -1);
        }

        public bool Exists88()
        {
            return Exists(0, -2);
        }

        public bool Exists888()
        {
            return Exists(0, -3);
        }

        public bool Exists8888()
        {
            return Exists(0, -4);
        }

        public bool Exists6()
        {
            return Exists(1, 0);
        }

        public bool Exists66()
        {
            return Exists(2, 0);
        }

        public bool Exists4()
        {
            return Exists(-1, 0);
        }

        public bool Exists44()
        {
            return Exists(-2, 0);
        }

        public bool Exists86()
        {
            return Exists(1, -1);
        }

        public bool Exists84()
        {
            return Exists(-1, -1);
        }

        public bool Exists886()
        {
            return Exists(1, -2);
        }

        public bool Exists884()
        {
            return Exists(-1, -2);
        }

        public bool Exists8886()
        {
            return Exists(1, -3);
        }

        public bool Exists8884()
        {
            return Exists(-1, -3);
        }
       
    }
}
