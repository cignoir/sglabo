﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sglabo.entities
{
    class BattleCube
    {
        public ScreenPosition core = new ScreenPosition(404, 316);
        public BattleCubeCell[,] cells = new BattleCubeCell[5, 5];
        
        public const int SCAN_RANGE_X_MIN = 0;
        public const int SCAN_RANGE_X_MAX = 4;

        public const int SCAN_RANGE_Y_MIN = 0;
        public const int SCAN_RANGE_Y_MAX = 4;

        public const int CORE_X = 2;
        public const int CORE_Y = 4;

        public BattleCube()
        {
            cells = new BattleCubeCell[5, 5];
            for(int x = 0; x < 5; x++)
            {
                for(int y = 0; y < 5; y++)
                {
                    cells[x, y] = new BattleCubeCell(new GridPosition(x, y));
                }
            }

            if(Battle.IsBattleArena())
            {
                cells[0, 0].sPos = new ScreenPosition(455, 89);
                cells[1, 0].sPos = new ScreenPosition(492, 118);
                cells[2, 0].sPos = new ScreenPosition(533, 152);
                cells[3, 0].sPos = new ScreenPosition(574, 184);
                cells[4, 0].sPos = new ScreenPosition(615, 217);

                cells[0, 1].sPos = new ScreenPosition(418, 128);
                cells[1, 1].sPos = new ScreenPosition(459, 159);
                cells[2, 1].sPos = new ScreenPosition(500, 193);
                cells[3, 1].sPos = new ScreenPosition(542, 226);
                cells[4, 1].sPos = new ScreenPosition(582, 258);

                cells[0, 2].sPos = new ScreenPosition(385, 168);
                cells[1, 2].sPos = new ScreenPosition(427, 201);
                cells[2, 2].sPos = new ScreenPosition(468, 234);
                cells[3, 2].sPos = new ScreenPosition(509, 266);
                cells[4, 2].sPos = new ScreenPosition(550, 330);

                cells[0, 3].sPos = new ScreenPosition(353, 210);
                cells[1, 3].sPos = new ScreenPosition(394, 242);
                cells[2, 3].sPos = new ScreenPosition(435, 275);
                cells[3, 3].sPos = new ScreenPosition(477, 308);
                cells[4, 3].sPos = new ScreenPosition(518, 341);

                cells[0, 4].sPos = new ScreenPosition(320, 251);
                cells[1, 4].sPos = new ScreenPosition(362, 284);
                cells[2, 4].sPos = new ScreenPosition(402, 317);
                cells[3, 4].sPos = new ScreenPosition(444, 351);
                cells[4, 4].sPos = new ScreenPosition(485, 383);
            }
            else
            {
                cells[0, 0].sPos = new ScreenPosition(460, 51);
                cells[1, 0].sPos = new ScreenPosition(508, 87);
                cells[2, 0].sPos = new ScreenPosition(556, 124);
                cells[3, 0].sPos = new ScreenPosition(603, 162);
                cells[4, 0].sPos = new ScreenPosition(649, 202);

                cells[0, 1].sPos = new ScreenPosition(422, 96);
                cells[1, 1].sPos = new ScreenPosition(470, 134);
                cells[2, 1].sPos = new ScreenPosition(517, 173);
                cells[3, 1].sPos = new ScreenPosition(564, 211);
                cells[4, 1].sPos = new ScreenPosition(611, 249);

                cells[0, 2].sPos = new ScreenPosition(385, 144);
                cells[1, 2].sPos = new ScreenPosition(432, 182);
                cells[2, 2].sPos = new ScreenPosition(479, 221);
                cells[3, 2].sPos = new ScreenPosition(525, 260);
                cells[4, 2].sPos = new ScreenPosition(572, 297);

                cells[0, 3].sPos = new ScreenPosition(347, 193);
                cells[1, 3].sPos = new ScreenPosition(394, 232);
                cells[2, 3].sPos = new ScreenPosition(441, 269);
                cells[3, 3].sPos = new ScreenPosition(488, 306);
                cells[4, 3].sPos = new ScreenPosition(535, 344);

                cells[0, 4].sPos = new ScreenPosition(310, 241);
                cells[1, 4].sPos = new ScreenPosition(357, 279);
                cells[2, 4].sPos = new ScreenPosition(404, 316);
                cells[3, 4].sPos = new ScreenPosition(451, 353);
                cells[4, 4].sPos = new ScreenPosition(498, 391);
            }
        }

        public BattleCube Scan()
        {
            for(int x = SCAN_RANGE_X_MIN; x <= SCAN_RANGE_X_MAX; x++)
            {
                for(int y = SCAN_RANGE_Y_MIN; y <= SCAN_RANGE_Y_MAX; y++)
                {
                    cells[x, y].Scan();
                }
            }

            return this;
        }

        #region NPC
        public bool ExistsNPC(int diffX, int diffY){
            return cells[CORE_X + diffX, CORE_Y + diffY].existsNPC;
        }

        public bool NPC8 { get { return ExistsNPC(0, -1); } }
        public bool NPC88 { get { return ExistsNPC(0, -2); } }
        public bool NPC888 { get { return ExistsNPC(0, -3); } }
        public bool NPC8888 { get { return ExistsNPC(0, -4); } }
        public bool NPC88884 { get { return ExistsNPC(-1, -4); } }
        public bool NPC888844 { get { return ExistsNPC(-2, -4); } }
        public bool NPC88886 { get { return ExistsNPC(1, -4); } }
        public bool NPC888866 { get { return ExistsNPC(2, -4); } }
        public bool NPC6 { get { return ExistsNPC(1, 0); } }
        public bool NPC66 { get { return ExistsNPC(2, 0); } }
        public bool NPC4 { get { return ExistsNPC(-1, 0); } }
        public bool NPC44 { get { return ExistsNPC(-2, 0); } }
        public bool NPC86 { get { return ExistsNPC(1, -1); } }
        public bool NPC84 { get { return ExistsNPC(-1, -1); } }
        public bool NPC886 { get { return ExistsNPC(1, -2); } }
        public bool NPC8866 { get { return ExistsNPC(2, -2); } }
        public bool NPC8844 { get { return ExistsNPC(-2, -2); } }
        public bool NPC88866 { get { return ExistsNPC(2, -3); } }
        public bool NPC88844 { get { return ExistsNPC(-2, -3); } }
        public bool NPC884 { get { return ExistsNPC(-1, -2); } }
        public bool NPC844 { get { return ExistsNPC(-2, -1); } }
        public bool NPC866 { get { return ExistsNPC(2, -1); } }
        public bool NPC8886 { get { return ExistsNPC(1, -3); } }
        public bool NPC8884 { get { return ExistsNPC(-1, -3); } }
        #endregion

        #region PC
        public bool ExistsPC(int diffX, int diffY)
        {
            return cells[CORE_X + diffX, CORE_Y + diffY].existsPC;
        }

        public bool PC8 { get { return ExistsPC(0, -1); } }
        public bool PC88 { get { return ExistsPC(0, -2); } }
        public bool PC888 { get { return ExistsPC(0, -3); } }
        public bool PC8888 { get { return ExistsPC(0, -4); } }
        public bool PC88884 { get { return ExistsPC(-1, -4); } }
        public bool PC88886 { get { return ExistsPC(1, -4); } }
        public bool PC888844 { get { return ExistsPC(-2, -4); } }
        public bool PC888866 { get { return ExistsPC(2, -4); } }
        public bool PC6 { get { return ExistsPC(1, 0); } }
        public bool PC66 { get { return ExistsPC(2, 0); } }
        public bool PC4 { get { return ExistsPC(-1, 0); } }
        public bool PC44 { get { return ExistsPC(-2, 0); } }
        public bool PC86 { get { return ExistsPC(1, -1); } }
        public bool PC84 { get { return ExistsPC(-1, -1); } }
        public bool PC886 { get { return ExistsPC(1, -2); } }
        public bool PC8866 { get { return ExistsPC(2, -2); } }
        public bool PC8844 { get { return ExistsPC(-2, -2); } }
        public bool PC88866 { get { return ExistsPC(2, -3); } }
        public bool PC88844 { get { return ExistsPC(-2, -3); } }
        public bool PC884 { get { return ExistsPC(-1, -2); } }
        public bool PC844 { get { return ExistsPC(-2, -1); } }
        public bool PC866 { get { return ExistsPC(2, -1); } }
        public bool PC8886 { get { return ExistsPC(1, -3); } }
        public bool PC8884 { get { return ExistsPC(-1, -3); } }
        #endregion
    }
}
