using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pentago_Tamir_Davidi
{
    class ArtificialIntelligence
    {
        private Board board; //הלוח הנוכחי במשחק
        private List<Board> boards; //רשימה של 8 לוחות שעשויים להיווצר בעקבות לחיצה על כל אחד מחיצי הסיבוב


        public ArtificialIntelligence(Board board, Graphics g, FormVictory formVictory, FormDefeat formDefeat)
        { //פעולה בונה

            this.board = board;

            this.boards = new List<Board>();

            //boards.Add(this.boardLUTurnRight);
            //boards.Add(this.boardLUTurnLeft);
            //boards.Add(this.boardRUTurnRight);
            //boards.Add(this.boardRUTurnLeft);
            //boards.Add(this.boardRDTurnRight);
            //boards.Add(this.boardRDTurnLeft);
            //boards.Add(this.boardLDTurnRight);
            //boards.Add(this.boardLDTurnLeft);

            for (int i = 0; i < 8; i++)
                boards.Add(new Board(board));

        }

        public void RotateBoards3()  //  פעולה מסובבת את כל חלקי הלוח בהתאם לחץ הסיבוב המתאים לו 
        {
            boards[0].GetQuarters()[0, 0].Rotate90DegreesRight(); // הפעולה מסובבת את הלוח הימני עליון ימינה
            boards[1].GetQuarters()[0, 0].Rotate90DegreesLeft();  // הפעולה מסובבת את הלוח הימני עליון שמאלה
            boards[2].GetQuarters()[0, 1].Rotate90DegreesRight(); // הפעולה מסובבת את הלוח הימני תחתון ימינה
            boards[3].GetQuarters()[0, 1].Rotate90DegreesLeft();   // הפעולה מסובבת את הלוח הימני תחתון שמאלה
            boards[4].GetQuarters()[1, 1].Rotate90DegreesRight(); // הפעולה מסובבת את הלוח השמאלי תחתון ימינה
            boards[5].GetQuarters()[1, 1].Rotate90DegreesLeft();  // הפעולה מסובבת את הלוח השמאלי תחתון שמאלה
            boards[6].GetQuarters()[1, 0].Rotate90DegreesRight(); // הפעולה מסובבת את הלוח השמאלי עליון ימינה
            boards[7].GetQuarters()[1, 0].Rotate90DegreesLeft();  // הפעולה מסובבת את הלוח השמאלי עליון שמאלה
            for (int i = 0; i < boards.Count; i++)
                boards[i].CopyQuartersToAllCells();
        }


        
        public bool ThereIsFiveSequence(int oreh, LineSequence maxLineSequence, ToorSequence maxToorSequence, SlantRightSequence maxSlantRightSequence, SlantLeftSequence maxSlantLeftSequence)
        {//האם קיים רצף באורך נתון 
            if (maxLineSequence.GetOreh() == oreh || maxToorSequence.GetOreh() == oreh || maxSlantLeftSequence.GetOreh() == oreh || maxSlantRightSequence.GetOreh() == oreh)
                return true;
            else
                return false;

        }

        public bool AllKindsTotalBlocked(LineSequence maxLineSequence, ToorSequence maxToorSequence, SlantRightSequence maxSlantRightSequence, SlantLeftSequence maxSlantLeftSequence)
        { //אם כל סוגי הרצפים (שורה,טור, אלכסונים) המקסימלים חסומים משני צידיהם
            if (maxLineSequence.TotalBlocked() && maxToorSequence.TotalBlocked() && maxSlantLeftSequence.TotalBlocked() && maxSlantRightSequence.TotalBlocked())
                return true;
            else
                return false;
        }

        public bool AtLeastOneTotalFreeSequence(int oreh, LineSequence maxLineSequence, ToorSequence maxToorSequence, SlantRightSequence maxSlantRightSequence, SlantLeftSequence maxSlantLeftSequence)
        {
            if ((maxLineSequence.GetOreh() == oreh && maxLineSequence.TotalFree()) ||         ////// האם יש רצף באורך מסויים ופתוח משני הכיוונים לפחות בסוג רצף אחד
                (maxToorSequence.GetOreh() == oreh && maxToorSequence.TotalFree()) ||
               (maxSlantLeftSequence.GetOreh() == oreh && maxSlantLeftSequence.TotalFree()) ||
                (maxSlantRightSequence.GetOreh() == oreh && maxSlantRightSequence.TotalFree()))
                return true;
            else
                return false;
        }

        public bool AtLeastOneSequenceHalfFree(int oreh, LineSequence maxLineSequence, ToorSequence maxToorSequence, SlantRightSequence maxSlantRightSequence, SlantLeftSequence maxSlantLeftSequence)
        { //// האם יש רצף באורך מסויים ופתוח למחצה לפחות בסוג רצף אחד
            if ((maxLineSequence.GetOreh() == oreh && maxLineSequence.HalfFree()) ||
                 (maxToorSequence.GetOreh() == oreh && maxToorSequence.HalfFree()) ||
                 (maxSlantLeftSequence.GetOreh() == oreh && maxSlantLeftSequence.HalfFree()) ||
                 (maxSlantRightSequence.GetOreh() == oreh && maxSlantRightSequence.HalfFree()))
                return true;

            return false;  
        }

        public bool SpaceSlantVictory(Board board, Cell.Status status)
        { //אם יש מצב של אלכסון של שלושה ברבעון אחד , הרביעי נמצא במרכז הרבעון המנוגד לו ווקצוותיו פנויים כך שסיבוב רבעון זה יצור חמישיה ברצף
            //זה מצב ניצחון בטוח
            if (board.RecursiaLengthLeftSlant(0, 0, status) == 3)
            {
                if (board.GetAllCells()[4, 4].GetStatus() == status)
                    if (board.GetAllCells()[3, 5].GetStatus() == Cell.Status.Empty || board.GetAllCells()[5, 3].GetStatus() == Cell.Status.Empty)
                        return true;
            }

            else if (board.RecursiaLengthLeftSlant(3, 3, status) == 3)
            {
                if (board.GetAllCells()[1, 1].GetStatus() == status)
                    if (board.GetAllCells()[2, 0].GetStatus() == Cell.Status.Empty || board.GetAllCells()[0, 2].GetStatus() == Cell.Status.Empty)
                        return true;
            }

            else if (board.RecursiaLengthRightSlant(3, 2, status) == 3)
            {
                if (board.GetAllCells()[1, 4].GetStatus() == status)
                    if (board.GetAllCells()[2, 5].GetStatus() == Cell.Status.Empty || board.GetAllCells()[0, 3].GetStatus() == Cell.Status.Empty)
                        return true;
            }

            else if (board.RecursiaLengthRightSlant(0, 5, status) == 3)
            {
                if (board.GetAllCells()[4, 1].GetStatus() == status)
                    if (board.GetAllCells()[3, 0].GetStatus() == Cell.Status.Empty || board.GetAllCells()[5, 2].GetStatus() == Cell.Status.Empty)
                        return true;
            }

            return false;




        }

        public int Rate(Board board, Cell.Status status) //הפעולה מעריכה את מצב הלוח ונותנת לו ניקוד בהתאם לכמה הוא קרוב לניצחון לצבע מסוים
        {
            LineSequence maxLineSequence = board.MaxFreeSequanceLine(status);
            ToorSequence maxToorSequence = board.MaxFreeSequanceToor(status);
            SlantRightSequence maxSlantRightSequence = board.MaxFreeSequenceSlantRight(status);
            SlantLeftSequence maxSlantLeftSequence = board.MaxFreeSequenceSlantLeft(status);


            if (ThereIsFiveSequence(5, maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence) ||
                SpaceSlantVictory(board, status))
                return 1000; ///אם יש חמש ברצף
            else if (AllKindsTotalBlocked(maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence))
                return 1;  ////אם הכל חסום

            else if (AtLeastOneTotalFreeSequence(4, maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence))
                return 80;    ////אורך קבוע 4 ופתוח משני הכיוונים לפחות בסוג רצף אחד
            else if (AtLeastOneSequenceHalfFree(4, maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence))
                return 70;   //אורך קבוע 4 ופתוח מכיוון אחד לפחות בסוג רצף אחד

            else if (AtLeastOneTotalFreeSequence(3, maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence))
                return 60;    ////אורך קבוע 3 ופתוח משני הכיוונים לפחות בסוג רצף אחד
            else if (AtLeastOneSequenceHalfFree(3, maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence))
                return 50; // אורך קבוע 3 ופתוח מכיוון אחד לפחות בסוג רצף אחד

            else if (AtLeastOneTotalFreeSequence(2, maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence))
                return 40;    ////אורך קבוע 2 ופתוח משני הכיוונים לפחות בסוג רצף אחד
            else if (AtLeastOneSequenceHalfFree(2, maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence))
                return 30;  // אורך קבוע 2 ופתוח מכיוון אחד לפחות בסוג רצף אחד

            else if (AtLeastOneTotalFreeSequence(1, maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence))
                return 20;    ////אורך קבוע 1 ופתוח משני הכיוונים לפחות בסוג רצף אחד
            else if (AtLeastOneSequenceHalfFree(1, maxLineSequence, maxToorSequence, maxSlantRightSequence, maxSlantLeftSequence))
                return 10;   // אורך קבוע 1 ופתוח מכיוון אחד לפחות בסוג רצף אחד
            else
                return 5;




        }

        public void RatePlaceRotate(Graphics g, Board realBoard, FormVictory formVictory, FormDefeat formDefeat)
        {//הפעולה מבצעת תור עבור המחשב:מציבה אבן ומסובבת את אחד מחלקי הלוח
            PlacingBest(g, realBoard, formVictory, formDefeat); //מציבה אבן במקום המיטבי
            for (int i = 0; i < boards.Count; i++)   //מעדכן את לוחות המצבים של כפתורי החצים בהצבת הגולה
            {
                boards[i] = new Board(realBoard);
            }
            RotateBoards3();    //מסובב את לוחות מצבי החצים

            int max = -100; //ערך התחלתי מינימלי
            int[] maxValueOfBoards = new int[8];    //מערך שמכיל את הניקוד המקסימלי לכל לוח - לוח לכל כפתור סיבוב

            for (int i = 0; i < maxValueOfBoards.Length; i++) 
            {
                maxValueOfBoards[i] =Rate(boards[i], Cell.Status.Black) - Rate(boards[i], Cell.Status.White) ;   //// למחשב מצא מצב (לוח) מיטבי
                if (maxValueOfBoards[i] > max)
                    max = maxValueOfBoards[i];
            }


            for (int i = 0; i < maxValueOfBoards.Length; i++)             
            {
                if (max == maxValueOfBoards[i])   ///אם זה הלוח המיטבי
                {                                  //סובב את הרבעון המתאים
                    if (i == 0)
                        realBoard.GetLeftUpTurnRight().Activate();          
                    else if (i == 1)
                        realBoard.GetLeftUpTurnLeft().Activate();
                    else if (i == 2)
                        realBoard.GetRightUpTurnRight().Activate();
                    else if (i == 3)
                        realBoard.GetRightUpTurnLeft().Activate();
                    else if (i == 4)
                        realBoard.GetRightDownTurnRight().Activate();
                    else if (i == 5)
                        realBoard.GetRightDownTurnLeft().Activate();
                    else if (i == 6)
                        realBoard.GetLeftDownTurnRight().Activate();
                    else if (i == 7)
                        realBoard.GetLeftDownTurnLeft().Activate();

                    return;
                }
            }



        }








        public void PlacingBest(Graphics g, Board realBoard, FormVictory formVictory, FormDefeat formDefeat) ///ממקמת גולה במקום המיטבי בלוח
        {
            Board testBoard = new Board(realBoard);
            int testRate = 0;
            PlacingOptions pOptions = new PlacingOptions();
            PlacingOption bestOption = new PlacingOption();

            for (int i = 0; i < realBoard.GetAllCells().GetLength(0); i++)     ////עובר תא תא על אפשרויות ההצבה בלוח ונוקד שווי לכל הצבה מוצא את המקסימלי ומציב בה
            {
                for (int j = 0; j < realBoard.GetAllCells().GetLength(1); j++)
                {
                    if (realBoard.GetAllCells()[i, j].GetStatus() == Cell.Status.Empty)
                    {
                        testBoard.GetAllCells()[i,j].SetStatus(Cell.Status.Black);                        
                        testRate = Rate(testBoard, Cell.Status.Black) - Rate(testBoard, Cell.Status.White);        //מנקד את המצב
                        testBoard.GetAllCells()[i, j].SetStatus(Cell.Status.Empty);

                        pOptions.Add(new PlacingOption(i, j, testRate));     ////מוסיף את אופצית ההצבה במיקום זה לרשימת האופציות
                    }

                }

            }

            bestOption = pOptions.MaxOption();

            realBoard.GetAllCells()[bestOption.GetI(), bestOption.GetJ()].SetStatus(Cell.Status.Black);

            realBoard.GetAllCells()[bestOption.GetI(), bestOption.GetJ()].PlaceBlack(g);   //מציב את הגולה במקום שנמצא מתאים

            System.Threading.Thread.Sleep(700);

            if (board.IsTotalVictory(Cell.Status.White))     // בודק אם יש ניצחון או הפסד
            {
                System.Threading.Thread.Sleep(700);//זה מאט את התגובה של המחשב
                formVictory.Show();
                return;
            }
            else if (board.IsTotalVictory(Cell.Status.Black))
            {
                System.Threading.Thread.Sleep(700);//זה מאט את התגובה של המחשב 
                formDefeat.Show();
                return;
            }


        }


        public void RandomPlacing(Graphics g)   //. פעולה לבדיקה עצמית בלבד ללא שימוש מעשי. הפעולה ממקמת גולה במיקום אקראי בלוח
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (this.board.GetAllCells()[i, j].GetStatus() == Cell.Status.Empty)
                    {
                        this.board.GetAllCells()[i, j].PlaceBlack(g);                       
                        
                        System.Threading.Thread.Sleep(700);
                        
                        return;
                    }
                }
            }
        }









    }
}
