using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    class BattleFieldCell
    {
        GridPosition gPos;

        ScreenPosition N;
        ScreenPosition E;
        ScreenPosition W;
        ScreenPosition S;

        ScreenPosition center;

        public BattleFieldCell(ScreenPosition N, ScreenPosition E, ScreenPosition W, ScreenPosition S)
        {
            this.N = N;
            this.E = E;
            this.W = W;
            this.S = S;

            this.center = new ScreenPosition(Math.Abs((N.x - S.x) / 2), Math.Abs((E.y - W.y) / 2));
            Rectangle rect = new Rectangle(center.x - 50, center.y - 20, 100, 40);
        }

        public bool ExistsPC()
        {
            return false;
        }

        public bool ExistsEnemy()
        {
            return false;
        }
    }
}
