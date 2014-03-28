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
        int movingValue = 100;

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
            mainPC.MoveMouseOnLocalTo(400, 200 - movingValue);
            mainPC.LeftClick();
            Thread.Sleep(1000);

            mainPC.MoveMouseOnLocalTo(400, 200 + movingValue);
            mainPC.LeftClick();
            Thread.Sleep(1000);
        }

        public void HorizontalMove()
        {
            mainPC.MoveMouseOnLocalTo(400 + movingValue, 200);
            mainPC.LeftClick();
            Thread.Sleep(1000);

            mainPC.MoveMouseOnLocalTo(400 - movingValue, 200);
            mainPC.LeftClick();
            Thread.Sleep(1000);
        }
    }
}
