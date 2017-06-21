using EZData;
using TempleRun.Main;
using TempleRun.Utils;

namespace TempleRun.ViewModel.MainScreen {
    public class MainScreenWindow : Window {
        #region HeroPopup
        public readonly VariableContext<ChooseHeroPopup> HeroPopupEzVariableContext = new VariableContext<ChooseHeroPopup>(null);
        public ChooseHeroPopup HeroPopup {
            get { return HeroPopupEzVariableContext.Value; }
            set { HeroPopupEzVariableContext.Value = value; }
        }
        #endregion

        #region WorldPopup
        public readonly VariableContext<ChooseWorldPopup> WorldPopupEzVariableContext = new VariableContext<ChooseWorldPopup>(null);
        public ChooseWorldPopup WorldPopup {
            get { return WorldPopupEzVariableContext.Value; }
            set { WorldPopupEzVariableContext.Value = value; }
        }
        #endregion

        private readonly ApplicationController applicationController;

        public MainScreenWindow(HeroPrefab[] heroes, WorldConfig[] worlds, ApplicationController applicationController) : base("UI/MainScreenWindow") {
            HeroPopup = new ChooseHeroPopup(heroes, OnHeroPopupTitlebarClick, OnHeroPopupSelectButtonClick);
            WorldPopup = new ChooseWorldPopup(worlds, OnWorldPopupSelectButtonClick);
            this.applicationController = applicationController;
            
            HeroPopup.SetVisible(true);
            WorldPopup.SetVisible(false);
        }

        #region Handlers
        public void OnHeroPopupTitlebarClick() {
            HeroPopup.SetVisible(true);
            WorldPopup.SetVisible(false);
        }

        public void OnHeroPopupSelectButtonClick() {
            applicationController.SetHero(HeroPopup.SelectedHero.Prefab);
            WorldPopup.SetVisible(true);
        }

        public void OnWorldPopupSelectButtonClick() {
            applicationController.SetWorld(WorldPopup.SelectedWorld.Config);
            applicationController.StartGame();
            
        }
        #endregion

    }
}
