using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Pentago_Tamir_Davidi
{
    class Board
    {
        private int x;     //ערך איקס בעוגן הלוח    
        private int y;     // ערך הוואי בעוגן הלוח  
        private int width;  //  רוחב הלוח   
        private int height;  /// גובה הלוח
        private Quarter[,] quarters;  // מערך דו מימדי של ארבעת חלקי הלוח - הרבעונים
        public enum Turn { Black, White, gameOver }; // פרמטר שמראה לי של מי התור - שלי או של המחשב או שהמשחק נגמר
        private Turn turn;
        public enum TurnClick { cell, rotationArrow, gameOver }; //פרמטר שמראה אם עכשיו צריכה להגיע לחיצה על תא בלוח או על חץ סיבוב
        private TurnClick turnClick;
        private Cell[,] allCells;    // מערך דוד מימדי של כל התאים בלוח
        private RotationArrow rightUpTurnRight;  //חץ סיבוב ימין למעלה שמסובב ימינה
        private RotationArrow rightUpTurnLeft;   //חץ סיבוב ימין למעלה שמסובב שמאלה
        private RotationArrow rightDownTurnRight;//חץ סיבוב ימין למטה שמסובב ימינה
        private RotationArrow rightDownTurnLeft;//חץ סיבוב ימין למטה שמסובב שמאלה
        private RotationArrow leftDownTurnRight;//חץ סיבוב שמאל למטה שמסובב ימינה
        private RotationArrow leftDownTurnLeft;//חץ סיבוב שמאלה למטה שמסובב שמאלה
        private RotationArrow leftUpTurnRight;//חץ סיבוב שמאל למעלה שמסובב ימינה
        private RotationArrow leftUpTurnLeft;//חץ סיבוב שמאל למעלה שמסובב שמאלה
        private Graphics g; //משתנה הגרפיקה
        private Pen penBoardFrame;   //עט שמצייר את מסגרת הלוח
        private Pen penCells;  // עט שמצייר את מסגרת התאים בלוח
        private Pen penWin;  // עט שמצייר את קו הניצחון
        private FormVictory formVictory; //טופס ניצחון
        private FormDefeat formDefeat;  // טופס הפסד
        private FormTie formTie;  // טופס תיקו


        //פעולה בונה
        public Board(int x, int y, int width, int height, FormGame formGame, Graphics g, Pen penBoardFrame, Pen penCells, FormVictory formVictory, FormDefeat formDefeat, FormTie formTie) // constructor function
       {
            this.formVictory = formVictory;
            this.formDefeat = formDefeat;
            this.formTie = formTie;
            this.g = g;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.penBoardFrame = penBoardFrame;
            this.penCells = penCells;
            this.penWin = new Pen(Color.Green, 10);

            int difX = (int)(this.width / 2); //  width of a quarter
            int difY = (int)(this.height / 2);  // height of a quarter
           
            this.quarters = new Quarter[2, 2];   //מגדירים את 4 חלקי הלוח 
            this.quarters[0, 0] = new Quarter(this.x, this.y, difX, difY,formGame);
            this.quarters[0, 1] = new Quarter(this.x + difX, this.y, difX, difY, formGame);
            this.quarters[1, 0] = new Quarter(this.x, this.y + difY, difX, difY, formGame);
            this.quarters[1, 1] = new Quarter(this.x + difX, this.y + difY, difX, difY, formGame);
            this.turn = Turn.White;       ///הגדרת התור הראשון של השחקן (לא המחשב)
            this.turnClick = TurnClick.cell;
            allCells = new Cell[6, 6];
            for (int i = 0; i < 3; i++)               ///מעדכן את איברי המערך הכולל אחרי כל תור
            {
                for (int j = 0; j < 3; j++)
                {
                    allCells[i, j] = this.quarters[0, 0].GetNineCells()[i, j];
                }
            } 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    allCells[i, j] = this.quarters[0, 1].GetNineCells()[i, j-3];
                }
            }
            for (int i = 3; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    allCells[i, j] = this.quarters[1, 0].GetNineCells()[i-3, j];
                }
            }
            for (int i = 3; i < 6; i++)
            { 
                for (int j = 3; j < 6; j++)
                {
                    allCells[i, j] = this.quarters[1, 1].GetNineCells()[i-3, j-3];
                } 
            }

            ////הגדרת חצי הסיבוב
            rightUpTurnRight = new RotationArrow((int)(this.allCells[1, 5].GetX() * 1.19), this.allCells[1, 5].GetY(), 85, 85, formGame, Properties.Resources.rightUpTurnRight2, RotationArrow.Direction.Right, this.quarters[0, 1], g, this, this.penBoardFrame, this.penCells, formVictory, formDefeat, formTie);
            rightUpTurnLeft = new RotationArrow((int)(this.allCells[0, 4].GetX() * 0.97), (int)(this.allCells[0, 4].GetY() * 0.2), 105, 105, formGame, Properties.Resources.rightUpTurnLeft2, RotationArrow.Direction.Left, this.quarters[0, 1], g, this, this.penBoardFrame, this.penCells, formVictory, formDefeat, formTie);
            rightDownTurnRight = new RotationArrow((int)(this.allCells[4, 5].GetX() * 1.19), this.allCells[4, 5].GetY(), 90, 90, formGame, Properties.Resources.rightDownTurnRight2, RotationArrow.Direction.Right, this.quarters[1, 1], g, this, this.penBoardFrame, this.penCells, formVictory, formDefeat, formTie);
            rightDownTurnLeft = new RotationArrow((int)(this.allCells[5, 4].GetX() * 0.985), (int)(this.allCells[5, 4].GetY() * 1.16), 95, 95, formGame, Properties.Resources.rightDownTurnLeft2, RotationArrow.Direction.Left, this.quarters[1, 1], g, this, this.penBoardFrame, this.penCells, formVictory, formDefeat, formTie);
            leftDownTurnRight = new RotationArrow((int)(this.allCells[5, 1].GetX() * 0.98), (int)(this.allCells[5, 1].GetY() * 1.165), 90, 90, formGame, Properties.Resources.LeftDownTurnRight2, RotationArrow.Direction.Right, this.quarters[1, 0], g, this, this.penBoardFrame, this.penCells, formVictory, formDefeat, formTie);
            leftDownTurnLeft = new RotationArrow((int)(this.allCells[4, 0].GetX() * 0.3), (int)(this.allCells[4, 0].GetY() * 0.97), 95, 95, formGame, Properties.Resources.LeftDownTurnLeft2, RotationArrow.Direction.Left, this.quarters[1, 0], g, this, this.penBoardFrame, this.penCells, formVictory, formDefeat, formTie);
            leftUpTurnRight = new RotationArrow((int)(this.allCells[1, 0].GetX() * 0.3), (int)(this.allCells[1, 0].GetY() * 0.98), 87, 87, formGame, Properties.Resources.LeftUpTurnRight2, RotationArrow.Direction.Right, this.quarters[0, 0], g, this, this.penBoardFrame, this.penCells, formVictory, formDefeat, formTie);
            leftUpTurnLeft = new RotationArrow((int)(this.allCells[0, 1].GetX() * 0.98), (int)(this.allCells[0, 1].GetY() * 0.21), 97, 97, formGame, Properties.Resources.LeftUpTurnLeft2, RotationArrow.Direction.Left, this.quarters[0, 0], g, this, this.penBoardFrame, this.penCells, formVictory, formDefeat, formTie);
 

       }

       public void Draw(Graphics g)            ////מציירת את הלוח
       {
 
           int difX1=(int)(this.width / 2);  // רוחב רבעון הוא חצי מרוחב הלוח
           int difY1 = (int)(this.height / 2); //גובה רבעון הוא חצי מגובה הלוח
           for (int i = 0; i < quarters.GetLength(0); i++)
           {
               for (int j = 0; j < quarters.GetLength(1); j++)
               {
                   this.quarters[0, 0].SetAll(this.x, this.y, difX1, difY1);     ////מעדכן את גודל הרבעונים
                   this.quarters[0, 1].SetAll(this.x+difX1, this.y, difX1, difY1);
                   this.quarters[1, 0].SetAll(this.x, this.y + difY1, difX1, difY1);
                   this.quarters[1, 1].SetAll(this.x + difX1, this.y + difY1, difX1, difY1);

                   this.quarters[i, j].Draw(g, this.penBoardFrame, this.penCells);  /// שולח את פקודת הציור ליחידות קטנות יותר - לתאים שבהם
               }

           }
       }

       public void MouseClick(Graphics g, int x0, int y0)   /// מנהל אירוע לחיצה על הלוח
       {
           bool locationInCircle = false;  // האם הלחיצה נעשתה בתוך התא העגול
           bool locationInBoard = true; // האם הלחיצה נעשתה בתוך הלוח
           bool locationFree = true;
           if (this.turn == Turn.White)  // אם עכשיו תור המשתמש
           {
               if (this.turnClick == TurnClick.rotationArrow) // אם כעת על השחקן ללחוץ על כפתור הסיבוב
               {
                   MessageBox.Show("you need to click on a rotation arrow first"); // אז תצא הודעה שתגיד שעל המשתמש ללחוץ קודם על כפתור סיבוב
                   return;
               }

               if (!this.Inside(x0, y0))  ///if click wasnt in board and wasnt in arrow
               {
                   locationInBoard = false;
                   MessageBox.Show("click was neither in the board nor in rotation arrow"); // הודעה שתגיד למשתמש שהלחיצה לא על חץ סיבוב ולא בלוח
               }

               bool tempInCircle=false;
               for (int i = 0; i < 2; i++)
               {
                   for (int j = 0; j < 2; j++)
                   {
                       if (this.quarters[i, j].Inside(x0, y0)) // אם הלחיצה נמצאת באחד מהרבעונים
                       {
                           tempInCircle = this.quarters[i, j].IsInCellCircle(x0, y0); // בודק אם הלחיצה נעשתה בתוך העיגול של התא
                           if (tempInCircle) //אם כן
                               locationInCircle = true;
                       }
                   }
               }

               if (!locationInCircle) //אם הלחיצה לא נעשתה בתא של העיגול
                   MessageBox.Show("click inside the circle"); // הצג הודעת שגיאה

               else // אם הלחיצה נעשתה בתור המתאים ובתוך העיגול של התא
               for (int i = 0; i < 2; i++)
               {
                   for (int j = 0; j < 2; j++)
                   {
                       if (this.quarters[i, j].Inside(x0, y0)) //בודקת באיזה רבעון נעשה הלחיצה
                           locationFree = this.quarters[i, j].MouseClick(g, x0, y0, this.turn); // אם הלחיצה נעשתה על תא ריק, פעולה מחזירה אמת וממקמת אבן בתא זה. אם התא מלא אז מחזירה שקר.
                   }
               }


               if (locationFree && locationInBoard && locationInCircle) //אם נעשה תור ומוקמה אבן בלוח
               {
                   this.turnClick = TurnClick.rotationArrow;   //כעת על המשתמש יהיה ללחוץ כל כפתור סיבוב
               }

               locationInCircle = false;
               tempInCircle = false;
           }

       }

       public bool IsTie() // פעולה שבודקת אם מצב הלוח הוא תיקו
       {
           for (int i = 0; i < 6; i++)
           {
               for (int j = 0; j < 6; j++)
               {
                   if (allCells[i, j].GetStatus() == Cell.Status.Empty) //אם קיים תא ריק החזר שקר
                       return false;
               }
           }
           return true; //אם כל התאים מלאים החזר אמת
       }
       
        public void DrawStatus(Graphics g) // מציירת את מצב בלוח לאחר מחיקת הלוח
        {
           for (int i = 0; i < 2; i++)
           {
               for (int j = 0; j < 2; j++)
               {
                   this.quarters[i, j].DrawStatus(g);
               }
           }
        }

        public bool IsFiveEqualStatus(Cell c1, Cell c2, Cell c3, Cell c4,Cell c5) // האם חמישה תאים זהים בתכולתם - האם כולם שחורים או כולם לבנים 
        {
            if ((c1.GetStatus() == c2.GetStatus()) && (c1.GetStatus() == c3.GetStatus()) && 
                (c1.GetStatus() == c4.GetStatus()) && (c1.GetStatus() == c4.GetStatus()) && 
                (c1.GetStatus() == c5.GetStatus()) && (c1.GetStatus()!=Cell.Status.Empty))
                return true;
            else
                return false;
        }
        
        


        public void DrawVictoryLineInLine(Cell cStart, Cell cEnd)    ///מצייר את הקו ניצחון בשורה
        {
            g.DrawLine(this.penWin, cStart.GetX(), cStart.GetY() + cStart.GetHeight() / 2, cEnd.GetX() + cEnd.GetWidth(), cEnd.GetY() + cEnd.GetHeight() / 2);
        }

        public void DrawVictoryLineInColumn(Cell cStart, Cell cEnd)   /// בטור מצייר את הקו ניצחון
        {
            g.DrawLine(this.penWin, cStart.GetX() + cStart.GetWidth() / 2, cStart.GetY(), cEnd.GetX() + cEnd.GetWidth() / 2, cEnd.GetY()+ cEnd.GetHeight());
        }

        public void DrawVictoryLineInSlantLeft(Cell cStart, Cell cEnd)   ///  באלכסון שמאלי מצייר את הקו ניצחון
        {
            g.DrawLine(this.penWin, cStart.GetX(), cStart.GetY(), cEnd.GetX() + cEnd.GetWidth(), cEnd.GetY() + cEnd.GetHeight());
        }

        public void DrawVictoryLineInSlantRight(Cell cStart, Cell cEnd)  //  באלכסון ימני מצייר את הקו ניצחון 
        {
            g.DrawLine(this.penWin, cStart.GetX()+ cStart.GetWidth(), cStart.GetY(), cEnd.GetX() , cEnd.GetY() + cEnd.GetHeight());
        }





        public bool IsVictoryInOneLineSpecificColor(Cell.Status statusCompetitor, int IndexLine)// האם יש ניצחון בשורה אחת בצבע מסוים
        {
            if ((IsFiveEqualStatus(this.allCells[IndexLine, 0], this.allCells[IndexLine, 1], this.allCells[IndexLine, 2], this.allCells[IndexLine, 3], this.allCells[IndexLine, 4]) &&
                (this.allCells[IndexLine, 0].GetStatus() == statusCompetitor)))
            {
                DrawVictoryLineInLine(this.allCells[IndexLine, 0], this.allCells[IndexLine, 4]);
                return true;
            }
            else if (IsFiveEqualStatus(this.allCells[IndexLine, 1], this.allCells[IndexLine, 2], this.allCells[IndexLine, 3], this.allCells[IndexLine, 4], this.allCells[IndexLine, 5]) &&
                (this.allCells[IndexLine, 1].GetStatus() == statusCompetitor))
            {
                DrawVictoryLineInLine(this.allCells[IndexLine, 1], this.allCells[IndexLine, 5]);
                return true;
            }
            else
                return false;


        }
        public bool IsVictoryInAllLines(Cell.Status statusCompetitor) // האם יש ניצחון בצבע מסוים באחת מן השורות בלוח
        {
            for (int i = 0; i < 6; i++)
            {
                if (IsVictoryInOneLineSpecificColor(statusCompetitor, i))
                    return true;
            }
            return false;
        }


        public bool IsVictoryInOneColumnSpecificColor(Cell.Status statusCompetitor, int IndexColumn) // האם יש ניצחון בטור אחד בצבע מסוים
        {
            if (IsFiveEqualStatus(this.allCells[0, IndexColumn], this.allCells[1, IndexColumn], this.allCells[2, IndexColumn], this.allCells[3, IndexColumn], this.allCells[4, IndexColumn]) &&
                (this.allCells[0, IndexColumn].GetStatus() == statusCompetitor))
            {
                DrawVictoryLineInColumn(this.allCells[0, IndexColumn], this.allCells[4, IndexColumn]);
                return true;
            }
            else if ((IsFiveEqualStatus(this.allCells[1, IndexColumn], this.allCells[2, IndexColumn], this.allCells[3, IndexColumn], this.allCells[4, IndexColumn], this.allCells[5, IndexColumn])) &&
                (this.allCells[1, IndexColumn].GetStatus() == statusCompetitor))
            {
                DrawVictoryLineInColumn(this.allCells[1, IndexColumn], this.allCells[5, IndexColumn]);
                return true;
            }
            else
                return false;
          
  
        }
        public bool IsVictoryInAllColumns(Cell.Status statusCompetitor) //האם יש ניצחון באחד מן הטורים בלוח בצבע מסוים
        {
            for (int j = 0; j < 6; j++)
            {
                if (IsVictoryInOneColumnSpecificColor(statusCompetitor,j))
                    return true;
            }
            return false;
        }

        
        public bool IsVictoryLeftMainUpSlant(Cell.Status statusCompetitor) // האם יש ניצחון באלכסון השמאלי הראשי העליון
        {
            if (IsFiveEqualStatus(this.allCells[0, 0], this.allCells[1, 1], this.allCells[2, 2], this.allCells[3, 3], this.allCells[4, 4]))
            {
                if (statusCompetitor == this.allCells[0, 0].GetStatus())
                {
                    DrawVictoryLineInSlantLeft(this.allCells[0, 0], this.allCells[4, 4]);
                    return true;
                }
            }
            return false;
        }

        public bool IsVictoryLeftMainDownSlant(Cell.Status statusCompetitor) //האם יש ניצחון באלכסון השמאלי ראשי תחתון
        {
            if (IsFiveEqualStatus(this.allCells[1, 1], this.allCells[2, 2], this.allCells[3, 3], this.allCells[4, 4], this.allCells[5, 5]))
            {
                if (statusCompetitor == this.allCells[1, 1].GetStatus())
                {
                    DrawVictoryLineInSlantLeft(this.allCells[1, 1], this.allCells[5, 5]);
                    return true;
                }
            }
            return false;
        }

        public bool IsVictoryLeftDownSlant(Cell.Status statusCompetitor) //האם יש ניצחון באלכסון השמאלי משני תחתון
        {
            if (IsFiveEqualStatus(this.allCells[1, 0], this.allCells[2, 1], this.allCells[3, 2], this.allCells[4, 3], this.allCells[5, 4]))
            {
                if (statusCompetitor == this.allCells[1, 0].GetStatus())
                {
                    DrawVictoryLineInSlantLeft(this.allCells[1, 0], this.allCells[5, 4]);
                    return true;
                }
            }
            return false;
        }

        public bool IsVictoryLeftUpSlant(Cell.Status statusCompetitor) //האם יש ניצחון באלכסון השמאלי משני עליון
        {
            if (IsFiveEqualStatus(this.allCells[0, 1], this.allCells[1, 2], this.allCells[2, 3], this.allCells[3, 4], this.allCells[4, 5]))
            {
                if (statusCompetitor == this.allCells[0, 1].GetStatus())
                {
                    DrawVictoryLineInSlantLeft(this.allCells[0, 1], this.allCells[4, 5]);
                    return true;
                }
            }
            return false;
        }

        public bool IsVictoryLeftSlant(Cell.Status statusCompetitor) //האם יש ניצחון באחד מהאלכסונים השמאליים הנוטים שמאלה
        {
            if (IsVictoryLeftMainUpSlant(statusCompetitor) || IsVictoryLeftMainDownSlant(statusCompetitor) ||
                IsVictoryLeftUpSlant(statusCompetitor) || IsVictoryLeftDownSlant(statusCompetitor))
                return true;
            else
                return false;
        }


        public bool IsVictoryRightMainUpSlant(Cell.Status statusCompetitor) //האם יש ניצחון באלכסון הראשי ימני עליון
        {
            if (IsFiveEqualStatus(this.allCells[0, 5], this.allCells[1, 4], this.allCells[2, 3], this.allCells[3, 2], this.allCells[4, 1]))
            {
                if (statusCompetitor == this.allCells[0, 5].GetStatus())
                {
                    DrawVictoryLineInSlantRight(this.allCells[0, 5], this.allCells[4, 1]);
                    return true;
                }
            }
            return false;
        }

        public bool IsVictoryRightMainDownSlant(Cell.Status statusCompetitor) //האם יש ניצחון באלכסון הראשי ימני תחתון
        {
            if (IsFiveEqualStatus(this.allCells[1, 4], this.allCells[2, 3], this.allCells[3, 2], this.allCells[4, 1], this.allCells[5, 0]))
            {
                if (statusCompetitor == this.allCells[1, 4].GetStatus())
                {
                    DrawVictoryLineInSlantRight(this.allCells[1, 4], this.allCells[5, 0]);
                    return true;
                }
            }
            return false;
        }

        public bool IsVictoryRightDownSlant(Cell.Status statusCompetitor) //האם יש ניצחון באלכסון המשני ימני תחתון
        {
            if (IsFiveEqualStatus(this.allCells[1, 5], this.allCells[2, 4], this.allCells[3, 3], this.allCells[4, 2], this.allCells[5, 1]))
            {
                if (statusCompetitor == this.allCells[1, 5].GetStatus())
                {
                    DrawVictoryLineInSlantRight(this.allCells[1, 5], this.allCells[5, 1]);
                    return true;
                }
            }
            return false;
        }

        public bool IsVictoryRightUpSlant(Cell.Status statusCompetitor) //האם יש ניצחון באלכסון המשני ימני עליון
        {
            if (IsFiveEqualStatus(this.allCells[0, 4], this.allCells[1, 3], this.allCells[2, 2], this.allCells[3, 1], this.allCells[4, 0]))
            {
                if (statusCompetitor == this.allCells[0, 4].GetStatus())
                {
                    DrawVictoryLineInSlantRight(this.allCells[0, 4], this.allCells[4, 0]);
                    return true;
                }
            }
            return false;
        }

        public bool IsVictoryRightSlant(Cell.Status statusCompetitor) // האם יש ניצחון באחד מהאלכסונים הנוטים ימינה
        {
            if (IsVictoryRightMainUpSlant(statusCompetitor) || IsVictoryRightMainDownSlant(statusCompetitor) ||
                IsVictoryRightUpSlant(statusCompetitor) || IsVictoryRightDownSlant(statusCompetitor))
                return true;
            else
                return false;
        }


        public bool IsVictoryAllSlants(Cell.Status statusCompetitor) //האם יש ניצחון באחד מן האלכסונים בכלל
        {
            if (IsVictoryRightSlant(statusCompetitor) || IsVictoryLeftSlant(statusCompetitor))
                return true;
            else
                return false;
        }


        public bool IsTotalVictory(Cell.Status statusCompetitor) //האם יש ניצחון או בשורה או בטור או באלכסון
        {
            if (IsVictoryAllSlants(statusCompetitor) || IsVictoryInAllLines(statusCompetitor) || IsVictoryInAllColumns(statusCompetitor)) 
                return true;
            else
                return false;
        }

        public void WinLoseTieForms(int time, string playerName) //הפעולה בודקת אם הסתיים המשחק ובהתאם פותחת טופס ניצחון/ הפסד או תיקו
        {
            if (IsTotalVictory(Cell.Status.White))   //בדוק ניצחון
            {
                        //אם השחקן מנצח אז רשום את הזמן שלקח לנצח ושם השחקן בקובץ טקסט
               
                File.AppendAllText("timeTable.txt", string.Format(time.ToString() + Environment.NewLine));
                File.AppendAllText("timeTable.txt", string.Format(playerName + Environment.NewLine));

                turn = Turn.gameOver;
                turnClick = TurnClick.gameOver;
                System.Threading.Thread.Sleep(700);//זה מאט את התגובה של המחשב
                formVictory.Show();
            }
            else if (IsTotalVictory(Cell.Status.Black))
            {
                turn = Turn.gameOver;
                turnClick = TurnClick.gameOver;
                System.Threading.Thread.Sleep(700);//זה מאט את התגובה של המחשב 
                formDefeat.Show();
            }

            if (IsTie())    // בודק אם תיקו
            {
                turn = Turn.gameOver;
                turnClick = TurnClick.gameOver;
                System.Threading.Thread.Sleep(700);//זה מאט את התגובה של המחשב
                formTie.Show();
            }
        }

        public int RecursiaLengthLine(int i, int j, Cell.Status status)  ///////////////רקורסיה! שמחשבת אורך של שורה בצבע מסוים שיוצא מתא מסוים////////////
        {
            if (status == Cell.Status.Black)
            {
                if (j == this.allCells.GetLength(1) || this.allCells[i, j].GetStatus() != Cell.Status.Black)
                    return 0;
                return 1 + RecursiaLengthLine(i, j + 1, status);
            }

            else if (status == Cell.Status.White)
            {
                if (j == this.allCells.GetLength(1) || this.allCells[i, j].GetStatus() != Cell.Status.White)
                    return 0;
                return 1 + RecursiaLengthLine(i, j + 1, status);
            }
            else return -1;
        }

        public LineSequence MaxFreeSequanceLine(Cell.Status status)  //מחזיר את הרצף הכי ארוך שבשורה בצבע מסוים
        {
            LineSequence Ls = new LineSequence(0, false, false);
            int max = 0;
            int oreh = 0;
            bool blockedRight = false;
            bool blockedLeft = false;

            for (int i = 0; i < this.allCells.GetLength(0); i++)
            {
                
                for (int j = 0; j < this.allCells.GetLength(1); j++) //עובר תא תא בלוח
                {

                    oreh = RecursiaLengthLine(i, j, status); //בודק את אורך הרצף שיוצא מתא ספציפי בשורה

                    if (status == Cell.Status.Black)
                    {                                     ///בדיקה אם הרצף חסום מצידיו
                        if (j + oreh == 6 || this.allCells[i, j + oreh].GetStatus() == Cell.Status.White)
                            blockedRight = true;

                        if (j == 0 || this.allCells[i, j - 1].GetStatus() == Cell.Status.White)
                            blockedLeft = true;
                    }
                    
                    if (status == Cell.Status.White)
                    {                                           ///בדיקה אם הרצף חסום מצידיו
                        if (j + oreh == 6 || this.allCells[i, j + oreh].GetStatus() == Cell.Status.Black)
                            blockedRight = true;

                        if (j == 0 || this.allCells[i, j - 1].GetStatus() == Cell.Status.Black)
                            blockedLeft = true;
                    }

                    if (oreh > max) //אם האורך הוא הגדול עד כה הכנס למשתנה הרצף הכי ארוך
                    {
                        max = oreh;
                        Ls.SetOreh(oreh);
                        Ls.SetBlockedRight(blockedRight);
                        Ls.SetBlockedLeft(blockedLeft);
                    }

                    oreh = 0;

                    blockedRight = false;     
                    blockedLeft = false;
    
             
                }
            }
            return Ls;
        }


        public int RecursiaLengthToor(int i, int j, Cell.Status status)  //////////////רקורסיה! שמחשבת אורך של טור שיוצא מתא מסוים בצבע מסוים////////////
        {
            if (status == Cell.Status.Black)
            {
                if (i == this.allCells.GetLength(0) || this.allCells[i, j].GetStatus() != Cell.Status.Black)
                    return 0;
                return 1 + RecursiaLengthToor(i + 1, j, status);
            }
            else if (status == Cell.Status.White)
            {
                if (i == this.allCells.GetLength(0) || this.allCells[i, j].GetStatus() != Cell.Status.White)
                    return 0;
                return 1 + RecursiaLengthToor(i + 1, j, status);
            }
            else return -1;
        }


        public ToorSequence MaxFreeSequanceToor(Cell.Status status)   //מחזיר את הרצף הכי ארוך שבטור
        {
            ToorSequence Ts = new ToorSequence(0, false, false);
            int max = 0;
            int oreh = 0;
            bool blockedUp = false;
            bool blockedDown = false;

            for (int j = 0; j < this.allCells.GetLength(1); j++)
            {
                for (int i = 0; i < this.allCells.GetLength(0); i++) ///עוברת על כל תאי הלוח
                {

                    oreh = RecursiaLengthToor(i, j, status);  //בודק את אורך הרצף שיוצא מתא ספציפי בשורה

                    if (status == Cell.Status.Black)
                    {                                              ///בדיקה אם הרצף חסום מצידיו
                        if (i + oreh == 6 || this.allCells[i + oreh, j].GetStatus() == Cell.Status.White)
                            blockedDown = true;

                        if (i == 0 || this.allCells[i - 1, j].GetStatus() == Cell.Status.White)
                            blockedUp = true;
                    }
                    
                    if (status == Cell.Status.White)
                    {                                   ///בדיקה אם הרצף חסום מצידיו
                        if (i + oreh == 6 || this.allCells[i + oreh, j].GetStatus() == Cell.Status.Black)
                            blockedDown = true;

                        if (i == 0 || this.allCells[i - 1, j].GetStatus() == Cell.Status.Black)
                            blockedUp = true;
                    }

                    if (oreh > max)                    //אם האורך הוא הגדול עד כה הכנס למשתנה הרצף הכי ארוך
                    {
                        max = oreh;
                        Ts.SetOreh(oreh);
                        Ts.SetBlockedUp(blockedUp);
                        Ts.SetBlockedDown(blockedDown);
                    }


                    oreh = 0;

                    blockedUp = false;
                    blockedDown = false;
 
                    
                }
            }
            return Ts;
        }


        public int RecursiaLengthLeftSlant(int i, int j, Cell.Status status)  /////////רקורסיה! שמחשבת אורך של אלכסון שמאלי צבע מסוים שיוצא מתא מסוים////////////
        {
            if (status == Cell.Status.Black)
            {
                if (i == this.allCells.GetLength(0) || j == this.allCells.GetLength(1) || this.allCells[i, j].GetStatus() != Cell.Status.Black)
                    return 0;
                return 1 + RecursiaLengthLeftSlant(i + 1, j + 1, status);
            }

            else if (status == Cell.Status.White)
            {
                if (i == this.allCells.GetLength(0) || j == this.allCells.GetLength(1) || this.allCells[i, j].GetStatus() != Cell.Status.White)
                    return 0;
                return 1 + RecursiaLengthLeftSlant(i + 1, j + 1, status);
            }
            else return -1;
        }

        public int RecursiaLengthRightSlant(int i, int j, Cell.Status status)  //////////רקורסיה! שמחשבת אורך של אלכסון ימני צבע מסוים שיוצא מתא מסוים////////////
        {
            if (status == Cell.Status.Black)
            {
                if (i == this.allCells.GetLength(0) || j == -1 || this.allCells[i, j].GetStatus() != Cell.Status.Black)
                    return 0;
                return 1 + RecursiaLengthRightSlant(i + 1, j - 1, status);
            }
            else if (status == Cell.Status.White)
            {
                if (i == this.allCells.GetLength(0) || j == -1 || this.allCells[i, j].GetStatus() != Cell.Status.White)
                    return 0;
                return 1 + RecursiaLengthRightSlant(i + 1, j - 1, status);
            }
            else return -1;
        }



        public SlantLeftSequence MaxFreeSequenceSlantLeft(Cell.Status status) //מחזיר את הרצף הכי ארוך באלכסון שמאלי
        {
            SlantLeftSequence Sls = new SlantLeftSequence(0, false, false);
            int max = 0;
            int oreh = 0;
            bool blockedLeftUp = false;
            bool blockedRightDown = false;

            for (int i = 0; i < this.allCells.GetLength(0); i++)
            {
                for (int j = 0; j < this.allCells.GetLength(1); j++)   //עובר על כל תאי הלוח
                {
                    if (status == Cell.Status.Black)
                    {                                             ///בדיקה אם הרצף חסום מצידיו
                        if (i == 0 || j == 0 || this.allCells[i - 1, j - 1].GetStatus() == Cell.Status.White)
                            blockedLeftUp = true;

                        oreh = RecursiaLengthLeftSlant(i, j, status);   //בודק את אורך הרצף שיוצא מתא ספציפי בשורה

                        if ((i + oreh) == 6 || (j + oreh) == 6 || this.allCells[i + oreh, j + oreh].GetStatus() == Cell.Status.White)
                            blockedRightDown = true;

                    }

                    if (status == Cell.Status.White)
                    {                                   ///בדיקה אם הרצף חסום מצידיו
                        if (i == 0 || j == 0 || this.allCells[i - 1, j - 1].GetStatus() == Cell.Status.Black)
                            blockedLeftUp = true;

                        oreh = RecursiaLengthLeftSlant(i, j, status);    //בודק את אורך הרצף שיוצא מתא ספציפי בשורה

                        if ((i + oreh) == 6 || (j + oreh) == 6 || this.allCells[i + oreh, j + oreh].GetStatus() == Cell.Status.Black)
                            blockedRightDown = true;
                    }
                          

                    if (oreh > max)                  //אם האורך הוא הגדול עד כה הכנס למשתנה הרצף הכי ארוך
                    {
                        max = oreh;
                        Sls.SetOreh(oreh);
                        Sls.SetBlockedLefttUp(blockedLeftUp);
                        Sls.SetBlockedRightDown(blockedRightDown);
                    }
                                   
                    oreh = 0;
                    blockedLeftUp = false;
                    blockedRightDown = false;   
                }
            }

            return Sls;

        }

        public SlantRightSequence MaxFreeSequenceSlantRight(Cell.Status status) //מחזיר את הרצף הכי ארוך באלכסון ימני
        {
            SlantRightSequence Srs = new SlantRightSequence(0, false, false);
            int max = 0;
            int oreh = 0;
            bool blockedLeftDown = false;
            bool blockedRightUp = false;


            for (int i = 0; i < this.allCells.GetLength(0); i++)
            {
                for (int j = 0; j< this.allCells.GetLength(1); j++)    //עובר על כל תאי הלוח
                {
                    if (status == Cell.Status.Black)
                    {
                        if (i == 0 || j == 5 || this.allCells[i - 1, j + 1].GetStatus() == Cell.Status.White)
                            blockedRightUp = true;

                        oreh = RecursiaLengthRightSlant(i, j, status);    //בודק את אורך הרצף שיוצא מתא ספציפי בשורה

                        if ((i + oreh) == 6 || (j - oreh) == -1 || this.allCells[i + oreh, j - oreh].GetStatus() == Cell.Status.White)
                            blockedLeftDown = true;
                    }

                    if (status == Cell.Status.White)
                    {
                        if (i == 0 || j == 5 || this.allCells[i - 1, j + 1].GetStatus() == Cell.Status.Black)
                            blockedRightUp = true;

                        oreh = RecursiaLengthRightSlant(i, j, status);   //בודק את אורך הרצף שיוצא מתא ספציפי בשורה

                        if ((i + oreh) == 6 || (j - oreh) == -1 || this.allCells[i + oreh, j - oreh].GetStatus() == Cell.Status.Black)
                            blockedLeftDown = true;
                    }

                    if (oreh > max)                //אם האורך הוא הגדול עד כה הכנס למשתנה הרצף הכי ארוך
                    {
                        max = oreh;
                        Srs.SetOreh(oreh);
                        Srs.SetBlockedRightUp(blockedRightUp);
                        Srs.SetBlockedLeftDown(blockedLeftDown);
                    }

                    blockedRightUp = false;
                    blockedLeftDown = false;

                    
                }
            }

            return Srs;

        }

         
        public Board(Board board)         //פעולה בונה מעתיקה
        {

            this.x = board.GetX();
            this.y = board.GetY();
            this.width = board.GetWidth();
            this.height = board.GetHeight();
            this.quarters = new Quarter[2, 2];
            this.quarters[0, 0] = new Quarter(board.GetQuarters()[0, 0]);
            this.quarters[0, 1] = new Quarter(board.GetQuarters()[0, 1]);
            this.quarters[1, 0] = new Quarter(board.GetQuarters()[1, 0]);
            this.quarters[1, 1] = new Quarter(board.GetQuarters()[1, 1]); 
            
            this.rightUpTurnRight = board.GetRightUpTurnRight();
            this.rightUpTurnLeft = board.GetRightUpTurnLeft();
            this.rightDownTurnRight = board.GetRightDownTurnRight();
            this.rightDownTurnLeft = board.GetRightDownTurnLeft();
            this.leftDownTurnRight = board.GetLeftDownTurnRight();
            this.leftDownTurnLeft = board.GetLeftDownTurnLeft();
            this.leftUpTurnRight = board.GetLeftUpTurnRight();
            this.leftUpTurnLeft = board.GetLeftUpTurnLeft();
            allCells = new Cell[6, 6];
            for (int i = 0; i < 3; i++)               ///מעדכן את איברי המערך הכולל אחרי כל תור
            {
                for (int j = 0; j < 3; j++)
                {
                    allCells[i, j]= board.GetQuarters()[0,0].GetNineCells()[i, j];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    allCells[i, j] = board.GetQuarters()[0, 1].GetNineCells()[i, j - 3];
                }
            }
            for (int i = 3; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    allCells[i, j] = board.GetQuarters()[1, 0].GetNineCells()[i - 3, j];
                }
            }
            for (int i = 3; i < 6; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    allCells[i, j] = board.GetQuarters()[1, 1].GetNineCells()[i - 3, j - 3];
                }
            }

        }

        public void CopyQuartersToAllCells() //מעדכן את המערך של כל תאי הלוח מהנתונים שברבעונים
        {
            for (int i = 0; i < 3; i++)               ///מעדכן את איברי המערך הכולל אחרי כל תור
            {
                for (int j = 0; j < 3; j++)
                {
                    allCells[i, j] = this.quarters[0, 0].GetNineCells()[i, j];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    allCells[i, j] = this.quarters[0, 1].GetNineCells()[i, j - 3];
                }
            }
            for (int i = 3; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    allCells[i, j] = this.quarters[1, 0].GetNineCells()[i - 3, j];
                }
            }
            for (int i = 3; i < 6; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    allCells[i, j] = this.quarters[1, 1].GetNineCells()[i - 3, j - 3];
                }
            }

        }



        public bool Inside(int x, int y) // checks if a point is in the board
        {
            if ((x > this.x) && (x < this.x + this.width) && (y > this.y) && (y < this.y + this.height))
            {
                return true;
            }
            return false;
        }

        public void SetCellsRadius(int width) // מגדיר את רדיוס התאים בלוח
        {
            for (int i = 0; i < this.allCells.GetLength(0); i++)
            {
                for (int j = 0; j < this.allCells.GetLength(1); j++)
                {
                    allCells[i, j].SetRadius((int)(width / 12));  ///מעדכן את הרדיוס
                }
            }
        }

        public void SetCenterCellsRadius(int x, int y, int width, int height) 
        {
            int difX1 = (int)(this.width / 2);
            int difY1 = (int)(this.height / 2);

            for (int i = 0; i < quarters.GetLength(0); i++)
            {
                for (int j = 0; j < quarters.GetLength(1); j++)
                {
                    this.quarters[0, 0].SetAll(this.x, this.y, difX1, difY1);     ////מעדכן את גודל הרבעונים
                    this.quarters[0, 1].SetAll(this.x + difX1, this.y, difX1, difY1);
                    this.quarters[1, 0].SetAll(this.x, this.y + difY1, difX1, difY1);
                    this.quarters[1, 1].SetAll(this.x + difX1, this.y + difY1, difX1, difY1);
                }
            }
        }

        public void SetTurn(Board.Turn turn)
        {
            this.turn = turn;
        }
        public void SetX(int x)
        {
            this.x = x;
        }
        public void SetY(int y)
        {
            this.y = y;
        }
        public void SetWidth(int width)
        {
            this.width = width;
        }
        public void SetHeight(int height)
        {
            this.height = height;
        }
        public void SetTurnClick(TurnClick turnClick)
        {
            this.turnClick = turnClick;
        }
        public void SetLocationArrows()
        {
            rightUpTurnRight.SetLocationXYWidthHeight((int)(this.allCells[1, 5].GetX() * 1.19), this.allCells[1, 5].GetY(), 85, 85);
            rightUpTurnLeft.SetLocationXYWidthHeight((int)(this.allCells[0, 4].GetX() * 0.97), (int)(this.allCells[0, 4].GetY() * 0.2), 105, 105);
            rightDownTurnRight.SetLocationXYWidthHeight((int)(this.allCells[4, 5].GetX() * 1.19), this.allCells[4, 5].GetY(), 90, 90);
            rightDownTurnLeft.SetLocationXYWidthHeight((int)(this.allCells[5, 4].GetX() * 0.985), (int)(this.allCells[5, 4].GetY() * 1.16), 95, 95);
            leftDownTurnRight.SetLocationXYWidthHeight((int)(this.allCells[5, 1].GetX() * 0.98), (int)(this.allCells[5, 1].GetY() * 1.165), 90, 90);
            leftDownTurnLeft.SetLocationXYWidthHeight((int)(this.allCells[4, 0].GetX() * 0.3), (int)(this.allCells[4, 0].GetY() * 0.97), 95, 95);
            leftUpTurnRight.SetLocationXYWidthHeight((int)(this.allCells[1, 0].GetX() * 0.3), (int)(this.allCells[1, 0].GetY() * 0.98), 87, 87);
            leftUpTurnLeft.SetLocationXYWidthHeight((int)(this.allCells[0, 1].GetX() * 0.98), (int)(this.allCells[0, 1].GetY() * 0.21), 97, 97);
        }



        public RotationArrow GetRightUpTurnRight()
        {
            return this.rightUpTurnRight;
        }
        public RotationArrow GetRightUpTurnLeft()
        {
            return this.rightUpTurnLeft;
        }
        public RotationArrow GetRightDownTurnRight()
        {
            return this.rightDownTurnRight;
        }
        public RotationArrow GetRightDownTurnLeft()
        {
            return this.rightDownTurnLeft;
        }
        public RotationArrow GetLeftDownTurnRight()
        {
            return this.leftDownTurnRight;
        }
        public RotationArrow GetLeftDownTurnLeft()
        {
            return this.leftDownTurnLeft;
        }
        public RotationArrow GetLeftUpTurnRight()
        {
            return this.leftUpTurnRight;
        }
        public RotationArrow GetLeftUpTurnLeft()
        {
            return this.leftUpTurnLeft;
        }

        public Turn GetTurn()
        {
            return this.turn;
        }
            
        public TurnClick GetTurnClick()
        {
            return turnClick;
        }
        public Quarter[,] GetQuarters()
        {
            return this.quarters;
        }

        public Cell[,] GetAllCells()
        {
            return this.allCells;
        }
        public int GetX()
        {
            return this.x;
        }
        public int GetY()
        {
            return this.y;
        }
        public int GetHeight()
        {
            return this.height;
        }
        public int GetWidth()
        {
            return this.width;
        }




    }
}