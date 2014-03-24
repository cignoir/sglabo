using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sglabo.entities;
using WindowsInput.Native;

namespace sglabo
{
    partial class SGWindow
    {
        public void BattleAction()
        {
            if(job == Job.戦士)
            {
                Warrior.PlaySkill(battleField, this);
            }
        }
    }
}
