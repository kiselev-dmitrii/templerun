using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TempleRun.Main;
using UnityEngine;

namespace TempleRun.Assets.Scripts.Main {
    public class HeroCollisionDetector : MonoBehaviour {
        private GameController gameController;

        private void Awake() {
            gameController = GameController.Instance;
        }

        #region Handlers
        public void OnTriggerEnter(Collider collider) {
            if (collider.tag == "Obstacle") {
                Debug.Log("Obstacle");
                gameController.StartCoroutine(gameController.OnObstacleCollided());
            }
        }
        #endregion
    }
}
