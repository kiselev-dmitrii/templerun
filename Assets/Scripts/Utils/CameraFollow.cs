using UnityEngine;

namespace TempleRun.Utils {
    public class CameraFollow : MonoBehaviour {
        public Transform Target;
        public float Smooth = 30f;
        private Vector3 offset;

        protected void Start() {
            offset = transform.position - Target.position;
        }

        protected void Update() {
            Vector3 newPosition = Target.position + offset;
            transform.position = Vector3.Lerp(transform.position, newPosition, Smooth * Time.deltaTime);
        }
    }
}
