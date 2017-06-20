using UnityEngine;

namespace TempleRun.Main {
    public class TapHeroController : MonoBehaviour {
        private Hero hero;
        private int prevTouchCount;

        public void Awake() {
            hero = GetComponent<Hero>();
            prevTouchCount = Input.touchCount;
        }

        public void Update() {
            int currentTouchCount = Input.touchCount;
            if (currentTouchCount > prevTouchCount) {
                hero.Jump();
            }
            prevTouchCount = currentTouchCount;
        }
    }
}
