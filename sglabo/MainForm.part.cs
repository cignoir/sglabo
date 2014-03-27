using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sglabo.AI;
using sglabo.entities;

namespace sglabo
{
    public partial class MainForm: Form
    {
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
                        if(thread != null) thread.Abort();
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

        private void RefleshView()
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

                // FIXME
                var index = int.Parse(pictureBox.Name.Substring(pictureBox.Name.Length - 1, 1));
                switch(sg.pcCode)
                {
                    case 190:
                        sg.job = Job.黒印;
                        sg.ai = new SealMage();
                        sg.IsCenter = false;
                        break;
                    case 197:
                        sg.job = Job.戦士;
                        sg.ai = new Warrior();
                        sg.IsCenter = false;
                        break;
                    case 228:
                        sg.job = Job.精霊;
                        sg.ai = new SpiritMage();
                        sg.IsCenter = true;
                        break;
                    case 216:
                        sg.job = Job.守護;
                        sg.ai = new ProtectionMage();
                        sg.IsCenter = false;
                        break;
                    case 182:
                        sg.job = Job.盗賊;
                        sg.ai = new Thief();
                        sg.IsCenter = false;
                        break;
                    default:
                        sg.job = Job.戦士;
                        sg.ai = new Warrior();
                        sg.IsCenter = false;
                        break;
                }

                switch(index)
                {
                    case 1:
                        switch(sg.job)
                        {
                            case Job.戦士:
                                radioButton1.Checked = false;
                                jobSelector1.Text = "戦士";
                                break;
                            case Job.精霊:
                                radioButton1.Checked = true;
                                jobSelector1.Text = "精霊";
                                break;
                            case Job.盗賊:
                                radioButton1.Checked = false;
                                jobSelector1.Text = "盗賊";
                                break;
                            case Job.守護:
                                radioButton1.Checked = false;
                                jobSelector1.Text = "守護";
                                break;
                            case Job.黒印:
                                radioButton1.Checked = false;
                                jobSelector1.Text = "黒印";
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        switch(sg.job)
                        {
                            case Job.戦士:
                                radioButton2.Checked = false;
                                jobSelector2.Text = "戦士";
                                break;
                            case Job.精霊:
                                radioButton2.Checked = true;
                                jobSelector2.Text = "精霊";
                                break;
                            case Job.盗賊:
                                radioButton2.Checked = false;
                                jobSelector2.Text = "盗賊";
                                break;
                            case Job.守護:
                                radioButton2.Checked = false;
                                jobSelector2.Text = "守護";
                                break;
                            case Job.黒印:
                                radioButton2.Checked = false;
                                jobSelector2.Text = "黒印";
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch(sg.job)
                        {
                            case Job.戦士:
                                radioButton3.Checked = false;
                                jobSelector3.Text = "戦士";
                                break;
                            case Job.精霊:
                                radioButton3.Checked = true;
                                jobSelector3.Text = "精霊";
                                break;
                            case Job.盗賊:
                                radioButton3.Checked = false;
                                jobSelector3.Text = "盗賊";
                                break;
                            case Job.守護:
                                radioButton3.Checked = false;
                                jobSelector3.Text = "守護";
                                break;
                            case Job.黒印:
                                radioButton3.Checked = false;
                                jobSelector3.Text = "黒印";
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        switch(sg.job)
                        {
                            case Job.戦士:
                                radioButton4.Checked = false;
                                jobSelector4.Text = "戦士";
                                break;
                            case Job.精霊:
                                radioButton4.Checked = true;
                                jobSelector4.Text = "精霊";
                                break;
                            case Job.盗賊:
                                radioButton4.Checked = false;
                                jobSelector4.Text = "盗賊";
                                break;
                            case Job.守護:
                                radioButton4.Checked = false;
                                jobSelector4.Text = "守護";
                                break;
                            case Job.黒印:
                                radioButton4.Checked = false;
                                jobSelector4.Text = "黒印";
                                break;
                            default:
                                break;
                        }
                        break;
                    case 5:
                        switch(sg.job)
                        {
                            case Job.戦士:
                                radioButton5.Checked = false;
                                jobSelector5.Text = "戦士";
                                break;
                            case Job.精霊:
                                radioButton5.Checked = true;
                                jobSelector5.Text = "精霊";
                                break;
                            case Job.盗賊:
                                radioButton5.Checked = false;
                                jobSelector5.Text = "盗賊";
                                break;
                            case Job.守護:
                                radioButton5.Checked = false;
                                jobSelector5.Text = "守護";
                                break;
                            case Job.黒印:
                                radioButton5.Checked = false;
                                jobSelector5.Text = "黒印";
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

            if(SGWindow.sgList.Count > 0) codeLabel1.Text = SGWindow.sgList.ElementAt(0).pcCode.ToString();
            if(SGWindow.sgList.Count > 1) codeLabel2.Text = SGWindow.sgList.ElementAt(1).pcCode.ToString();
            if(SGWindow.sgList.Count > 2) codeLabel3.Text = SGWindow.sgList.ElementAt(2).pcCode.ToString();
            if(SGWindow.sgList.Count > 3) codeLabel4.Text = SGWindow.sgList.ElementAt(3).pcCode.ToString();
            if(SGWindow.sgList.Count > 4) codeLabel5.Text = SGWindow.sgList.ElementAt(4).pcCode.ToString();

            SGWindow.MainPC().Activate();
        }

        public void ShowMapImage(Bitmap bmp)
        {
            mapPictureBox.Image = bmp;
        }
    }
}
