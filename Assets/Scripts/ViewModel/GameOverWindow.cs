using TempleRun.Main;
using TempleRun.Utils;

namespace TempleRun.ViewModel {
    public class GameOverWindow : Window {
        private readonly ApplicationController applicationController;

        public GameOverWindow(ApplicationController applicationController) : base("UI/GameOverWindow") {
            this.applicationController = applicationController;
        }

        #region Handlers
        public void OnBackToMenuButtonClick() {
            applicationController.DestroyGame();
            Destroy();
        }
        #endregion
    }
}
