using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using sglabo.AI;
using sglabo.entities;

namespace sglabo
{
    class Battle
    {
        MainForm mainForm;

        public static int turn;
        public static Area area;
        public static int mapCode;
        public SGWindow mainPC;
        int loopLimit = 100;
        public bool inBattle = true;

        public static bool firstMatch = true;

        public Battle(MainForm mainForm)
        {
            this.mainForm = mainForm;
            MainForm.isBattleTaskRunning = true;

            mainPC = SGWindow.MainPC();
            mainPC.Activate();

            mainPC.RightClick();
            mainPC.RightClick();
            mainPC.MoveMouseOnLocalTo(404, 316);
            area = AreaConverter.ConvertFrom(mainForm.areaSelectorText);
            Thread.Sleep(1000);

            if(!IsBattleArena()) mapCode = DetectMap(area);
        }

        public void Run()
        {
            mainPC.Activate();


            while(inBattle)
            {
                turn++;

                mainForm.SetStatus(Properties.Resources.WaitingForMovingPhase);
                if(turn > 1) LoopWait(loopLimit);

                if(inBattle)
                {
                    try
                    {
                        foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                        {
                            pc.Activate();
                            pc.ap = turn == 1 ? 10 : pc.ap + 10;
                            //if(pc.job == Job.盗賊) pc.ap -= 1; // ファミ

                            if(pc.ai != null)
                            {
                                pc.ap -= pc.ai.SealCost();

                                if(turn == 1)
                                {
                                    pc.ai.TopView();
                                    if(pc.job == Job.黒印 || pc.job == Job.錬金) pc.ai.InitSeal();
                                }

                                try
                                {
                                    mainForm.SetStatus(Properties.Resources.ScanningBattleMap);
                                    var cube = new BattleCube();
                                    pc.ai.UpdateSituation(pc, cube);

                                    mainForm.SetStatus(Properties.Resources.NowMoving);
                                    pc.ai.PlayMove();
                                }
                                catch(Exception e)
                                {
                                    MessageBox.Show(e.Source + "\n" + e.Message + "\n" + e.StackTrace);
                                }
                            }
                        }
                        Thread.Sleep(2000);

                        mainForm.SetStatus(Properties.Resources.WaitingForActionPhase);
                        LoopWait(loopLimit);

                        foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                        {
                            if(pc.ai != null)
                            {
                                pc.Activate();

                                mainForm.SetStatus(Properties.Resources.ScanningBattleMap);
                                var cube = new BattleCube();
                    
                                mainForm.SetStatus(Properties.Resources.NowActing);
                                pc.ai.UpdateSituation(pc, cube.Scan());

                                pc.ai.PlaySkill();
                            }
                        }
                        Thread.Sleep(2000);

                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message + "\r\n" + e.StackTrace);
                    }
                }
            }
            
            if(mainPC.IsWaitingForLot()){
                mainForm.SetStatus(Properties.Resources.ItemLot);
                foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                {
                    pc.Activate();
                    pc.ItemLot();
                }
            }
            mainForm.SetStatus(Properties.Resources.BattleEnd);
            MainForm.isBattleTaskRunning = false;

            mainForm.DeleteMapImage();
            turn = 0;

            Thread.Sleep(2000);
        }

        private void LoopWait(int limit = 100)
        {
            while(limit > 0)
            {
                if(mainPC.IsField() || mainPC.IsWaitingForLot())
                {
                    inBattle = false;
                    break;
                }
                if(mainPC.IsWaitingForBattleInput()) break;

                Thread.Sleep(1000);
                limit--;
            }
        }

        public int DetectMap(Area area)
        {
            var sg = SGWindow.MainPC();
            sg.Activate();

            Bitmap bmp = null;

            var ptSize = SGWindow.GetPTSize();
            switch(area){
                case Area.ルデンヌ大森林:
                    switch(ptSize)
                    {
                        case PTSize.LARGE: break;
                        case PTSize.MEDIUM: break;
                        case PTSize.SMALL: bmp = sg.CaptureRectangle(new Rectangle(573, 248, 40, 40)); break;
                    }
                    break;
                case Area.ナビア北限地帯:
                    switch(ptSize)
                    {
                        case PTSize.LARGE: bmp = sg.CaptureRectangle(new Rectangle(573, 248, 40, 40)); break;
                        case PTSize.MEDIUM: break;
                        case PTSize.SMALL: break;
                    }
                    break;
                case Area.グランドロン :
                    switch(ptSize)
                    {
                        case PTSize.LARGE: bmp = sg.CaptureRectangle(new Rectangle(573, 248, 40, 40)); break;
                        case PTSize.MEDIUM: break;
                        case PTSize.SMALL: break;
                    }
                    break;
                default:
                    break;
            }
            if(bmp != null){
                mainForm.ShowMapImage(bmp.Clone() as Bitmap);
            }

            return bmp != null ? GraphicUtils.GenerateUniqueCode(bmp) : 0;
        }

        public ScreenPosition Core()
        {
            // FIXME
            // LARGE以外は移動でCOREが変動する

            ScreenPosition core = new ScreenPosition(0, 0);
            switch(SGWindow.GetPTSize())
            {
                case PTSize.LARGE:
                    core = new ScreenPosition(404, 316);
                    break;
                case PTSize.MEDIUM:
                case PTSize.SMALL:
                    core = new ScreenPosition(335, 416);
                    break;
                default:
                    break;
            }
            return core;
        }

        public static bool IsGrandron()
        {
            return Battle.mapCode == 9346174 || Battle.mapCode == 266499533 || Battle.mapCode == 209593538;
        }

        public static bool IsBattleArena()
        {
            return area == Area.戦豹の試練 || area == Area.猛虎の試練 || area == Area.荒獅子の試練;
        }
    }
}
