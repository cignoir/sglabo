using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    class GridPosition
    {
        public int x { get; set; }
        public int y { get; set; }

        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
