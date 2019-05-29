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
        PipeSpawner spawner;
        public List<Pipe> pipes;
        Form1 parent;
        int targetFPS = 60;

        /// <summary>
        /// constructor
        /// </summary>
        public Game(Form1 _parent) {
            parent = _parent;
            window = new GameWindow(400, 600);
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
            window.Run(1 / targetFPS);
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

            player.Draw();

            foreach (Pipe p in pipes)
                p.Draw();


            window.SwapBuffers();
        }

        /// <summary>
        /// Deals with when keys are pressed
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        void KeyPressed(object o, KeyPressEventArgs e) {
            if (e.KeyChar == 'w') {
                player.Flap();
            }
        }

        /// <summary>
        /// Starts and sets up the game environment
        /// </summary>
        void StartGame() {

            //sets up the player to start in the middle of the screen, closer to the left
            player = new Bird(new PointF(window.Width / 5, window.Height / 2), this);
            pipes = new List<Pipe>();
            spawner = new PipeSpawner(this);

        }

        /// <summary>
        /// Deals with what needs to happen each tick of the game
        /// </summary>
        void Tick() {

            MakeGameHarder();
            CheckToRemovePipes();

            player.Tick();
            spawner.Tick();

            foreach (Pipe p in pipes)
                p.Move();

            CheckForCollision();
        }

        /// <summary>
        /// Speeds up the game, and makes sure that the pipes spawn fast enough for the new speed
        /// </summary>
        void MakeGameHarder() {

            //makes the game faster
            Pipe.speed += 0.003f;


            //calculates how many frames it should take the pipe to leave the screen, 
            //then multiplies that by the time per frame to get the time taken for a pipe to leave the screen
            PipeSpawner.spawnRate = (window.Width / Pipe.speed) * (float)window.UpdatePeriod * 1000;
        }

        /// <summary>
        /// Makes sure that pipes are removed when they go off screen
        /// </summary>
        void CheckToRemovePipes() {
            if (pipes.Count != 0)
                if (pipes[0].BoundCollisionCheck())
                    pipes.RemoveAt(0);
        }

        /// <summary>
        /// Checks the bird against the first pipe to make sure that there wasnt any collisions
        /// </summary>
        void CheckForCollision() {

            //makes sure that there are actually objects to check for collision
            if (pipes.Count != 0 && player != null) {

                //sets up a pipe to check
                Pipe p = pipes[0];

                //checks to see if the player is within the width and the gap of the pipes
                if (player.coords.X + player.radius > p.coords.X && player.coords.X - player.radius < p.coords.X + p.width) {

                    //checks to see if the player is within the gamp of the pipes
                    if (player.coords.Y - player.radius > p.coords.Y && player.coords.Y + player.radius < p.coords.Y + p.gap) {

                        //the player's color turns yellow if no collision
                        player.color = Color.Yellow;

                        //adds the score to the player, if this is their first time passing the pipe
                        if (p.scoreTaken == false) {

                            player.score++;
                            p.scoreTaken = true;
                        }

                    } else {

                        //Ends the game if there was a collision
                        EndGame();
                    }

                } else {

                    //the player's color turns yellow if they haven't reached a pipe yet
                    player.color = Color.Yellow;
                }
            }
        }

        /// <summary>
        /// Ends the game
        /// </summary>
        void EndGame() {

            parent.EndGame(player.score);

            window.Close();

            
        }
    }
}


/* still to do
 * 
 * 
 * scoring system
 */
