using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pentago_Tamir_Davidi
{
    class SlantRightSequence
    {
        private bool blockedRightUp; //האם הרצף חסום מימין למעלה
        private bool blockedLeftDown; //האם הרצף חסום משמאל למטה
        private int oreh; //אורכו של הרצף

        public SlantRightSequence(int oreh, bool blockedRightUp, bool blockedLeftDown) //פעולה בונה
        {
            this.oreh = oreh;
            this.blockedRightUp = blockedRightUp;
            this.blockedLeftDown = blockedLeftDown;
        }

        public bool TotalBlocked() //האם הרצף חחסום משני צידיו
        {
            if (blockedLeftDown && blockedRightUp)
                return true;
            else return false;
        }

        public bool TotalFree() //האם הרצף פתוח משני צידיו
        {
            if (!blockedLeftDown && !blockedRightUp)
                return true;
            else return false;
        }

        public bool HalfFree() //האם הרצף פתוח רק מצד אחד
        {
            if (!TotalBlocked() && !TotalFree())
                return true;
            return false;
        }

        public void SetOreh(int oreh) //פעולות מעדכנות
        {
            this.oreh = oreh;
        }

        public void SetBlockedRightUp(bool blockedRightUp)
        {
            this.blockedRightUp = blockedRightUp;
        }

        public void SetBlockedLeftDown(bool blockedLeftDown)
        {
            this.blockedLeftDown = blockedLeftDown;
        }

        public int GetOreh() // פעולות מאחזרות
        {
            return this.oreh;
        }

        public bool GetBlockedRightUp()
        {
            return this.blockedRightUp;
        }
        
        public bool GetBlockedLeftDown()
        {
            return this.blockedLeftDown;
        }


    }
}
