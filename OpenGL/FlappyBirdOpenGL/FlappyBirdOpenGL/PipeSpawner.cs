﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdOpenGL {
    class PipeSpawner {

        /// <summary>
        /// A reference to the Game class
        /// </summary>
        Game parent;

        /// <summary>
        /// The rate at which pipes should spawn (seconds between spawns)
        /// </summary>
        public static float spawnRate;

        /// <summary>
        /// A counter to keep track of how long it has been since the last spawn (holds seconds)
        /// </summary>
        float spawnCounter;

        /// <summary>
        /// Constructor
        /// </summary>
        public PipeSpawner(Game _parent) {
            parent = _parent;
            spawnRate = (parent.window.Width / Pipe.speed) * (float)parent.window.UpdatePeriod * 1000;
        }

        /// <summary>
        /// Deals with what should happen in the spawner each tick
        /// </summary>
        public void Tick() {

            //adds the time passes since the last tick
            spawnCounter += (float)parent.window.UpdatePeriod * 1000;

            CheckToSpawn();
        }

        /// <summary>
        /// Checks to see if a new pipe should be spawned into the game
        /// </summary>
        void CheckToSpawn() {
            if (spawnCounter >= spawnRate) {
                spawnCounter = 0;
                parent.pipes.Add(new Pipe(parent));
            }
        }
    }
}
