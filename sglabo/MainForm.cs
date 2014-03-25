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
            pictureBoxes.Add(pictureBox2);
            pictureBoxes.Add(pictureBox3);
            pictureBoxes.Add(pictureBox4);
            pictureBoxes.Add(pictureBox5);

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
                var sg = new SGWindow(proc, SGWindow.sgList.Count == 0);
                SGWindow.sgList.Add(sg);

                var pictureBox = pictureBoxes.Where(x => x.Image == null).First();
                pictureBox.Image = sg.pcName;
            }
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count == 0) return;
 
            var sg = SGWindow.sgList.First();
            sg.Activate();

            if(textBox1.Text.Length != 0)
            {
                var rectInfo = textBox1.Text.Split(',');
                int x = int.Parse(rectInfo[0]);
                int y = int.Parse(rectInfo[1]);
                int width = int.Parse(rectInfo[2]);
                int height = int.Parse(rectInfo[3]);

                //var input = new InputSimulator();
                //input.Mouse.MoveMouseTo(sg.sPos.x + 400, sg.sPos.y + 300);

                var rect = new Rectangle(x, y, width, height);
                var bmp = sg.CaptureRectangle(rect);
                bmp.Save(@"C:\map.bmp");
                var code = GraphicUtils.GenerateUniqueCode(bmp).ToString();
                statusLabel.Text = Properties.Resources.MapCodeGenerated + ":" + code;
                Clipboard.SetDataObject(code);
            }
            else
            {
                sg.CapturePCNameFromStatus();
                sg.IsWaitingForLot();
            }
        }

        private void detectColorButton_Click(object sender, EventArgs e)
        {
            var bmp = Image.FromFile(@"C:\Users\Cignoir\Desktop\battle2.png") as Bitmap;
            var sg = SGWindow.sgList.First();
            var color = sg.DetectColor(bmp);
            bmp.Dispose();

            statusLabel.Text = color.white + ":" + color.yellow + ":" + color.brown;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pictureBoxes.Add(pictureBox1);
            pictureBoxes.Add(pictureBox2);
            pictureBoxes.Add(pictureBox3);
            pictureBoxes.Add(pictureBox4);
            pictureBoxes.Add(pictureBox5);

            foreach(SGWindow sg in SGWindow.sgList){
                sg.job = JobConverter.ConvertToJobFrom(jobSelector1.Text);
                sg.ai = JobConverter.ConvertToAIFrom(jobSelector1.Text);
            }

            if(SGWindow.sgList.Count > 0)
            {
                Win32API.RECT rect;
                Win32API.GetWindowRect(SGWindow.sgList.First().hWnd, out rect);
                this.Location = new Point(rect.right + 6, rect.top - 5);
            }
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
            if(sg.IsWaitingForBattleInput())
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
            else
            {
                SetStatus(Properties.Resources.Field);
                // フィールド移動
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY_START);
            Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY_STOP);
        }

        #region Activateボタン
        private void activateButton1_Click(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 1){
                var sg = SGWindow.sgList.ElementAt(0);
                if(sg != null) sg.Activate();
            }
        }

        private void activateButton2_Click(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 2)
            {
                var sg = SGWindow.sgList.ElementAt(1);
                if(sg != null) sg.Activate();
            }
        }

        private void activateButton3_Click(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 3)
            {
                var sg = SGWindow.sgList.ElementAt(2);
                if(sg != null) sg.Activate();
            }
        }

        private void activateButton4_Click(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 4)
            {
                var sg = SGWindow.sgList.ElementAt(3);
                if(sg != null) sg.Activate();
            }
        }

        private void activateButton5_Click(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count == 5)
            {
                var sg = SGWindow.sgList.ElementAt(4);
                if(sg != null) sg.Activate();
            }
        }
        #endregion

        #region ジョブ選択コンボボックス
        private void jobSelector1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 1)
            {
                var sg = SGWindow.sgList.ElementAt(0);
                sg.job = JobConverter.ConvertToJobFrom(jobSelector1.Text);
                sg.ai = JobConverter.ConvertToAIFrom(jobSelector1.Text);
            }
        }

        private void jobSelector2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 2)
            {
                var sg = SGWindow.sgList.ElementAt(1);
                sg.job = JobConverter.ConvertToJobFrom(jobSelector2.Text);
                sg.ai = JobConverter.ConvertToAIFrom(jobSelector2.Text);
            }
        }

        private void jobSelector3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 3)
            {
                var sg = SGWindow.sgList.ElementAt(2);
                sg.job = JobConverter.ConvertToJobFrom(jobSelector3.Text);
                sg.ai = JobConverter.ConvertToAIFrom(jobSelector3.Text);
            }
        }

        private void jobSelector4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 4)
            {
                var sg = SGWindow.sgList.ElementAt(3);
                sg.job = JobConverter.ConvertToJobFrom(jobSelector4.Text);
                sg.ai = JobConverter.ConvertToAIFrom(jobSelector4.Text);
            }
        }

        private void jobSelector5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 5)
            {
                var sg = SGWindow.sgList.ElementAt(4);
                sg.job = JobConverter.ConvertToJobFrom(jobSelector5.Text);
                sg.ai = JobConverter.ConvertToAIFrom(jobSelector5.Text);
            }
        }
        #endregion

        #region 中央配置選択ラヂオボタン
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 1)
            {
                var sg = SGWindow.sgList.ElementAt(0);
                sg.IsCenter = radioButton1.Checked;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 2)
            {
                var sg = SGWindow.sgList.ElementAt(1);
                sg.IsCenter = radioButton2.Checked;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 3)
            {
                var sg = SGWindow.sgList.ElementAt(2);
                sg.IsCenter = radioButton3.Checked;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 4)
            {
                var sg = SGWindow.sgList.ElementAt(3);
                sg.IsCenter = radioButton4.Checked;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 5)
            {
                var sg = SGWindow.sgList.ElementAt(4);
                sg.IsCenter = radioButton5.Checked;
            }
        }
        #endregion

        #region autoチェックボックス
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 1)
            {
                var sg = SGWindow.sgList.ElementAt(0);
                sg.auto = checkBox1.Checked;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 2)
            {
                var sg = SGWindow.sgList.ElementAt(1);
                sg.auto = checkBox2.Checked;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 3)
            {
                var sg = SGWindow.sgList.ElementAt(2);
                sg.auto = checkBox3.Checked;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 4)
            {
                var sg = SGWindow.sgList.ElementAt(3);
                sg.auto = checkBox4.Checked;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count >= 5)
            {
                var sg = SGWindow.sgList.ElementAt(4);
                sg.auto = checkBox5.Checked;
            }
        }
        #endregion

        private void captureWindow_Click(object sender, EventArgs e)
        {
            var sg = SGWindow.sgList.First();
            var bmp = sg.Capture();
            bmp.Save(@"C:\screenshot.bmp");
        }
    }
}
