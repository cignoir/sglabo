using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using sglabo.entities;
using WindowsInput;

namespace sglabo
{
    class Field
    {
        SGWindow mainPC;
        InputSimulator input = new InputSimulator();
        int movingValue = 200;
        int intervalMS = 500;

        public Field()
        {
            mainPC = SGWindow.MainPC();
            movingValue = MainForm.movingValue;
        }
 
        public void Run()
        {
            mainPC.Activate();

            while(MainForm.isStarted && !MainForm.isBattleTaskRunning)
            {
                if(MainForm.fieldMovingDirection == Direction.VERTICAL)
                {
                    VerticalMove();
                }
                else
                {
                    HorizontalMove();
                }
            }
        }

        public void VerticalMove()
        {
            mainPC.MoveMouseOnLocalTo(400, 300 - movingValue);
            mainPC.LeftClick();
            mainPC.LeftClick();
            Thread.Sleep(intervalMS);

            mainPC.MoveMouseOnLocalTo(400, 300 + movingValue);
            mainPC.LeftClick();
            mainPC.LeftClick();
            Thread.Sleep(intervalMS);
        }

        public void HorizontalMove()
        {
            mainPC.MoveMouseOnLocalTo(400 + movingValue, 300);
            mainPC.LeftClick();
            mainPC.LeftClick();
            Thread.Sleep(intervalMS);

            mainPC.MoveMouseOnLocalTo(400 - movingValue, 300);
            mainPC.LeftClick();
            mainPC.LeftClick();
            Thread.Sleep(intervalMS);
        }
    }
}
