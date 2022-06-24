using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pentago_Tamir_Davidi
{
    class Score
    {
        private int time; //זמן שלקח למשתמש לנצח
        private string playerName; //שמו של המשתמש

        public Score(int time, string playerName) //פעולה בונה
        {
            this.time = time;
            this.playerName = playerName;
        }

        public Score(int time) //פעולות מעדכנות
        {
            this.time = time;
        }

        public Score(string playerName)
        {
            this.playerName = playerName;
        }

        public void SetTime(int time)
        {
            this.time = time;
        }

        public void SetPlayerName(string playerName)
        {
            this.playerName = playerName;
        }
         
        public int GetTime() //פעולות מאחזרות
        {
            return this.time;
        }

        public string GetPlayerName()
        {
            return this.playerName;
        }



    }
}
