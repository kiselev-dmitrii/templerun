using UnityEngine;

namespace TempleRun.Main.PathGenerator {
    public class PathItem : MonoBehaviour {
        public int Score;
        private Animator animator;

        protected void Awake() {
            animator = GetComponent<Animator>();
        }

        public void Take() {
            animator.SetBool("IsTaken", true);
        }

        public void OnUnspawn() {
            animator.SetBool("IsTaken", false);   
            animator.Update(Time.deltaTime);
        }
    }
}
