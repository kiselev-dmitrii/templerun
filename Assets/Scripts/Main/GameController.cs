using System.Collections;
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
        }

        public IEnumerator FinishGame() {
            Assert.IsTrue(game != null, "Game not runned");
            game.Stop();
            yield return new WaitForSeconds(1.0f);

        }
    }
}

