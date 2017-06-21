using EZData;

namespace TempleRun.Main {
    public class GameStats : EZData.Context {
        #region Property Score
        private readonly Property<int> _privateScoreProperty = new Property<int>();
        public Property<int> ScoreProperty { get { return _privateScoreProperty; } }
        public int Score {
            get { return ScoreProperty.GetValue(); }
            set { ScoreProperty.SetValue(value); }
        }
        #endregion

        #region Property Distance
        private readonly Property<float> _privateDistanceProperty = new Property<float>();
        public Property<float> DistanceProperty { get { return _privateDistanceProperty; } }
        public float Distance {
            get { return DistanceProperty.GetValue(); }
            set { DistanceProperty.SetValue(value); }
        }
        #endregion

        #region Property Velocity
        private readonly Property<float> _privateVelocityProperty = new Property<float>();
        public Property<float> VelocityProperty { get { return _privateVelocityProperty; } }
        public float Velocity {
            get { return VelocityProperty.GetValue(); }
            set { VelocityProperty.SetValue(value); }
        }
        #endregion
    
    }
}
