using System.Collections.Generic;
using UnityEngine;

namespace TempleRun {
    public class PathRenderer : MonoBehaviour {
        public PathSegment Segment;
        public int SegmentPoolSize;
        public Camera Camera;
        public float CameraWidth;

        private Pool mSegmentPool;
        public int mStartIndex;
        public int mEndIndex;
        private List<PathSegment> mUsedSegments;

        protected void Awake() {
            mSegmentPool = gameObject.AddComponent<Pool>();
            mSegmentPool.Initialize(Segment.gameObject, SegmentPoolSize);

        }

        protected void Update() {
            float x1 = Camera.transform.position.x - CameraWidth / 2;
            float x2 = Camera.transform.position.x + CameraWidth / 2;
            mStartIndex = Mathf.CeilToInt(x1 / Segment.Length);
            mEndIndex = Mathf.FloorToInt(x2 / Segment.Length);

            foreach (var segment in mUsedSegments) {
                if (!IsInRange(segment.Index)) {
                    
                }
            }
        }

        private bool IsInRange(int segmentIndex) {
            return segmentIndex >= mStartIndex && segmentIndex <= mEndIndex;
        }
    }
}
