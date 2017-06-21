using System;
using System.Collections;
using System.Linq;
using TempleRun.Assets.Scripts.Main;
using TempleRun.ViewModel;
using TempleRun.ViewModel.MainScreen;
using UnityEngine;
using UnityEngine.Assertions;

namespace TempleRun.Main {
    public class ApplicationController : MonoBehaviour {
        public static ApplicationController Instance { get; private set; }
        public const String HeroDirectory = "Heroes";
        public const String WorldDirectory = "Worlds";

        private HeroPrefab[] heroes;
        private WorldConfig[] worlds;

        private HeroPrefab selectedHero;
        private WorldConfig selectedWorld;
        private Game game;

        private MainScreenWindow mainScreen;
        private GameScreenWindow gameScreen;

        public void Awake() {
            Instance = this;
            heroes = Resources.LoadAll<GameObject>(HeroDirectory).Select(x => x.GetComponent<HeroPrefab>()).ToArray();
            worlds = Resources.LoadAll<WorldConfig>(WorldDirectory);

            mainScreen = new MainScreenWindow(heroes, worlds, this);
            mainScreen.Activate();
        }

        public void SetHero(HeroPrefab heroDef) {
            selectedHero = heroDef;
        }

        public void SetWorld(WorldConfig worldDef) {
            selectedWorld = worldDef;
        }

        public void StartGame() {
            Assert.IsTrue(game == null, "Game already runned");
            game = new Game(selectedWorld, selectedHero);
            game.Run();

            mainScreen.Destroy();
            gameScreen = new GameScreenWindow(game.Stats);
            gameScreen.Activate();
        }

        public void FinishGame() {
            Assert.IsTrue(game != null, "Game not runned");
            StartCoroutine(FinishGameCoroutine());
        }

        private IEnumerator FinishGameCoroutine() {
            game.Hero.Die();
            game.Stop();
            yield return new WaitForSeconds(1.0f);

            var gameOverWindow = new GameOverWindow(this);
            gameOverWindow.Activate();
        }

        public void DestroyGame() {
            game.Dispose();
            game = null;

            mainScreen = new MainScreenWindow(heroes, worlds, this);
            mainScreen.Activate();

            gameScreen.Destroy();
        }
    }
}

