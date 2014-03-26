using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using sglabo.entities;

namespace sglabo
{
    class Battle
    {
        MainForm mainForm;

        public static int turn;
        public static int mapCode;
        public SGWindow mainPC;
        int loopLimit = 100;
        public bool inBattle = true;

        public Battle(MainForm mainForm)
        {
            this.mainForm = mainForm;
            MainForm.isBattleTaskRunning = true;

            mainPC = SGWindow.MainPC();
            mainPC.Activate();

            Area area = AreaConverter.ConvertFrom(mainForm.areaSelectorText);
            mapCode = DetectMap(area);
        }

        public void Run()
        {
            mainPC.Activate();

            while(inBattle)
            {
                turn++;

                foreach(SGWindow sg in SGWindow.sgList){
                    sg.ap += 10;
                }

                mainForm.SetStatus(Properties.Resources.WaitingForMovingPhase);
                if(turn > 1) LoopWait(loopLimit);

                if(inBattle)
                {
                    mainForm.SetStatus(Properties.Resources.NowMoving);
                    foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                    {
                        if(pc.ai != null)
                        {
                            pc.Activate();

                            mainForm.SetStatus(Properties.Resources.ScanningBattleMap);
                            var cube = new BattleCube(new ScreenPosition(0, 0));
                            pc.ai.UpdateSituation(pc, cube.Scan());
                            pc.ai.PlayMove();
                        }
                    }

                    mainForm.SetStatus(Properties.Resources.WaitingForActionPhase);
                    LoopWait(loopLimit);

                    foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                    {
                        if(pc.ai != null)
                        {
                            pc.Activate();

                            mainForm.SetStatus(Properties.Resources.ScanningBattleMap);
                            var cube = new BattleCube(new ScreenPosition(0, 0));
                    
                            mainForm.SetStatus(Properties.Resources.NowActing);
                            pc.ai.UpdateSituation(pc, cube.Scan());
                            //pc.ai.PlaySkill();
                        }
                    }
                    Thread.Sleep(1000);
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

            Thread.Sleep(3000);
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

        public static int DetectMap(Area area)
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
                default:
                    break;
            }

            return bmp != null ? GraphicUtils.GenerateUniqueCode(bmp) : 0;
        }
    }
}
