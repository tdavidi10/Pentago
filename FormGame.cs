using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Pentago_Tamir_Davidi
{
    public partial class FormGame : Form
    {     ////we are white, computer is black
        FormVictory formVictory; //טופס ניצחון
        FormDefeat formDefeat; //טופס הפסד
        FormTie formTie; //טופס תיקו
        Graphics g; //משתנה גראפיקה
        Board board; //לוח המשחק
        Brush brush; //בראש של הצביעה
        Pen penBoardFrame;  //עט ציור מסגרת הלוח
        Pen penCells; //עט ציור תאי הלוח
        int xBoard; //איקס עוגן הלוח
        int yBoard; //וואי עוגן הלוח
        int widthBoard; //רוחב הלוח
        int heightBoard; //גובה הלוח
        MainMenu mainMenu; //טופס תפריט ראשי
        int time; //זמן שעבר מתחילת המשחק
        static string playerName; //שם השחקן


        public FormGame(MainMenu mainMenu) //פעולה בונה
        {
            InitializeComponent();
            formVictory = new FormVictory(mainMenu);
            formDefeat = new FormDefeat(mainMenu);
            formTie = new FormTie(mainMenu);
            xBoard = (int)(ClientRectangle.Width / 5);
            yBoard=(int)(ClientRectangle.Height / 5);
            widthBoard=(int)(3*((int)(ClientRectangle.Width/5)));
            heightBoard = (int)(3 * ((int)(ClientRectangle.Height / 5)));
            g = CreateGraphics();
            brush = new SolidBrush(Color.DarkRed);
            penBoardFrame = new Pen(brush, 4);
            penCells = new Pen(brush, 2); 
            board = new Board(xBoard, yBoard, widthBoard, heightBoard, this, g, penBoardFrame, penCells, formVictory, formDefeat, formTie);
            this.mainMenu = mainMenu;
            time = 0;
            

        }

        private void FormGame_Load(object sender, EventArgs e)
        {
           

        }



        public void FormGame_MouseClick(object sender, MouseEventArgs e) //מנהלת את ההקלקה בטופס
        {
            board.MouseClick(g, e.X, e.Y);
            RotationArrow.SetTime(time);
            board.WinLoseTieForms(time, playerName); //בדיקה אם הסתיים המשחק
        }



        private void FormGame_Paint(object sender, PaintEventArgs e) //אחרי כל תור הלוח מתעדכן בהתאמה
        {
            g = CreateGraphics();
            xBoard = (int)(ClientRectangle.Width / 5);
            yBoard = (int)(ClientRectangle.Height / 5);
            widthBoard = (int)(3 * ((int)(ClientRectangle.Width / 5)));
            heightBoard = (int)(3 * ((int)(ClientRectangle.Height / 5)));
            board.SetX(xBoard);
            board.SetY(yBoard);
            board.SetWidth(widthBoard);
            board.SetHeight(heightBoard);
            board.SetCenterCellsRadius(xBoard, yBoard, widthBoard, heightBoard);
            board.SetLocationArrows();          
            board.Draw(g);
            board.DrawStatus(g);
        }


        private void HomeButton_Click(object sender, EventArgs e) //לחיצה על כפתור הבית מעבירה את המשתמש לתפריט הראשי
        {
            this.Hide();
            mainMenu.Show();
            
        }

        private void FormGame_FormClosing(object sender, FormClosingEventArgs e) //לחיצה על כפתור היציאה יוצא מהתוכנה
        { 
            Application.Exit();
        }

        

        private void timeCounter_Tick(object sender, EventArgs e) //הטיימר מציג לשחקן כמה זמן בשניות עבר מתחילת המשחק
        {
            time++;
            timerLabel.Text =  time.ToString() + " sec";
        }

        public static void SetPlayerName(string playerName0) //שימוש במשתנה סטטי שם משתמש
        {
            playerName = playerName0;
        }

    }
}
