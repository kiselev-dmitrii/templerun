using System;
using UnityEngine;

namespace TempleRun.Main {
    public class Game : IDisposable {
        public World World;
        public Hero Hero;
        public int Score;

        public Game(WorldConfig worldConfig, HeroPrefab heroPrefab) {
            World = GameObject.Instantiate(worldConfig.World);
            Hero = World.SpawnHero(heroPrefab);

            Hero.gameObject.AddComponent<KeyboardHeroController>();
            World.Camera.Target = Hero.transform;
        }

        public void Run() {
            Hero.Run();
        }

        public void Stop() {
            World.Camera.Target = null;
            Hero.Stop();
        }

        public void Dispose() {
            GameObject.DestroyImmediate(World.gameObject);
            GameObject.DestroyImmediate(Hero.gameObject);
        }
    }
}
