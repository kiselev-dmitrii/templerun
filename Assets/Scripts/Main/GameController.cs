using System.Collections;
using TempleRun.Assets.Scripts.Main;
using TempleRun.View;
using TempleRun.ViewModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace TempleRun.Main {
    public enum GameState {
        Menu,
        Game,
        Finishing
    }

    public class GameController : MonoBehaviour {
        public Canvas MainUI;
        public Canvas GameUI;
        public VelocityIndicator Indicator;
        public static GameController Instance { get; private set; }

        private HeroPrefab selectedHero;
        private WorldConfig selectedWorld;
        private Game game;

        private void Awake() {
            Instance = this;
            MainUI.gameObject.SetActive(true);
            GameUI.gameObject.SetActive(false);
        }

        public void SetHero(HeroPrefab heroDef) {
            selectedHero = heroDef;
        }

        public void SetWorld(WorldConfig worldDef) {
            selectedWorld = worldDef;
        }

        public void StartGame() {
            Assert.IsTrue(game == null, "Game already runned");
            MainUI.gameObject.SetActive(false);
            GameUI.gameObject.SetActive(true);

            game = new Game(selectedWorld, selectedHero);
            game.Run();

            Indicator.Initialize(game.Hero);
            game.Hero.gameObject.AddComponent<HeroCollisionDetector>();
        }

        public void FinishGame() {
            Assert.IsTrue(game != null, "Game not runned");
            game.Hero.Die();
            game.Stop();
        }

        public void DestroyGame() {
            MainUI.gameObject.SetActive(true);
            GameUI.gameObject.SetActive(false);
            game.Dispose();
            game = null;
        }

        public IEnumerator OnObstacleCollided() {
            FinishGame();
            yield return new WaitForSeconds(1.0f);
            
            var gameOverWindow = new GameOverWindow();
            gameOverWindow.Activate();
        }
    }
}

