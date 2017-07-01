using TempleRun.Main.PathGenerator;
using UnityEngine;

namespace TempleRun.Main {
    public class HeroCollisionDetector : MonoBehaviour {
        private ApplicationController applicationController;

        private void Awake() {
            applicationController = ApplicationController.Instance;
        }

        #region Handlers
        public void OnTriggerEnter(Collider col) {
            if (col.CompareTag("Obstacle")) {
                applicationController.FinishGame();
                return;
            }

            if (col.CompareTag("Item")) {
                var item = col.gameObject.GetComponent<PathItem>();
                item.Take();
                applicationController.IncrementScore(item.Score);
            }
        }
        #endregion
    }
}
