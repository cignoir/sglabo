using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sglabo
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
            var proc = Process.GetProcessesByName("ST_231")[0];
            var sg = new SGWindow(proc);
            sg.Activate();

            sg.OpenStatusWindow();
            sg.CloseStatusWindow();

            var bmp = sg.Capture();
            Rectangle rect = new Rectangle(362, 239, 100, 40);
            bmp = bmp.Clone(rect, PixelFormat.Format32bppArgb);
            bmp.Save(@"C:\hoge.bmp");

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
            }
            Marshal.Copy(ba, 0, bmpdata.Scan0, ba.Length);
            bmp.UnlockBits(bmpdata);

            MessageBox.Show(whiteCount + ":" + yellowCount + ":" + brownCount);
        }
    }
}
