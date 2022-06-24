using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pentago_Tamir_Davidi
{
    class PlacingOptions
    {
        private List<PlacingOption> options; // רשימה של מהלכים אפשריים לביצוע על ידי המחשב

        public PlacingOptions() //פעולה בונה
        { 
            this.options=new List<PlacingOption>();
        }


        public PlacingOption MaxOption() //פעולה שמחזירה את המהלך עם הניקוד הגבוה ביותר ברשימה
        {
            if (this.options.Count == 0)
                return null;
            
            int iMax = -1;
            int jMax = -1;
            int maxRate = int.MinValue;
            PlacingOption maxPlacingOption = new PlacingOption(iMax,jMax, maxRate);

            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].GetRate() > maxRate)
                {
                    maxRate = options[i].GetRate();
                    iMax = options[i].GetI();
                    jMax = options[i].GetJ();
                    maxPlacingOption.SetIJRate(iMax, jMax, maxRate);
                }
            }

            return maxPlacingOption;

        }

        public List<PlacingOption> GetOptions() //מחזירה את הרשימה
        {
            return this.options;
        }

        public void Add(PlacingOption placingOption) //מוסיפה בסוף הרשימה מהלך חדש
        {
            this.options.Add(placingOption);
        }

        

      







    }
}
