using TempleRun.Main;
using TempleRun.Utils;

namespace TempleRun.ViewModel {
    public class GameOverWindow : Window {
        private readonly GameController gameController;

        public GameOverWindow() : base("UI/GameOverWindow") {
            gameController = GameController.Instance;
        }

        #region Handlers
        public void OnBackToMenuButtonClick() {
            gameController.DestroyGame();
            Destroy();
        }
        #endregion
    }
}
