using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;
using WindowsInput.Native;

namespace sglabo
{
    partial class SGWindow
    {
        public Bitmap Capture()
        {
            Activate();

            input.Keyboard.KeyDown(VirtualKeyCode.END).Sleep(100);
            Bitmap bmp = GraphicUtils.CaptureActiveWindow();
            input.Keyboard.KeyUp(VirtualKeyCode.END).Sleep(100);
            return bmp;
        }

        public Bitmap CaptureRectangle(Rectangle rect)
        {
            Activate();
            var bmp = Capture();
            if(bmp != null)
            {
                var copy = bmp.Clone(rect, PixelFormat.Format32bppArgb);
                bmp.Dispose();
                return copy;
            }
            else
            {
                return null;
            }
        }

        public Bitmap CapturePCNameFromStatus()
        {
            Activate();

            CloseStatusWindow();
            OpenStatusWindow();
            pcName = CaptureRectangle(new Rectangle(543, 65, 180, 16));
            CloseStatusWindow();

            return pcName;
        }

        public Bitmap CaptureBattleStatus()
        {
            return CaptureRectangle(new Rectangle(0, 28, 67, 86));
        }

        public Bitmap CaptureFieldStatus()
        {
            return CaptureRectangle(new Rectangle(2, 20, 28, 50));
        }

        public SGColor DetectColor(Bitmap bmp, bool shouldBeDispose = true)
        {
            if(bmp != null)
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
            else
            {
                return new SGColor(0, 0, 0, 0, 0, 0, 0);
            }
        }
    }
}
