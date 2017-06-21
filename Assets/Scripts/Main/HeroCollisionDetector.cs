using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TempleRun.Main;
using UnityEngine;

namespace TempleRun.Assets.Scripts.Main {
    public class HeroCollisionDetector : MonoBehaviour {
        private ApplicationController applicationController;

        private void Awake() {
            applicationController = ApplicationController.Instance;
        }

        #region Handlers
        public void OnTriggerEnter(Collider col) {
            if (col.tag == "Obstacle") {
                applicationController.FinishGame();
            }
        }
        #endregion
    }
}
