using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace FlappyBirdOpenGL {
    class Game {
        public GameWindow window;
        Bird player;

        /// <summary>
        /// constructor
        /// </summary>
        public Game() {
            window = new GameWindow(400, 500);
            StartGame();
            OpenGame();
        }

        /// <summary>
        /// Opens the window
        /// </summary>
        void OpenGame() {

            window.Load += Load;
            window.Resize += Resize;
            window.RenderFrame += Render;
            window.KeyPress += KeyPressed;

            //makes the window refress every 60th of a second
            window.Run(1.0 / 60.0);
        }

        /// <summary>
        /// Method that happens when the game loads
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        void Load(object o, EventArgs e) {
            GL.ClearColor(0, 0, 0, 0);

        }

        /// <summary>
        /// Deals with resizing the window
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        void Resize(object o, EventArgs e) {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, window.Width, window.Height, 0, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);

        }

        /// <summary>
        /// Renders each frame
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        void Render(object o, EventArgs e) {

            Tick();


            GL.Clear(ClearBufferMask.ColorBufferBit);

            //draws out the player
            GL.Begin(BeginMode.Polygon);

            //loops through and draws out a cirle to represent the player
            for (int i = 0; i <= 360; i += 30) {

                double x = player.coords.X + Math.Sin(i / 57.2) * player.radius;
                double y = player.coords.Y + Math.Cos(i / 57.2) * player.radius;

                GL.Vertex2(x, y);
            }

            GL.End();
            window.SwapBuffers();
        }

        /// <summary>
        /// Deals with when keys are pressed
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        void KeyPressed(object o, KeyPressEventArgs e) {
            if (e.KeyChar == 'w' ) {
                player.Flap();
            }
        }

        /// <summary>
        /// Starts and sets up the game environment
        /// </summary>
        void StartGame() {

            //sets up the player to start in the middle of the screen, closer to the left
            player = new Bird(new PointF(window.Width / 5, window.Height / 2), this);
        }

        /// <summary>
        /// Deals with what needs to happen each tick of the game
        /// </summary>
        void Tick() {
            player.Tick();
        }

    }
}
