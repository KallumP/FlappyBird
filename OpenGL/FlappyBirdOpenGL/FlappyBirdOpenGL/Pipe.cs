using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
namespace FlappyBirdOpenGL {
    class Pipe {
        static Random rnd = new Random();
        Game parent;
        public PointF coords;
        public int width;
        public int gap;
        Color color;
        public bool scoreTaken = false;

        /// <summary>
        /// This variable is the same over all the pipes
        /// </summary>
        public static float speed = 4;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_parent">takes in an instance of the Game class</param>
        public Pipe(Game _parent) {
            parent = _parent;

            color = Color.Green;
            coords = new PointF(parent.window.Width, NewHeight());
            width = 60;
            gap = 150;
        }

        /// <summary>
        /// Creates a new height for the pipe to spawn at
        /// </summary>
        /// <returns></returns>
        float NewHeight() {

            float height = rnd.Next(parent.window.Height / 10, parent.window.Height - (parent.window.Height / 3) - gap);

            return height;
        }

        /// <summary>
        /// Makes the pipe move towards the player
        /// </summary>
        public void Move() {
            coords.X -= speed;
        }

        /// <summary>
        /// Draws out the pipe
        /// </summary>
        public void Draw() {

            GL.Begin(BeginMode.Polygon);

            GL.Color3(color);

            GL.Vertex2(coords.X, 0);
            GL.Vertex2(coords.X + width, 0);
            GL.Vertex2(coords.X + width, coords.Y);
            GL.Vertex2(coords.X, coords.Y);


            GL.End();


            GL.Begin(BeginMode.Polygon);

            GL.Vertex2(coords.X, coords.Y + gap);
            GL.Vertex2(coords.X + width, coords.Y + gap);
            GL.Vertex2(coords.X + width, parent.window.Height);
            GL.Vertex2(coords.X, parent.window.Height);


            GL.End();
        }

        /// <summary>
        /// Returns true if the pipe's coordinates are outside the window
        /// </summary>
        /// <returns></returns>
        public bool BoundCollisionCheck() {

            if (coords.X + width < 0)
                return true;

            return false;
        }
    }
}
