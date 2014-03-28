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
        List<SGWindow> tmpList = new List<SGWindow>();

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
                            AbortAllThreads();

                            isStarted = false;
                            SetStatus(Properties.Resources.ProcessStopped);
                        }
                        break;
                    case Win32API.WM_HOTKEY_STOP:
                        AbortAllThreads();

                        isStarted = false;
                        SetStatus(Properties.Resources.ProcessStopped);
                        break;
                    //case Win32API.WM_HOTKEY1:
                    //    this.tmpList = SGWindow.sgList.Where(x => x.pcCode == 228) as List<SGWindow>;
                    //    if(tmpList.Count() > 0)
                    //    {
                    //        tmpList.First().Activate();
                    //    }
                    //    else
                    //    {
                    //        SGWindow.sgList.First().Activate();
                    //    }
                    //    break;
                    //case Win32API.WM_HOTKEY2:
                    //    this.tmpList = SGWindow.sgList.Where(x => x.pcCode == 197) as List<SGWindow>;
                    //    if(tmpList.Count() > 0) tmpList.First().Activate();
                    //    break;
                    //case Win32API.WM_HOTKEY3:
                    //    this.tmpList = SGWindow.sgList.Where(x => x.pcCode == 216) as List<SGWindow>;
                    //    if(tmpList.Count() > 0) tmpList.First().Activate();
                    //    break;
                    //case Win32API.WM_HOTKEY4:
                    //    this.tmpList = SGWindow.sgList.Where(x => x.pcCode == 190) as List<SGWindow>;
                    //    if(tmpList.Count() > 0) tmpList.First().Activate();
                    //    break;
                    //case Win32API.WM_HOTKEY5:
                    //    this.tmpList = SGWindow.sgList.Where(x => x.pcCode == 182) as List<SGWindow>;
                    //    if(tmpList.Count() > 0) tmpList.First().Activate();
                    //    break;
                    default:
                        break;
                }
            }
        }

        private void AbortAllThreads()
        {
            if(fieldThread != null && fieldThread.IsAlive) fieldThread.Abort();
            if(battleThread != null && battleThread.IsAlive) battleThread.Abort();
            if(NoThreadsWorking()) isBattleTaskRunning = false;
        }

        private bool NoThreadsWorking()
        {
            return (battleThread == null || (battleThread != null && !battleThread.IsAlive))
                    && (fieldThread == null || (fieldThread != null && !fieldThread.IsAlive));
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
                        //Win32API.RegisterHotKey(sg.hWnd, Win32API.WM_HOTKEY4, Win32API.MOD_ALT, (int)Keys.D4);
                        break;
                    case 197:
                        sg.job = Job.戦士;
                        sg.ai = new Warrior();
                        sg.IsCenter = false;
                        //Win32API.RegisterHotKey(sg.hWnd, Win32API.WM_HOTKEY2, Win32API.MOD_ALT, (int)Keys.D2);
                        break;
                    case 228:
                        sg.job = Job.精霊;
                        sg.ai = new SpiritMage();
                        sg.IsCenter = true;
                        //Win32API.RegisterHotKey(sg.hWnd, Win32API.WM_HOTKEY1, Win32API.MOD_ALT, (int)Keys.D1);
                        break;
                    case 216:
                        sg.job = Job.守護;
                        sg.ai = new ProtectionMage();
                        sg.IsCenter = false;
                        //Win32API.RegisterHotKey(sg.hWnd, Win32API.WM_HOTKEY3, Win32API.MOD_ALT, (int)Keys.D3);
                        break;
                    case 182:
                        sg.job = Job.盗賊;
                        sg.ai = new Thief();
                        sg.IsCenter = false;
                        //Win32API.RegisterHotKey(sg.hWnd, Win32API.WM_HOTKEY5, Win32API.MOD_ALT, (int)Keys.D5);
                        break;
                    default:
                        sg.job = Job.戦士;
                        sg.ai = new Warrior();
                        sg.IsCenter = false;
                        //Win32API.RegisterHotKey(sg.hWnd, Win32API.WM_HOTKEY1, Win32API.MOD_ALT, (int)Keys.D1);
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

            if(SGWindow.sgList.Count > 0 && SGWindow.sgList.Where(x => x.IsCenter).Count() == 0){
                SGWindow.sgList.First().IsCenter = true;
                radioButton1.Checked = true;
            }

            SGWindow.MainPC().Activate();
        }

        public void ShowMapImage(Bitmap bmp)
        {
            mapPictureBox.Image = bmp;
        }

        public void DeleteMapImage()
        {
            if(mapPictureBox.Image != null)
            {
                mapPictureBox.Image.Dispose();
                mapPictureBox.Image = null;
            }
        }
    }
}
