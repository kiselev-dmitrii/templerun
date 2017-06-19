using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace TempleRun.Assets.Scripts.Utils {
    public class WeightedRandom<T> {
        private readonly T[] items;
        private readonly float[] weights;
        private readonly float weightSum;

        public WeightedRandom(T[] items, float[] weights) {
            Assert.IsTrue(items.Length == weights.Length);
            this.items = items;
            this.weights = weights;
            this.weightSum = weights.Sum(x => x);
        }

        public WeightedRandom(IEnumerable<T> items, Func<T, float> weightFunc) {
            this.items = items.ToArray();
            this.weights = items.Select(x => weightFunc(x)).ToArray();
            this.weightSum = weights.Sum(x => x);
        } 

        public T GetRandom() {
            float rnd = UnityEngine.Random.Range(0, weightSum);

            float partialSum = 0;
            for (int i = 0; i < items.Length; ++i) {
                partialSum += weights[i];
                if (rnd <= partialSum) return items[i];
            }
            return default(T);
        }
    }
}
