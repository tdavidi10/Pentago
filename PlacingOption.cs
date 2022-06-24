using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pentago_Tamir_Davidi
{
    class PlacingOption
    {
        private int rate;  //הניקוד למהלך - אפשרות
        private int i; // ערך I של המקום בו נמקם אבן בלוח
        private int j;// ערך J של המקום בו נמקם אבן בלוח

        public PlacingOption(int i, int j, int rate) //פעולה בונה
        {
            this.i = i;
            this.j = j;
            this.rate = rate;
        }

        public PlacingOption() //פעולה בונה ריקה
        {

        }

        public int GetI() //פעולות מאחזרות
        {
            return this.i;
        }

        public int GetJ()
        {
            return this.j;
        }

        public int GetRate()
        {
            return this.rate;
        }

        public void SetI(int i) //פעולות מעדכנות
        {
            this.i = i;
        }

        public void SetJ(int j)
        {
            this.j = j;
        }

        public void SetRate(int rate)
        {
            this.rate = rate;
        }

        public void SetIJRate(int i, int j, int rate)
        {
            this.i = i;
            this.j = j;
            this.rate = rate;
        }











    }
}
