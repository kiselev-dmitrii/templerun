using TempleRun.ViewModel;
using UnityEngine;

namespace TempleRun {
    public class Launcher : MonoBehaviour {
        public RootContext DataContext;
        public MainScreen MainScreen { get; private set; }

        public void Awake() {
            MainScreen = new MainScreen();
            DataContext.SetContext(MainScreen);
        }
    }
}
