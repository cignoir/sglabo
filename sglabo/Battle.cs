using System;
using System.Collections.Generic;
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

        public BattleField battleField;
        public static int turn;
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
            battleField = new BattleField(BattleField.Detect(area));
            battleField.Scan();
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
                    mainForm.SetStatus(Properties.Resources.ScanningBattleMap);
                    battleField.Scan();
                    SGWindow.battleField = battleField;
                    Thread.Sleep(1000);

                    mainForm.SetStatus(Properties.Resources.NowMoving);
                    foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                    {
                        if(pc.ai != null)
                        {
                            pc.Activate();
                            pc.ai.UpdateSituation(battleField, pc);
                            pc.ai.PlayMove();
                        }
                        else
                        {
                            MessageBox.Show(Properties.Resources.NoAIFound);
                        }
                    }

                    mainForm.SetStatus(Properties.Resources.WaitingForActionPhase);
                    LoopWait(loopLimit);

                    mainForm.SetStatus(Properties.Resources.ScanningBattleMap);
                    battleField.Scan();
                    Thread.Sleep(1000);

                    mainForm.SetStatus(Properties.Resources.NowActing);
                    foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                    {
                        if(pc.ai != null)
                        {
                            pc.Activate();
                            pc.ai.UpdateSituation(battleField, pc);
                            pc.ai.PlaySkill();
                        }
                        else
                        {
                            MessageBox.Show(Properties.Resources.NoAIFound);
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
    }
}
