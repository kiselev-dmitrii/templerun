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

        #region Property MaxVelocity
        private readonly Property<float> _privateMaxVelocityProperty = new Property<float>();
        public Property<float> MaxVelocityProperty { get { return _privateMaxVelocityProperty; } }
        public float MaxVelocity {
            get { return MaxVelocityProperty.GetValue(); }
            set { MaxVelocityProperty.SetValue(value); }
        }
        #endregion

        public GameScreenWindow(Game game) : base("UI/GameScreenWindow") {
            Stats = game.Stats;
            MaxVelocity = game.Hero.Def.MaxVelocity;
        }
    }
}
