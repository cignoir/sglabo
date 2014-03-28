using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace sglabo.entities
{
    class BattleCubeCell
    {
        public GridPosition gPos;
        public ScreenPosition sPos;
        int height;
        bool canMove = true;

        public bool existsPC = false;
        public bool existsNPC = false;

        public bool sealedCross = false;
        public bool sealedDot = false;

        public BattleCubeCell(GridPosition gPos)
        {
            this.sPos = new ScreenPosition(0, 0);
            this.gPos = gPos;
        }

        public BattleCubeCell(GridPosition gPos, ScreenPosition sPos, int height, bool canMove)
        {
            this.gPos = gPos;
            this.sPos = sPos;
            this.height = height;
            this.canMove = canMove;
        }

        public Bitmap CaptureName()
        {
            Bitmap bmp = GraphicUtils.CaptureActiveWindow();
            Rectangle rect = new Rectangle(sPos.x - 6, sPos.y - 6, 12, 12);
            var copy = bmp.Clone(rect, PixelFormat.Format32bppArgb);
            bmp.Dispose();
            return copy;
        }

        public void Scan()
        {
            var color = DetectColor(CaptureName());
            existsNPC = color.yellow > 0 && color.brown > 0;
            existsPC = color.brown > 0 && color.white > 0 && color.yellow == 0;
        }

        public SGColor DetectColor(Bitmap bmp, bool shouldBeDispose = true)
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
            int redCount = 0;
            int enemyPinkCount = 0;

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
                if(r == 255 && g == 0 && b == 0) redCount++;
                if(r == 245 && g == 105 && b == 80) enemyPinkCount++;
            }
            Marshal.Copy(ba, 0, bmpdata.Scan0, ba.Length);
            bmp.UnlockBits(bmpdata);
            if(shouldBeDispose)
            {
                bmp.Dispose();
            }

            return new SGColor(whiteCount, yellowCount, brownCount, pinkCount, greenCount, redCount, enemyPinkCount);
        }
    }
}
