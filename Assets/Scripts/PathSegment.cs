using UnityEngine;

namespace TempleRun {
    public class PathSegment : MonoBehaviour {
        public float Length;

        public int Index {
            get { return Mathf.CeilToInt(transform.position.x / Length); }
        }
    }
}
