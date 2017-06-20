using UnityEngine;

namespace TempleRun.Main {
    public class KeyboardHeroController : MonoBehaviour {
        private Hero hero;

        public void Awake() {
            hero = GetComponent<Hero>();
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                hero.Jump();
            }
        }
    }
}
