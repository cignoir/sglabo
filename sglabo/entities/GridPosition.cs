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
        public int z { get; set; }

        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public GridPosition(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

    }
}
