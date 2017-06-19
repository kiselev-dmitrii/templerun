using UnityEngine;

namespace TempleRun.Assets.Scripts.Utils {
    public class AutoRotate : MonoBehaviour {
        public float Angle = 60;
        public Vector3 Axis = new Vector3(0, 1, 0);
        public Space Space = Space.Self;

        public void Update() {
            transform.Rotate(Axis, Angle * Time.deltaTime, Space);
        }
    }
}
