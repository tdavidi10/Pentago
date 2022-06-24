using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;

namespace Pentago_Tamir_Davidi
{
    class Quarter
    {
        private int x; //איקס של עוגן
        private int y; //וואי של עוגן
        private int width; //רוחב רבעון
        private int height; // גובה רבעון
        private Cell[,] nineCells; //מערך דוד מימדי של תשעת התאים שברבעון
        private FormGame formGame; //טופס המשחק
        private Cell.Status[,] nineCellsStatus; //מערך דו מימדי של תכולת תשעת התאים ברבעון
        

        public Quarter(int x, int y, int width, int height, FormGame formGame)  // constructor function
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.formGame = formGame;
            this.nineCellsStatus = new Cell.Status[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.nineCellsStatus[i, j] = Cell.Status.Empty; //מגדיר תחילה את כל התאים ברבעון כריקים
                }

            }
            nineCells = new Cell[3, 3];
            int difX = (int)(this.width) / 3;  // width of a cell
            int difY = (int)(this.height) / 3; // height of a cell
            this.nineCells[0, 0] = new Cell(this.x, this.y, difX, difY);
            this.nineCells[0, 1] = new Cell(this.x+difX, this.y, difX, difY);
            this.nineCells[0, 2] = new Cell(this.x + 2*difX, this.y, difX, difY);
            this.nineCells[1, 0] = new Cell(this.x, this.y+ difY, difX, difY);
            this.nineCells[1, 1] = new Cell(this.x+ difX , this.y + difY, difX, difY);
            this.nineCells[1, 2] = new Cell(this.x + 2*difX, this.y + difY, difX, difY);
            this.nineCells[2, 0] = new Cell(this.x, this.y + 2*difY, difX, difY);
            this.nineCells[2, 1] = new Cell(this.x+difX, this.y + 2 * difY, difX, difY);
            this.nineCells[2, 2] = new Cell(this.x + 2*difX, this.y + 2 * difY, difX, difY);
        }

        public Quarter(Quarter quarter)    //פעולה בונה מעתיקה
        {
            this.x = quarter.GetX();
            this.y = quarter.GetY();
            this.width = quarter.GetWidth();
            this.height = quarter.GetHeight();
            this.nineCellsStatus = new Cell.Status[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.nineCellsStatus[i, j] = quarter.GetNineCellsStatus()[i, j];
                }

            }

            this.nineCells= new Cell[3, 3];
            this.nineCells[0, 0] = new Cell(quarter.GetNineCells()[0, 0]);
            this.nineCells[0, 1] = new Cell(quarter.GetNineCells()[0, 1]);
            this.nineCells[0, 2] = new Cell(quarter.GetNineCells()[0, 2]);
            this.nineCells[1, 0] = new Cell(quarter.GetNineCells()[1, 0]);
            this.nineCells[1, 1] = new Cell(quarter.GetNineCells()[1, 1]);
            this.nineCells[1, 2] = new Cell(quarter.GetNineCells()[1, 2]);
            this.nineCells[2, 0] = new Cell(quarter.GetNineCells()[2, 0]);
            this.nineCells[2, 1] = new Cell(quarter.GetNineCells()[2, 1]);
            this.nineCells[2, 2] = new Cell(quarter.GetNineCells()[2, 2]);

        }

        public void Draw(Graphics g, Pen penBoardFrame,Pen penCells) //פעולה המציירת את הרבעון
        {
            int difX1 = (int)(this.width) / 3; //רוחב ואורך כל תא ברבעון הוא שליש מאורך ורוחב הרבעון עצמו
            int difY1 = (int)(this.height) / 3;
            for (int i = 0; i < nineCells.GetLength(0); i++)
            {
                for (int j = 0; j < nineCells.GetLength(1); j++) //הגדרת אורכי ורוחבי התאים ברבעון
                {
                    this.nineCells[0, 0].SetAll(this.x, this.y, difX1, difY1);
                    this.nineCells[0, 1].SetAll(this.x + difX1, this.y, difX1, difY1);
                    this.nineCells[0, 2].SetAll(this.x + 2 * difX1, this.y, difX1, difY1);
                    this.nineCells[1, 0].SetAll(this.x, this.y + difY1, difX1, difY1);
                    this.nineCells[1, 1].SetAll(this.x + difX1, this.y + difY1, difX1, difY1);
                    this.nineCells[1, 2].SetAll(this.x + 2 * difX1, this.y + difY1, difX1, difY1);
                    this.nineCells[2, 0].SetAll(this.x, this.y + 2 * difY1, difX1, difY1);
                    this.nineCells[2, 1].SetAll(this.x + difX1, this.y + 2 * difY1, difX1, difY1);
                    this.nineCells[2, 2].SetAll(this.x + 2 * difX1, this.y + 2 * difY1, difX1, difY1);


                    nineCells[i, j].Draw(g,penCells); //ציור התאים ברבעון
                    nineCells[i, j].DrawStatus(g); //ציור תכולת התאים ברבעון
                }

            }
            g.DrawRectangle(penBoardFrame, this.x, this.y, this.width, this.height); //ציור מסגרת הרבעון
        }

        public void DrawStatus(Graphics g) //ציור תכולת תאי הרבעון
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.nineCells[i, j].DrawStatus(g);
                }
            }
        }

        public void SetNineCellsStatus() //עדכון ערכי  תכולת תשעת התאים ברבעון
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    nineCellsStatus[i, j] = this.nineCells[i, j].GetStatus();
                }
            }
        }

        public void Rotate90DegreesLeft() // rotates the quarter 90 degrees to the left
        {
            SetNineCellsStatus();

            nineCells[0, 0].SetStatus(nineCellsStatus[0, 2]);
            nineCells[0, 1].SetStatus(nineCellsStatus[1, 2]);
            nineCells[0, 2].SetStatus(nineCellsStatus[2, 2]);
            nineCells[1, 0].SetStatus(nineCellsStatus[0, 1]);
           //nineCells[1, 1] stays the same//
            nineCells[1, 2].SetStatus(nineCellsStatus[2, 1]);
            nineCells[2, 0].SetStatus(nineCellsStatus[0, 0]);
            nineCells[2, 1].SetStatus(nineCellsStatus[1, 0]);
            nineCells[2, 2].SetStatus(nineCellsStatus[2, 0]);

            SetNineCellsStatus();
            //DrawStatus(g);
                
        }

        public void Rotate90DegreesRight() // rotates the quarter 90 degrees to the right
        {
            SetNineCellsStatus();

            nineCells[0, 0].SetStatus(nineCellsStatus[2, 0]);
            nineCells[0, 1].SetStatus(nineCellsStatus[1, 0]);
            nineCells[0, 2].SetStatus(nineCellsStatus[0, 0]);
            nineCells[1, 0].SetStatus(nineCellsStatus[2, 1]);
            //nineCells[1, 1] stays the same//
            nineCells[1, 2].SetStatus(nineCellsStatus[0, 1]);
            nineCells[2, 0].SetStatus(nineCellsStatus[2, 2]);
            nineCells[2, 1].SetStatus(nineCellsStatus[1, 2]);
            nineCells[2, 2].SetStatus(nineCellsStatus[0, 2]);

            SetNineCellsStatus();
            //this.DrawStatus(g);
        }

        public bool MouseClick(Graphics g, int x0, int y0, Board.Turn turn) //הפעולה מחזירה אמת כאשר הלחיצה הייתה על תא ריק
        {                                                                    //הפעולה מחזירה שקר כאשר הלחיצה על תא שיש בו כבר כדורית
            for (int i = 0; i < 3; i++)                                      //הפעולה בודקת באיזה תא בה התרחשה הלחיצה ומעבירה אליו את הטיפול בלחיצה
            {
                for (int j = 0; j < 3; j++)
                {
                    if (this.nineCells[i, j].Inside(x0, y0))
                    {
                        return nineCells[i, j].PlaceBall(g, turn);
                    }
                }

            }
            return true; ;
        }

        public bool IsInCellCircle(int x0, int y0) //הפעולה מחזירה אמת כאשר הלחיצה הייתה בתוך העיגול
        {        
                                                            //הפעולה מחזירה שקר כאשר הלחיצה על תא אבל לא בעיגול
            bool isInCircle=false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (nineCells[i, j].Inside(x0, y0))
                    {
                        if (nineCells[i, j].IsInCircle(x0, y0))
                            return true;
                        else
                            isInCircle=false;                        
                    }
                }

            }

            return isInCircle;
            
        }

        public bool Inside(int x, int y) // checks if a point is in the quarter
        {
            if ((x > this.x) && (x < this.x + this.width) && (y > this.y) && (y < this.y + this.height))
            {
                return true;
            }
            return false;
        }

        public void SetAll(int x, int y, int width, int height) //פעולות מעדכנות
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;


            int difX1 = (int)(this.width) / 3;
            int difY1 = (int)(this.height) / 3;

            for (int i = 0; i < nineCells.GetLength(0); i++)
            {
                for (int j = 0; j < nineCells.GetLength(1); j++)
                {
                    nineCells[0, 0].SetAll(this.x, this.y, difX1, difY1);
                    nineCells[0, 1].SetAll(this.x + difX1, this.y, difX1, difY1);
                    nineCells[0, 2].SetAll(this.x + 2 * difX1, this.y, difX1, difY1);
                    nineCells[1, 0].SetAll(this.x, this.y + difY1, difX1, difY1);
                    nineCells[1, 1].SetAll(this.x + difX1, this.y + difY1, difX1, difY1);
                    nineCells[1, 2].SetAll(this.x + 2 * difX1, this.y + difY1, difX1, difY1);
                    nineCells[2, 0].SetAll(this.x, this.y + 2 * difY1, difX1, difY1);
                    nineCells[2, 1].SetAll(this.x + difX1, this.y + 2 * difY1, difX1, difY1);
                    nineCells[2, 2].SetAll(this.x + 2 * difX1, this.y + 2 * difY1, difX1, difY1);

                }
            }
        }


        public void SetNineCells(Cell [,] nineCells)
        {
            this.nineCells = nineCells;
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


        public Cell.Status[,] GetNineCellsStatus() //פעולות מאחזרות
        {
            return this.nineCellsStatus;
        }
        public Cell[,] GetNineCells()
        {
            return this.nineCells;
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
