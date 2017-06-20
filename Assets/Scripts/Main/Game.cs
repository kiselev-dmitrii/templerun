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

#if UNITY_EDITOR
            Hero.gameObject.AddComponent<KeyboardHeroController>();
#else
            Hero.gameObject.AddComponent<TapHeroController>();
#endif

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
        }
    }
}
