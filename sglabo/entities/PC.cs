using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    class PC
    {
        //public static List<PC> list = new List<PC>();

        public string code;
        public Bitmap nameImage;

        public Job job;
        public GridPosition gPos;

        public int ap;
        public int moveCount;
    }
}
