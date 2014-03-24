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
using sglabo.AI;
using sglabo.entities;
using WindowsInput;
using WindowsInput.Native;

namespace sglabo
{
    public partial class MainForm: Form
    {
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        public static bool isBattleTaskRunning = false;
        public static bool isStarted = false;
        public Thread thread;
        public string areaSelectorText;

        public MainForm()
        {
            InitializeComponent();

            Win32API.RegisterHotKey(this.Handle, Win32API.WM_HOTKEY_START, Win32API.MOD_ALT, (int)Keys.S);
            Win32API.RegisterHotKey(this.Handle, Win32API.WM_HOTKEY_STOP, Win32API.MOD_ALT, (int)Keys.Q);

            pictureBoxes.Add(pictureBox1);
            RefleshPictures();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if(m.Msg == Win32API.WM_HOTKEY)
            {
                switch((int)m.WParam)
                {
                    case Win32API.WM_HOTKEY_START:
                        isStarted = !isStarted;
                        SetStatus(isStarted ? Properties.Resources.ProcessStarted : Properties.Resources.ProcessStopped);

                        if(!isStarted)
                        {
                            if(thread != null) thread.Abort();
                            isBattleTaskRunning = false;
                            isStarted = false;
                            SetStatus(Properties.Resources.ProcessStopped);
                        }
                        break;
                    case Win32API.WM_HOTKEY_STOP:
                        if(thread !=null) thread.Abort();
                        isBattleTaskRunning = false;
                        isStarted = false;
                        SetStatus(Properties.Resources.ProcessStopped);
                        break;
                    default:
                        break;
                }
            }
        }

        public void SetStatus(string message)
        {
            statusLabel.Text = message;
        }

        private void RefleshPictures()
        {
            foreach(PictureBox p in pictureBoxes)
            {
                p.Image = null;
            }

            SGWindow.sgList.Clear();

            foreach(Process proc in Process.GetProcessesByName("ST_231").OrderBy(x => x.Id))
            {
                var sg = new SGWindow(proc);
                SGWindow.sgList.Add(sg);

                var pictureBox = pictureBoxes.Where(x => x.Image == null).First();
                sg.pcName.Save(@"C:\hoge.bmp");
                pictureBox.Image = sg.pcName;
            }
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            var sg = SGWindow.sgList.First();
            sg.Activate();

            if(textBox1.Text.Length != 0)
            {
                var rectInfo = textBox1.Text.Split(',');
                int x = int.Parse(rectInfo[0]);
                int y = int.Parse(rectInfo[1]);
                int width = int.Parse(rectInfo[2]);
                int height = int.Parse(rectInfo[3]);

                var input = new InputSimulator();
                input.Mouse.MoveMouseTo(sg.sPos.x + 400, sg.sPos.y + 300);

                var rect = new Rectangle(x, y, width, height);
                var bmp = sg.CaptureRectangle(rect);
                var code = GraphicUtils.GenerateUniqueCode(bmp).ToString();
                statusLabel.Text = Properties.Resources.MapCodeGenerated + ":" + code;
                Clipboard.SetDataObject(code);
            }
            else
            {
                sg.CapturePCNameFromStatus();
                sg.IsWaitingLot();
            }
        }

        private void detectColorButton_Click(object sender, EventArgs e)
        {
            var bmp = Image.FromFile(@"C:\Users\Cignoir\Desktop\battle2.png") as Bitmap;
            var sg = SGWindow.sgList.First();
            var color = sg.DetectColor(bmp);
            bmp.Dispose();

            statusLabel.Text = color.white + ":" + color.white + ":" + color.brown;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pictureBoxes.Add(pictureBox1);

            var sg = SGWindow.sgList.First();
            if(sg == null)
            {
                MessageBox.Show(Properties.Resources.NoProcessesFound);
                this.Close();
            }
            else
            {
                sg.job = JobConverter.ConvertToJobFrom(jobSelector1.Text);
                sg.ai = JobConverter.ConvertToAIFrom(jobSelector1.Text);
            }

            Win32API.RECT rect;
            Win32API.GetWindowRect(sg.hWnd, out rect);
            this.Location = new Point(rect.right + 6, rect.top - 5);
        }

        private void refleshButton_Click(object sender, EventArgs e)
        {
            RefleshPictures();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!isStarted) return;
            if(isBattleTaskRunning) return;

            var sg = SGWindow.sgList.First();
            if(sg.IsField())
            {
                SetStatus(Properties.Resources.Field);
                // フィールド移動
            }
            else
            {
                if(!isBattleTaskRunning){
                    SetStatus(Properties.Resources.BattleStart);
                    if(thread != null && thread.IsAlive) thread.Abort();

                    areaSelectorText = areaSelector.Text;

                    thread = new Thread(new ThreadStart(new Battle(this).Run));
                    thread.IsBackground = false;
                    thread.Start();
                }

            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            isStarted = !isStarted;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            System.GC.Collect();
        }

        private void jobSelector1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sg = SGWindow.sgList.ElementAt(0);
            sg.job = JobConverter.ConvertToJobFrom(jobSelector1.Text);
            sg.ai = JobConverter.ConvertToAIFrom(jobSelector1.Text);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY_START);
            Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY_STOP);
        }

    }
}
