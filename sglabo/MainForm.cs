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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sglabo
{
    public partial class MainForm: Form
    {
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        public static bool isBattleTaskRunning = false;

        public MainForm()
        {
            InitializeComponent();

            RefleshPictures();
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            var sg = SGWindow.sgList.First();
            sg.Activate();
            sg.CapturePCNameFromStatus();
        }

        private void detectColorButton_Click(object sender, EventArgs e)
        {
            var bmp = Image.FromFile(@"C:\Users\Cignoir\Desktop\battle2.png") as Bitmap;
            var sg = SGWindow.sgList.First();
            var color = sg.DetectColor(bmp);

            statusLabel.Text = color.white + ":" + color.white + ":" + color.brown;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pictureBoxes.Add(pictureBox1);
        }

        private void refleshButton_Click(object sender, EventArgs e)
        {
            RefleshPictures();
        }

        private void RefleshPictures()
        {
            foreach(PictureBox p in pictureBoxes)
            {
                p.Image = null;
            }

            foreach(Process proc in Process.GetProcessesByName("ST_231").OrderBy(x => x.Id))
            {
                var sg = new SGWindow(proc);
                SGWindow.sgList.Add(sg);

                var pictureBox = pictureBoxes.Where(x => x.Image == null).First();
                pictureBox.Image = sg.pcName;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(isBattleTaskRunning) return;

            var sg = SGWindow.sgList.First();
            if(sg.IsField())
            {
                statusLabel.Text = "Field";
                // フィールド移動
            }
            else
            {
                statusLabel.Text = "Battle";

                var thread = new Thread(new ThreadStart(new Battle().Run));
                thread.IsBackground = true;
                thread.Start();
            }
        }

    }
}
