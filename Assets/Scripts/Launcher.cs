using TempleRun.Model;
using TempleRun.ViewModel;
using UnityEngine;

namespace TempleRun {
    public class Launcher : MonoBehaviour {
        public RootContext DataContext;
        public ApplicationModel Model { get; private set; }
        public MainScreen MainScreen { get; private set; }

        public void Awake() {
            Model = new ApplicationModel();
            MainScreen = new MainScreen();
            DataContext.SetContext(MainScreen);
        }
    }
}
