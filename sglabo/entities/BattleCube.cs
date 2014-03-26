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
        BattleCubeCell[,,] cells = new BattleCubeCell[5, 7, 5];
        
        public const int SCAN_RANGE_X_MIN = 1;
        public const int SCAN_RANGE_X_MAX = 3;

        public const int SCAN_RANGE_Y_MIN = 1;
        public const int SCAN_RANGE_Y_MAX = 4;

        public const int SCAN_RANGE_Z_MIN = 1;
        public const int SCAN_RANGE_Z_MAX = 3;

        public const int CORE_X = 2;
        public const int CORE_Y = 4;
        public const int CORE_Z = 2;

        public BattleCube(ScreenPosition core)
        {
            cells = new BattleCubeCell[5, 7, 5];

            cells[0, 4, 2].sPos = new ScreenPosition(core.x - 63 * 2, core.y - 32 * 2);
            cells[1, 4, 2].sPos = new ScreenPosition(core.x - 63 * 1, core.y - 32 * 1);
            cells[2, 4, 2].sPos = core;
            cells[3, 4, 2].sPos = new ScreenPosition(core.x + 63 * 1, core.y + 32 * 1);
            cells[4, 4, 2].sPos = new ScreenPosition(core.x + 63 * 2, core.y + 32 * 2);

            for(int y = 3; y >= 0; y--)
            {
                for(int x = 0; x < 5; x++)
                {
                    cells[x, y, 2].sPos = new ScreenPosition(cells[x, y + 1, 2].sPos.x + 55, cells[x, y + 1, 2].sPos.y - 40);
                }
            }

            for(int y = 5; y <= 6; y++)
            {
                for(int x = 0; x < 5; x++)
                {
                    cells[x, y, 2].sPos = new ScreenPosition(cells[x, y - 1, 2].sPos.x - 55, cells[x, y - 1, 2].sPos.y + 40);
                }
            }

            for(int x = 0; x < 5; x++)
            {
                for(int y = 0; y < 7; y++)
                {
                    cells[x, y, 1].sPos.x = cells[x, y, 2].sPos.x;
                    cells[x, y, 1].sPos.y = cells[x, y, 2].sPos.y - 34;

                    cells[x, y, 0].sPos.x = cells[x, y, 1].sPos.x;
                    cells[x, y, 0].sPos.y = cells[x, y, 1].sPos.y - 34;

                    cells[x, y, 3].sPos.x = cells[x, y, 2].sPos.x;
                    cells[x, y, 3].sPos.y = cells[x, y, 2].sPos.y + 34;

                    cells[x, y, 4].sPos.x = cells[x, y, 3].sPos.x;
                    cells[x, y, 4].sPos.y = cells[x, y, 3].sPos.y + 34;
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

            if(!result)
            {
                for(int z = SCAN_RANGE_Z_MIN; z <= SCAN_RANGE_Z_MAX; z++)
                {
                    if(z == CORE_Z) continue;
                    result = cells[CORE_X + diffX, CORE_Y + diffY, z].existsNPC;
                    if(result) break;
                }
            }

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

        public bool Exists4()
        {
            return Exists(-1, 0);
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
