using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TempleRun {
    public class PathRenderer : MonoBehaviour {
        public class PathSegmentPool : Pool<PathSegment> {}

        public PathSegment Segment;
        public int SegmentPoolSize;
        public Camera Camera;
        public float CameraWidth;

        private PathSegmentPool segmentPool;
        private Dictionary<int, PathSegment> usedSegments;
        private List<int> removeList; 

        protected void Awake() {
            segmentPool = gameObject.AddComponent<PathSegmentPool>();
            segmentPool.Initialize(Segment, SegmentPoolSize);
            usedSegments = new Dictionary<int, PathSegment>();
            removeList = new List<int>(Mathf.RoundToInt(CameraWidth/Segment.Width));
        }

        protected void Update() {
            float x1 = Camera.transform.position.x - CameraWidth / 2;
            float x2 = Camera.transform.position.x + CameraWidth / 2;
            int start = Mathf.CeilToInt(x1 / Segment.Width);
            int end = Mathf.FloorToInt(x2 / Segment.Width);

            removeList.Clear();
            foreach (var pair in usedSegments) {
                int idx = pair.Key;
                if (idx < start || idx > end) {
                    removeList.Add(idx);
                }
            }
            foreach (var idx in removeList) {
                var segment = usedSegments[idx];
                usedSegments.Remove(idx);
                segmentPool.Unspawn(segment);
            }

            for (int i = start; i <= end; i++) {
                if (!usedSegments.ContainsKey(i)) {
                    var segment = segmentPool.Spawn();
                    segment.transform.localPosition = new Vector3(Segment.Width*i, 0, 0);
                    segment.transform.localEulerAngles = Vector3.zero;
                    segment.transform.localScale = Vector3.one;
                    usedSegments.Add(i, segment);
                }
            }
        }
    }
}
