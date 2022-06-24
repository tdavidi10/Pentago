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
    public partial class FormDefeat : Form
    {
        MainMenu mainMenu;

        public FormDefeat(MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainMenu.Show();
        }

        private void FormDefeat_Load(object sender, EventArgs e)
        {

        }

        private void FormDefeat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
