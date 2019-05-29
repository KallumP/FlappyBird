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

        public Bird player;
        PipeSpawner spawner;
        public List<Pipe> pipes;
        public Form1 parent { get; set; }
        public int frameRate { get; set; }

        public Point dimensions { get; set; }

        public bool started { get; set; } = false;


        /// <summary>
        /// constructor
        /// </summary>
        public Game(Form1 _parent, int _frameRate, Point _dim) {
            parent = _parent;
            frameRate = _frameRate;
            dimensions = _dim;
        }

        /// <summary>
        /// Starts and sets up the game environment
        /// </summary>
        public void StartGame() {

            started = true;

            //sets up the player to start in the middle of the screen, closer to the left
            player = new Bird(new PointF(dimensions.X / 5, dimensions.Y / 2), this);
            pipes = new List<Pipe>();
            spawner = new PipeSpawner(this);

        }

        /// <summary>
        /// Deals with what needs to happen each tick of the game
        /// </summary>
        public void Tick() {

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

            spawner.CalculateSpawnRate();
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

                        //adds the score to the player, if this is their first time passing the pipe
                        if (p.scoreTaken == false) {
                            p.scoreTaken = true;
                            parent.UpdateScore(player.score++);
                        }

                    } else {

                        //Ends the game if there was a collision
                        EndGame();
                    }

                }
            }
        }

        /// <summary>
        /// Ends the game
        /// </summary>
        void EndGame() {

            started = false;
            parent.EndGame(player.score);
        }
    }
}
