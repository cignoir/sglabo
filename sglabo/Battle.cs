using System;
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
        public BattleField battleField;
        public int turn;
        public SGWindow mainPC;
        int loopLimit = 100;

        public Battle()
        {
            MainForm.isBattleTaskRunning = true;

            mainPC = SGWindow.sgList.First();
            mainPC.Activate();

            battleField = new BattleField(BattleField.DetectBattleMapName("ルデンヌ大森林"));
            battleField.Scan();
        }

        public void Run()
        {
            mainPC.Activate();

            while(turn == 0 || !mainPC.IsBattleEnd())
            {
                turn++;

                LoopWait(loopLimit);

                Thread.Sleep(1000);

                foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                {
                    pc.Activate();
                    pc.BattleMove();
                }
                Thread.Sleep(1000);
                LoopWait(loopLimit);

                foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
                {
                    pc.Activate();
                    pc.BattleAction();
                }
                Thread.Sleep(1000);
            }

            foreach(SGWindow pc in SGWindow.sgList.Where(x => x.auto))
            {
                pc.Activate();
                pc.ItemLot();
            }

            MainForm.isBattleTaskRunning = false;
        }

        private void LoopWait(int limit = 100)
        {
            while(limit > 0)
            {
                if(mainPC.IsWaitingBattleInput()) break;

                Thread.Sleep(1000);
                limit--;
            }
        }
    }
}
