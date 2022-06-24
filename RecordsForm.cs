using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;




namespace Pentago_Tamir_Davidi
{
    public partial class RecordsForm : Form
    {
        MainMenu mainMenu; //טופס תפריט ראשי
        string playerName; //שם שחקן
        List<Score> scores; //רשימה של כל התוצאות
        Label[] times; //רשימה של כל הזמנים 
        Label[] names; //רשימה של כל שמות השחקנים

        public RecordsForm(MainMenu mainMenu, string playerName) //פעולה בונה
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            this.playerName = playerName;
            scores = new List<Score>();
            times = new Label[] { time0, time1, time2, time3, time4, time5, time6 };
            names = new Label[] { playerName0, playerName1, playerName2, playerName3, playerName4, playerName5, playerName6 };
            ResetScoresList("timeTable.txt");
            SortScores();
            SetLabels();
        }

        private void RecordsForm_Load(object sender, EventArgs e)
        {

        }

        private void homeButton_Click(object sender, EventArgs e) //חזרה לתפריט ראשי
        {
            this.Hide();
            mainMenu.Show();
        }

        private void FormInstructions_FormClosing(object sender, FormClosingEventArgs e) //יציאה מהתוכנה
        {
            Application.Exit();
        }



        public void ResetScoresList(string fileName)    
        {                                               //הפעולה קוראת מקובץ טקסט את התוצאות מהמשחקים הקודמים

            string time;
            string playerName;


            using (StreamReader streamReader = File.OpenText(fileName))
            {
                //time = "";                ///קודם שם ואז מספר
                //playerName = "";

                //while (playerName != null)
                //{
                //    playerName = streamReader.ReadLine();
                //    if (playerName == null)
                //        return;
                //    scores.Add(new Score(playerName));
                //    time = streamReader.ReadLine();
                //    scores.Last().SetTime(int.Parse(time));                                       
                //}


                time = "";                ///קודם מספר ואז שם 
                playerName = "";

                while (time != null)
                {
                    time = streamReader.ReadLine();
                    if (time == "" || time == null)
                        return;
                    scores.Add(new Score(int.Parse(time)));
                    playerName = streamReader.ReadLine();
                    scores.Last().SetPlayerName(playerName);
                }


            }
        }

        public void SortScores()  ///מארגן את רשימת התוצאות לפי זמן הניצחון הקצר אל הארוך
        {
            Score temp;

            for (int i = 0; i < scores.Count; i++)
            {
                for (int j = i + 1; j < scores.Count; j++)
                {
                    if (scores[i].GetTime() > scores[j].GetTime())
                    {
                        temp = scores[i];
                        scores[i] = scores[j];
                        scores[j] = temp;
                    }
                }
            }
        }

        public void SetLabels() ///מעדכן את הטקסט בלייבלים לפי המיון
        {
            for (int i = 0; i < times.Length; i++)
            {
                times[i].Text = scores[i].GetTime().ToString();
                names[i].Text = scores[i].GetPlayerName();
                if (i % 2 == 0)
                {
                    times[i].BackColor = Color.FromArgb(209, 215, 232);
                    names[i].BackColor = Color.FromArgb(209, 215, 232);
                }
                else
                {
                    times[i].BackColor = Color.FromArgb(233, 237, 245);
                    names[i].BackColor = Color.FromArgb(233, 237, 245);
                }


            }
        }

        private void scoreTable_Click(object sender, EventArgs e)
        {

        }

       
    }
}
