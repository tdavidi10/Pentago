using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Pentago_Tamir_Davidi
{
    class RotationArrow
    {

        private System.Windows.Forms.PictureBox rotationArrow; //התמונה של החץ סיבוב
        public enum Direction { Left, Right }; //כיוון החץ - ימינה או שמאלה
        private Direction direction;
        private Quarter quarter; // הרבעון אליו משויך החץ
        private Graphics g; //משתנה הגרפיקה
        private FormGame formGame; //טופס המשחק
        private Board board; //הלוח
        private Pen penBoardFrame; //עט ציור מסגרת הלוח
        private Pen penCells; //עט ציור התאים בלוח
        private FormVictory formVictory; //טופס הניצחון
        private FormDefeat formDefeat; //טופס ההפסד
        private FormTie formTie; //טופס התיקו
        private ArtificialIntelligence AI; //הבינה המלאכותית
        static int time; //הזמן מתחילת המשחק
        static string playerName; //שם השחקן




        public RotationArrow(int x, int y, int width, int height, FormGame formGame, Image image, RotationArrow.Direction direction, //פעולה בונה
            Quarter quarter, Graphics g, Board board, Pen penBoardFrame, Pen penCells, FormVictory formVictory, 
            FormDefeat formDefeat, FormTie formTie) //פעולה בונה
        {
            this.formVictory = formVictory;
            this.formDefeat = formDefeat;
            this.formTie = formTie;
            this.formGame = formGame;
            this.penBoardFrame = penBoardFrame;
            this.penCells = penCells;
            this.board = board;
            this.g = g;
            this.quarter=quarter;
            this.direction = direction;
            this.AI = AI = new ArtificialIntelligence(board, g, formVictory, formDefeat);
            this.rotationArrow = new System.Windows.Forms.PictureBox();
            this.rotationArrow.BackColor = Color.Transparent;
            this.rotationArrow.BackgroundImage = image;
            this.rotationArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rotationArrow.Location = new System.Drawing.Point(x, y);
            this.rotationArrow.Name = "pictureBox";
            this.rotationArrow.Size = new System.Drawing.Size(width, height);
            formGame.Controls.Add(this.rotationArrow);
            this.rotationArrow.Visible = true;
            this.rotationArrow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseClick);

        }

        private void FormGame_MouseClick(object sender, MouseEventArgs e) //ניהול הקלקה על לחצן סיבוב
        {
            if (board.GetTurnClick() == Board.TurnClick.rotationArrow)   // אם התור הקלקה של החץ
            {
                this.board.SetTurnClick(Board.TurnClick.cell); 
                this.board.SetTurn(Board.Turn.Black);

                if (direction == RotationArrow.Direction.Right)    //סובב את הלוח בהתאם לכיוון
                    this.quarter.Rotate90DegreesRight();
                else if (direction == RotationArrow.Direction.Left)
                    this.quarter.Rotate90DegreesLeft();

                this.formGame.Refresh();           //צייר את הלוח בהתאם

                board.WinLoseTieForms(time, playerName); //אם המשחק הסתיים הצג טופס מתאים                                

                System.Threading.Thread.Sleep(1000); //עוצר הכל לכמעט שניה כדי שהמשתמש יבין מה קורה מולו

                if (board.GetTurnClick() != Board.TurnClick.gameOver && board.GetTurn() != Board.Turn.gameOver) //אם המשחק לא נגמר
                {
                    AI.RatePlaceRotate(g, this.board, formVictory, formDefeat);   //הבינה מלאכותית מחשבת את הצעד המיטבי עבורה ופועלת

                    board.SetTurn(Board.Turn.White);   // מעביר את התור ללבן - למשתמש

                    board.WinLoseTieForms(time, playerName); //אם המשחק הסתיים הצג טופס מתאים 
                }
  
            }

            else
                MessageBox.Show("click on a cell first"); //אם עכשיו תור על השחקן לקליק על תא תצא הודעת שגיאה


        }

        public void Activate() //פעולה המפעילה את לחצן חץ הסיבוב ללא הקלקה עליו
        {
            this.board.SetTurnClick(Board.TurnClick.cell);
            this.board.SetTurn(Board.Turn.White);

            if (direction == RotationArrow.Direction.Right)
                this.quarter.Rotate90DegreesRight();
            else if (direction == RotationArrow.Direction.Left)
                this.quarter.Rotate90DegreesLeft();

            this.formGame.Refresh();    //מצייר את הלוח בהתאם לאחר הסיבוב של חלק הלוח

            board.WinLoseTieForms(time, playerName);

        }

        public bool Inside(int x, int y) // checks if a point is in the rotation arrow
        {
            if ((x > this.rotationArrow.Location.X) && (x < this.rotationArrow.Location.X + this.rotationArrow.Size.Width) &&
                (y > this.rotationArrow.Location.Y) && (y < this.rotationArrow.Location.Y + this.rotationArrow.Size.Height))
            {
                return true;
            }
            else
                return false;
        }

        public void SetLocationXYWidthHeight(int x, int y, int width, int height) //פעולה מעדכנת
        {
            this.rotationArrow.Location = new System.Drawing.Point(x, y);
            this.rotationArrow.Size = new System.Drawing.Size(width, height);
        }

        public static void SetTime(int time0) //מגדירה את זמן המשחק כסטטי
        {
            time = time0;
        }

        public static void SetPlayerName(string playerName0) //מגדריה את שם השחקן כסטטי
        {
            playerName = playerName0;
        }






    }
}
