using System;
using System.Collections;
using UnityEngine;

namespace TempleRun.Main {
    public class Game : IDisposable {
        public World World { get; private set; }
        public Hero Hero { get; private set; }
        public GameStats Stats { get; private set; }
        private Coroutine statProcess;
        private Coroutine scoreProceess;

        public Game(WorldConfig worldConfig, HeroPrefab heroPrefab) {
            World = GameObject.Instantiate(worldConfig.World);
            Hero = CreateHero(heroPrefab);
            World.Camera.Target = Hero.transform;
            Stats = new GameStats();
        }

        public void Run() {
            Hero.Run();
            statProcess = World.StartCoroutine(UpdateStats());
            scoreProceess = World.StartCoroutine(UpdateScore());
        }

        public void Stop() {
            World.Camera.Target = null;
            Hero.Stop();
            World.StopCoroutine(statProcess);
            World.StopCoroutine(scoreProceess);
        }

        public void Dispose() {
            GameObject.DestroyImmediate(World.gameObject);
            statProcess = null;
            scoreProceess = null;
        }

        private IEnumerator UpdateStats() {
            while (true) {
                yield return 0;
                Stats.Velocity = Hero.HorizontalSpeed;
                Stats.Distance = GetHeroDistance();
            }   
        }

        private IEnumerator UpdateScore() {
            float prevHeroDistance = GetHeroDistance();
            float prevHeroSpeed = Hero.HorizontalSpeed;
            while (true) {
                yield return new WaitForSeconds(1);

                float curHeroDistance = GetHeroDistance();
                float curHeroSpeed = Hero.HorizontalSpeed;
                Stats.Score += CalculateScore(curHeroDistance - prevHeroDistance, prevHeroSpeed, curHeroSpeed);
                prevHeroDistance = curHeroDistance;
                prevHeroSpeed = curHeroSpeed;
            }
        }

        private Hero CreateHero(HeroPrefab template) {
            var heroPrefab = GameObject.Instantiate(template);
            var hero = heroPrefab.gameObject.AddComponent<Hero>();
            hero.transform.parent = World.transform;
            hero.transform.position = World.SpawnPoint.transform.position;
            hero.transform.rotation = World.transform.rotation;
            hero.Initialize(World);

#if UNITY_EDITOR
            hero.gameObject.AddComponent<KeyboardHeroController>();
#else
            hero.gameObject.AddComponent<TapHeroController>();
#endif

            return hero;
        }

        private float GetHeroDistance() {
            return Hero.transform.position.x - World.SpawnPoint.position.x;
        }

        private int CalculateScore(float deltaDistance, float prevSpeed, float newSpeed) {
            float avgSpeed = (prevSpeed + newSpeed) / 2;
            return Mathf.RoundToInt((deltaDistance + avgSpeed)/10);
        }
    }
}
