using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    class BattleFieldCell
    {
        GridPosition gPos;
        ScreenPosition sPos;
        int height;
        bool canMove = true;

        bool existsPC = false;
        bool existsNPC = false;

        PC pc;

        public BattleFieldCell(GridPosition gPos, ScreenPosition sPos, int height, bool canMove)
        {
            this.gPos = gPos;
            this.sPos = sPos;
            this.height = height;
            this.canMove = canMove;
        }

        public Bitmap CaptureName()
        {
            Bitmap bmp = GraphicUtils.CaptureActiveWindow();
            Rectangle rect = new Rectangle(sPos.x - 50, sPos.y - 20, 100, 40);
            return bmp.Clone(rect, PixelFormat.Format32bppArgb);
        }

        public void Scan()
        {
            var color = DetectColor(CaptureName());
            if(color.yellow > 10 && color.brown > 10)
            {
                existsPC = false;
                existsNPC = true;
                pc = null;
            }
            else if(color.white > 10 && color.brown > 10)
            {
                existsPC = true;
                existsNPC = false;

                var code = color.brown.ToString();
                pc = PC.list.Where(x => x.code == code).First();
            }
            else
            {
                existsPC = false;
                existsNPC = false;
                pc = null;
            }
        }

        public SGColor DetectColor(Bitmap bmp)
        {
            BitmapData bmpdata = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb
            );

            byte[] ba = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(bmpdata.Scan0, ba, 0, ba.Length);

            int whiteCount = 0;
            int yellowCount = 0;
            int brownCount = 0;
            int pinkCount = 0;
            int greenCount = 0;

            int pixsize = bmp.Width * bmp.Height * 4;
            for(int i = 0; i < pixsize; i += 4)
            {
                var b = ba[i + 0];
                var g = ba[i + 1];
                var r = ba[i + 2];
                var a = ba[i + 3];

                if(r == 255 && g == 255 && b == 255) whiteCount++;
                if(r == 255 && g == 255 && b == 0) yellowCount++;
                if(r == 102 && g == 34 && b == 0) brownCount++;
                if(r == 255 && g == 120 && b == 255) pinkCount++;
                if(r == 102 && g == 221 && b == 204) greenCount++;
            }
            Marshal.Copy(ba, 0, bmpdata.Scan0, ba.Length);
            bmp.UnlockBits(bmpdata);

            return new SGColor(whiteCount, yellowCount, brownCount, pinkCount, greenCount);
        }
    }
}
