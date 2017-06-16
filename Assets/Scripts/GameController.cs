using UnityEngine;

namespace TempleRun.Assets.Scripts {
    public class GameController : MonoBehaviour {
        public Hero player;

        public void Start() {
            player.Run();
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                player.Jump();
            }
        }
    }
}
