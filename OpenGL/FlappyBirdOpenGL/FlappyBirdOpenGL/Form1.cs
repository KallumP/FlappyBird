using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBirdOpenGL {
    public partial class Form1 : Form {

        Game game;

        public Form1() {
            InitializeComponent();
        }

        /// <summary>
        /// Lets the user start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_btn_Click(object sender, EventArgs e) {

            //hides the menu
            Hide();

            //opens up the game window
            game = new Game(this);
        }

        /// <summary>
        /// Ends the game and ouputs the score
        /// </summary>
        /// <param name="score"></param>
        public void EndGame(int score) {

            //reopens the menu
            Show();

            //ouputs the score
            tempScore_lbl.Text = score.ToString();
        }
    }
}
