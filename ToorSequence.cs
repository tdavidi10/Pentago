using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pentago_Tamir_Davidi
{
    class ToorSequence
    {
        private bool blockedUp; //האם הרצף חסום מלמעלה
        private bool blockedDown; //האם הרצף חסום מלמטה
        private int oreh; //אורכו של הרצף

        public ToorSequence(int oreh, bool blockedUp, bool blockedDown) //פעולה בונה
        {
            this.oreh = oreh;
            this.blockedUp = blockedUp;
            this.blockedDown = blockedDown;
        }

        public bool TotalBlocked() //האם הרצף חסום משני צידיו
        {
            if (blockedUp && blockedDown)
                return true;
            else return false;
        }

        public bool TotalFree() //האם הרצף פתוח משני צידיו
        {
            if (!blockedUp && !blockedDown)
                return true;
            else return false;
        }

        public bool HalfFree() //האם הרצף חסום רק מצד אחד
        {
            if (!TotalBlocked() && !TotalFree())
                return true;
            return false;
        }

        public void SetOreh(int oreh)// פעולות מעדכנות
        {
            this.oreh = oreh;
        }

        public void SetBlockedUp(bool blockedUp)
        {
            this.blockedUp = blockedUp;
        }

        public void SetBlockedDown(bool blockedDown)
        {
            this.blockedDown = blockedDown;
        }

        public int GetOreh() //פעולות מאחזרות
        {
            return this.oreh;
        }

        public bool GetBlockedUp()
        {
            return this.blockedUp;
        }
        
        public bool GetBlockedDown()
        {
            return this.blockedDown;
        }
          


    }
}
