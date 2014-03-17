using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    class ScreenPosition
    {
        public int x { get; set; }
        public int y { get; set; }

        public ScreenPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
