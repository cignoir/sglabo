﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        bool inBattle = true;

        public Battle(MainForm mainForm)
        {
            this.mainForm = mainForm;
            MainForm.isBattleTaskRunning = true;

            mainPC = SGWindow.sgList.First();
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

                mainForm.SetStatus("移動フェイズ待機中...");
                LoopWait(loopLimit);

                if(inBattle)
                {
                    battleField.Scan();
                    SGWindow.battleField = battleField;
                    Thread.Sleep(1000);

                    mainForm.SetStatus("移動中...");
                    foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                    {
                        pc.Activate();
                        pc.BattleMove();
                    }
                    Thread.Sleep(1000);

                    mainForm.SetStatus("行動フェイズ待機中...");
                    LoopWait(loopLimit);
                    mainForm.SetStatus("行動中...");

                    battleField.Scan();
                    Thread.Sleep(1000);

                    foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                    {
                        if(pc.ai != null)
                        {
                            pc.Activate();
                            pc.ai.PlaySkill(battleField, pc);
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
            
            if(mainPC.IsWaitingLot()){
                mainForm.SetStatus("アイテムのロット中...");
                foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                {
                    pc.Activate();
                    pc.ItemLot();
                }
            }
            mainForm.SetStatus("戦闘終了");

            MainForm.isBattleTaskRunning = false;
        }

        private void LoopWait(int limit = 100)
        {
            while(limit > 0)
            {
                if(mainPC.IsField() || mainPC.IsWaitingLot())
                {
                    inBattle = false;
                    break;
                }
                if(mainPC.IsWaitingBattleInput()) break;

                Thread.Sleep(1000);
                limit--;
            }
        }
    }
}
