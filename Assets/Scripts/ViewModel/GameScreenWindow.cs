using EZData;
using TempleRun.Main;
using TempleRun.Utils;

namespace TempleRun.ViewModel {
    public class GameScreenWindow : Window {
        #region Stats
        public readonly VariableContext<GameStats> StatsEzVariableContext = new VariableContext<GameStats>(null);
        public GameStats Stats {
            get { return StatsEzVariableContext.Value; }
            set { StatsEzVariableContext.Value = value; }
        }
        #endregion

        public GameScreenWindow(GameStats stats) : base("UI/GameScreenWindow") {
            Stats = stats;
        }
    }
}
