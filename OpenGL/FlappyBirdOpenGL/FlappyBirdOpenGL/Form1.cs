using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace FlappyBirdOpenGL {
    public partial class Form1 : Form {

        Game game;

        public Form1() {
            InitializeComponent();
        }

        int targetFrameRate = 60;

        private void Form1_Load(object sender, EventArgs e) {
            gameLoop.Interval = 1000 / targetFrameRate;

            game = new Game(this, targetFrameRate, new Point(canvas.Width, canvas.Height));

            SetupGL();
        }

        private void Form1_Resize(object sender, EventArgs e) {
            SetupGL();
        }

        /// <summary>
        /// sets up the viewports for the gl control
        /// </summary>
        void SetupGL() {
            GL.ClearColor(Color.Black);
            GL.Viewport(0, 0, canvas.Width, canvas.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, canvas.Width, canvas.Height, 0, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        /// <summary>
        /// Draws out the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_Paint(object sender, PaintEventArgs e) {

            //only draws if the game has started
            if (game.started) {

                GL.Clear(ClearBufferMask.ColorBufferBit);

                game.Tick();

                game.player.Draw();

                foreach (Pipe p in game.pipes)
                    p.Draw();

                canvas.SwapBuffers();
            }
        }

        /// <summary>
        /// Deals with what should happen each tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameLoop_Tick(object sender, EventArgs e) {
            canvas.Invalidate();
        }

        /// <summary>
        /// Lets the user start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_btn_Click(object sender, EventArgs e) {

            start_btn.Enabled = false;
            game.StartGame();
        }

        /// <summary>
        /// Deals with getting key inputs from the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e) {

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up || e.KeyCode == Keys.Space)
                game.player.Flap();

        }

        /// <summary>
        /// Updates the score
        /// </summary>
        /// <param name="score"></param>
        public void UpdateScore(int score) {
            score_lbl.Text = score.ToString();
        }

        /// <summary>
        /// Ends the game and ouputs the score
        /// </summary>
        /// <param name="score"></param>
        public void EndGame(int score) {

            //reopens the menu
            Show();

            UpdateScore(score);

            //allows the user to start the game again
            start_btn.Enabled = true;
        }
    }
}
