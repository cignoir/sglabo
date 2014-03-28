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
        public static Direction fieldMovingDirection = Direction.VERTICAL;
        public static int movingValue = 100;

        public Thread fieldThread;
        public Thread battleThread;
        public string areaSelectorText;

        public MainForm()
        {
            InitializeComponent();

            Win32API.RegisterHotKey(this.Handle, Win32API.WM_HOTKEY_START, Win32API.MOD_ALT, (int)Keys.S);
            Win32API.RegisterHotKey(this.Handle, Win32API.WM_HOTKEY_STOP, Win32API.MOD_ALT, (int)Keys.Q);
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            if(SGWindow.sgList.Count == 0) return;
 
            var sg = SGWindow.MainPC();
            sg.Activate();

            if(textBox1.Text.Length != 0)
            {
                var rectInfo = textBox1.Text.Split(',');
                int x = int.Parse(rectInfo[0]);
                int y = int.Parse(rectInfo[1]);
                int width = int.Parse(rectInfo[2]);
                int height = int.Parse(rectInfo[3]);

                var rect = new Rectangle(x, y, width, height);
                var bmp = sg.CaptureRectangle(rect);
                var code = GraphicUtils.GenerateUniqueCode(bmp).ToString();
                bmp.Save(@"C:\" + code + ".bmp");
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
            pictureBoxes = new List<PictureBox> { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };
            RefleshView();

            if(SGWindow.sgList.Count > 0)
            {
                Win32API.RECT rect;
                Win32API.GetWindowRect(SGWindow.sgList.First().hWnd, out rect);
                this.Location = new Point(rect.right + 6, rect.top - 5);
                SGWindow.MainPC().Activate();
            }
        }

        private void refleshButton_Click(object sender, EventArgs e)
        {
            RefleshView();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!isStarted) return;
            if(isBattleTaskRunning) return;

            var sg = SGWindow.MainPC();
            if(!isBattleTaskRunning && sg.IsWaitingForBattleInput())
            {
                SetStatus(Properties.Resources.BattleStart);

                AbortAllThreads();

                areaSelectorText = areaSelector.Text;

                if(NoThreadsWorking())
                {
                    battleThread = new Thread(new ThreadStart(new Battle(this).Run));
                    battleThread.IsBackground = false;
                    battleThread.Start();
                }
            }
            else if(sg.IsField())
            {
                SetStatus(Properties.Resources.Field);
                if(NoThreadsWorking())
                {
                    fieldThread = new Thread(new ThreadStart(new Field().Run));
                    fieldThread.IsBackground = false;
                    fieldThread.Start();
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY_START);
            Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY_STOP);
            //Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY1);
            //Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY2);
            //Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY3);
            //Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY4);
            //Win32API.UnregisterHotKey(this.Handle, Win32API.WM_HOTKEY5);
        }

        private void captureWindow_Click(object sender, EventArgs e)
        {
            var sg = SGWindow.sgList.First();
            var bmp = sg.Capture();
            bmp.Save(@"C:\screenshot.bmp");
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

        private void vMoveButton_CheckedChanged(object sender, EventArgs e)
        {
            if(vMoveButton.Checked) fieldMovingDirection = Direction.VERTICAL;
            if(hMoveButton.Checked) fieldMovingDirection = Direction.HORIZONTAL;
        }

        private void hMoveButton_CheckedChanged(object sender, EventArgs e)
        {
            if(vMoveButton.Checked) fieldMovingDirection = Direction.VERTICAL;
            if(hMoveButton.Checked) fieldMovingDirection = Direction.HORIZONTAL;
        }

        private void movingValueTextBox_TextChanged(object sender, EventArgs e)
        {
            movingValue = int.Parse(movingValueTextBox.Text);
        }

    }
}
