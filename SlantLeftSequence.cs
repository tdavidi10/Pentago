using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pentago_Tamir_Davidi
{
    class SlantLeftSequence
    {
        private bool blockedLeftUp; // האם הרצף חסום משמאל למעלה
        private bool blockedRightDown; //האם הרצף חסום מימין למטה
        private int oreh; //אורכו של הרצף

        public SlantLeftSequence(int oreh, bool blockedLeftUp, bool blockedRightDown) //פעולה בונה
        {
            this.oreh = oreh;
            this.blockedLeftUp = blockedLeftUp;
            this.blockedRightDown = blockedRightDown;
        }

        public bool TotalBlocked() //האם הרצף חסום משני צידיו
        {
            if (blockedLeftUp && blockedRightDown)
                return true;
            else return false;
        }

        public bool TotalFree() //האם הרצף פתוח משני צידיו
        {
            if (!blockedLeftUp && !blockedRightDown)
                return true;
            else return false;
        }

        public bool HalfFree() //האם הרצף חסום רק מצד אחד
        {
            if (!TotalBlocked() && !TotalFree())
                return true;
            return false;
        }

        public void SetOreh(int oreh) //פעולות מעדכנות
        {
            this.oreh = oreh;
        }

        public void SetBlockedLefttUp(bool blockedLeftUp)
        {
            this.blockedLeftUp = blockedLeftUp;
        }

        public void SetBlockedRightDown(bool blockedRightDown)
        {
            this.blockedRightDown = blockedRightDown;
        }

        public int GetOreh() //פעולות מאחזרות
        {
            return this.oreh;
        }

        public bool GetBlockedLeftUp()
        {
            return this.blockedLeftUp;
        }
        
        public bool GetBlockedRightDown()
        {
            return this.blockedRightDown;
        }


    }
}
