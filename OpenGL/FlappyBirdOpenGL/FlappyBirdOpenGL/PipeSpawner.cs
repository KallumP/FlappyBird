using System;
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
        /// A counter to keep track of how long it has been since the last spawn (holds ms)
        /// </summary>
        float spawnCounter;

        /// <summary>
        /// Constructor
        /// </summary>
        public PipeSpawner(Game _parent) {
            parent = _parent;

            CalculateSpawnRate();
        }
        
        /// <summary>
        /// Calculates the new spawnrate of the spawner
        /// </summary>
        public void CalculateSpawnRate() {

            //calculates the spawnrate by seeing how many frames it should take for a pipe to move through the screen
            //and then calculating how many miliseconds that shouuld take
            spawnRate = (parent.parent.Width / Pipe.speed) * (1000 / (float)parent.frameRate);
        }

        /// <summary>
        /// Deals with what should happen in the spawner each tick
        /// </summary>
        public void Tick() {

            //adds the time passed since the last tick (ms)
            spawnCounter += 1000 / (float)parent.frameRate;

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
