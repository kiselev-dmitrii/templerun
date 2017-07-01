using UnityEngine;

namespace TempleRun.Main.PathGenerator {
    public class PathObstacle : MonoBehaviour {
        public int Id;
        public float Probability;
        private BoxCollider boxCollider;

        public void Awake() {
            boxCollider = GetComponent<BoxCollider>();
        }

        public float Height {
            get { return boxCollider.size.y; }
        }
    }
}
