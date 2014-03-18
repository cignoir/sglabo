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
        SGWindow sg1;

        public MainForm()
        {
            InitializeComponent();
            var proc = Process.GetProcessesByName("ST_231")[0];
            sg1 = new SGWindow(proc);
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            sg1.Activate();
            sg1.CapturePCNameFromStatus();
        }

        private void detectColorButton_Click(object sender, EventArgs e)
        {
            var bmp = Image.FromFile(@"C:\Users\Cignoir\Desktop\battle2.png") as Bitmap;
            var color = sg1.DetectColor(bmp);

            textBox1.Text = color.white + ":" + color.white + ":" + color.brown;
        }
    }
}
