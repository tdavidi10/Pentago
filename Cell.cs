using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace Pentago_Tamir_Davidi
{
    class Cell
    {
        public enum Status { Black, White, Empty }; //תכולת התא
        private Status status;
        private int x; //איקס ששל העוגן 
        private int y; //וואי של העוגן
        private int width; //רוחב התא
        private int height; //גובה התא
        private int radius; //רדיוס העיגול שבתא
        private Point center; //מרכז העיגול שבתא

        public Cell(int x, int y, int width, int height) // constructor function
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.status = Status.Empty;
            this.radius = (int)(width / 2);
            this.center = new Point(x + (int)(width / 2), y + (int)(height / 2));
        }

        public Cell(Cell cell)       //פעולה בונה מעתיקה
        {
            this.x = cell.GetX();
            this.y = cell.GetY(); ;
            this.width = cell.GetWidth();
            this.height = cell.GetHeight();
            this.status = cell.GetStatus();
            this.radius = cell.GetRadius();
            this.center = new Point(cell.GetCenter().X, cell.GetCenter().Y);
        }

        public void Draw(Graphics g, Pen pen)    // Draws the frame of cell
        {
            g.DrawEllipse(pen, this.x, this.y, this.width, this.height);
        }

        public bool IsInCircle(int x, int y) //האם נק בתוך המעגל שבתא
        {
            if (Distance(center.X, center.Y, x, y) <= radius)
                return true;
            return false;

        }

        public double Distance(int x1, int y1, int x2, int y2)      //////מחזירה את המרחק בין שתי נקודות
        {
            return  Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }


        public void PlaceBlack(Graphics g) // fills the cell in black
        {
            g.DrawImage(Properties.Resources.blackGula, this.x, this.y, this.width, this.height);
            this.status = Cell.Status.Black;
        }

        public void PlaceWhite(Graphics g) // fills the cell in white
        {
            g.DrawImage(Properties.Resources.whiteGula, this.x, this.y, this.width, this.height);
            this.status = Cell.Status.White;
        }

        public bool PlaceBall(Graphics g, Board.Turn turn) // places a ball in a cell
        {                                                  //מחזירה שקר כאשר הלחיצה הייתה על תא שיש בו כבר כדורית
            if (this.status != Status.Empty)                   //מוודא שהתא ריק לפני שמציבים בו כדורית
            {                                                // מחזירה אמת כאשר הלחיצה הייתה על תא ריק
                MessageBox.Show("click only in empty cells");
                return false;
            }
            else
            {
                if (turn == Board.Turn.White)
                {
                    this.PlaceWhite(g);
                }
                if (turn == Board.Turn.Black)
                {
                    this.PlaceBlack(g);
                }
                return true;
            }
        }

        public void DrawStatus(Graphics g)           ///////////משחזרת את תכולת התא לאחר שנמחק
        {
            if (this.status == Cell.Status.Black)
            {
                PlaceBlack(g);
            }
            if (this.status == Cell.Status.White)
            {
                PlaceWhite(g);
            }
        }

        

        public bool Inside(int x, int y) // checks if a point is in the cell
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
            this.radius = (int)(width / 2);
            this.center = new Point(x + (int)(width / 2), y + (int)(height / 2));
        }

        public void SetRadius(int radius) //פעולות מעדכנות
        {
            this.radius = radius;
        }

        public void SetCenter(Point center)
        {
            this.center = center;
        }

        public void SetStatus(Status status)
        {
            this.status = status;
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


        public Status GetStatus()       ////פעולות מאחזרות
        {
            return this.status;
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
        public int GetRadius()
        {
            return this.radius;
        }
        public Point GetCenter()
        {
            return this.center;
        }












    }
}
