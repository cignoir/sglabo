using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    public class Vector2
    {
        public long x { get; set; }
        public long y { get; set; }

        public Vector2(long x, long y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
