using System;

namespace TempleRun.Assets.Scripts.Utils {
    [Serializable]
    public class IntRange {
        public int Min;
        public int Max;

        public IntRange(int min, int max) {
            Min = min;
            Max = max;
        }
    }
}
