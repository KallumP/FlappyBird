using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FlappyBirdOpenGL {
    class Bird {

        Game parent;

        /// <summary>
        /// the gravity to be applied to the bird (px per second per second)
        /// </summary>
        float gravity = 0.01f;

        /// <summary>
        /// the accelaration provided by a flap to the bird(px per second per second)
        /// </summary>
        float flapAcc = -7;

        float acc = 0;
        float vel = 0;
        float termVel = 20;

        /// <summary>
        /// The coordinates of the player
        /// </summary>
        public PointF coords;


        /// <summary>
        /// The width/radisu of the player
        /// </summary>
        public int radius { get; } = 20;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startingCoords">The starting coordinates for the player</param>
        /// <param name="_parent">A refference to the parent class</param>
        public Bird(PointF startingCoords, Game _parent) {
            coords = startingCoords;
            parent = _parent;
            termVel = 10 / (float)parent.window.UpdateFrequency;
        }

        /// <summary>
        /// The method that updates the bird every tick
        /// </summary>
        public void Tick() {
            acc += gravity * (float)parent.window.UpdateFrequency;
            CheckForBounds();
            Move();
        }

        /// <summary>
        /// Makes the bird flap up
        /// </summary>
        public void Flap() {
            acc = flapAcc;
        }

        /// <summary>
        /// A check to see if the ceiling or ground was hit
        /// </summary>
        void CheckForBounds() {
            if (coords.Y + radius > parent.window.Height) {

                coords.Y = parent.window.Height - 1;



            } else if (coords.Y - radius < 0) {


            }
        }

        /// <summary>
        /// Calculates the velocity, and then moves the bird
        /// </summary>
        void Move() {

            vel = 0;
            vel += acc;

            if (vel > termVel)
                vel = termVel;
            else if (vel < -termVel)
                vel = -termVel;


            coords.Y += vel;
        }
    }
}
