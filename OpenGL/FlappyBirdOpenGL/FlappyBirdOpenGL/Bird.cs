using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms.Design;

namespace FlappyBirdOpenGL {
    class Bird {

        /// <summary>
        /// An instance to the Game class
        /// </summary>
        Game parent;

        Color color;

        public int score { get; set; }

        /// <summary>
        /// the gravity to be applied to the bird (px per second per second)
        /// </summary>
        float gravity = 1f;

        /// <summary>
        /// the accelaration provided by a flap to the bird(px per second per second)
        /// </summary>
        float flapVel = -10f;

        /// <summary>
        /// The overal velocity of the bird
        /// </summary>
        float vel = 0;

        /// <summary>
        /// The fastest speed the bird should reach
        /// </summary>
        float termVel;

        /// <summary>
        /// The coordinates of the player
        /// </summary>
        public PointF coords;

        /// <summary>
        /// The width/radisu of the player
        /// </summary>
        public int radius = 20;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startingCoords">The starting coordinates for the player</param>
        /// <param name="_parent">A refference to the parent class</param>
        public Bird(PointF startingCoords, Game _parent) {
            coords = startingCoords;
            parent = _parent;
            termVel = 10;
            color = Color.Yellow;
        }

        /// <summary>
        /// The method that updates the bird every tick
        /// </summary>
        public void Tick() {
            vel += gravity;
            CheckForBounds();
            Move();
        }

        /// <summary>
        /// Makes the bird flap up
        /// </summary>
        public void Flap() {
            vel = flapVel;
        }

        /// <summary>
        /// A check to see if the ceiling or ground was hit
        /// </summary>
        void CheckForBounds() {

            //stops the bird from going outside the window
            if (coords.Y + radius > parent.dimensions.Y)
                coords.Y = parent.dimensions.Y - radius;

            else if (coords.Y - radius < 0)
                coords.Y = radius;
        }

        /// <summary>
        /// Calculates the velocity, and then moves the bird
        /// </summary>
        void Move() {

            if (vel > termVel)
                vel = termVel;
            else if (vel < -termVel)
                vel = -termVel;


            coords.Y += vel;
        }

        /// <summary>
        /// Draws out the player
        /// </summary>
        public void Draw() {

            GL.Begin(BeginMode.Polygon);

            //loops through and draws out a cirle to represent the player
            for (int i = 0; i <= 360; i += 20) {

                double x = coords.X + Math.Sin(i / 57.2) * radius;
                double y = coords.Y + Math.Cos(i / 57.2) * radius;

                GL.Color3(color);
                GL.Vertex2(x, y);
            }

            GL.End();
        }
    }
}
