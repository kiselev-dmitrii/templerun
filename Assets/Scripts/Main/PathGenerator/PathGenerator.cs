using System;
using System.Collections.Generic;
using TempleRun.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TempleRun.Main.PathGenerator {
    public class PathGenerator : MonoBehaviour {
        public class PathSegmentPool : Pool<PathSegment> {}
        public class ObstaclePool : Pool<PathObstacle> { }

        public PathSegment Segment;
        public int SegmentPoolSize;
        public PathObstacle[] Obstacles;
        public int ObstaclePoolSize;
        public IntRange ObstacleDistance = new IntRange(10, 20);
        public Camera Camera;
        public float CameraWidth;

        private PathSegmentPool segmentPool;
        private Dictionary<int, Pool<PathObstacle>> obstaclePools;
        private Dictionary<int, PathSegment> usedSegments;
        private Dictionary<int, PathObstacle> usedObstacles;

        private List<int> invisibleIndices;
        private IEnumerator<PathObstacle> obstacleChain;

        protected void Awake() {
            segmentPool = gameObject.AddComponent<PathSegmentPool>();
            segmentPool.Initialize(Segment, SegmentPoolSize);

            obstaclePools = new Dictionary<int, Pool<PathObstacle>>();
            foreach (var obstacle in Obstacles) {
                var pool = gameObject.AddComponent<ObstaclePool>();
                pool.Initialize(obstacle, 10);
                obstaclePools.Add(obstacle.Id, pool);
            }

            usedSegments = new Dictionary<int, PathSegment>();
            usedObstacles = new Dictionary<Int32, PathObstacle>();
            invisibleIndices = new List<int>(Mathf.RoundToInt(CameraWidth/Segment.Width));
            obstacleChain = GetNextObstacle();
        }

        protected void Update() {
            float x1 = Camera.transform.position.x - CameraWidth / 2;
            float x2 = Camera.transform.position.x + CameraWidth / 2;
            int start = Mathf.CeilToInt(x1 / Segment.Width);
            int end = Mathf.FloorToInt(x2 / Segment.Width);

            invisibleIndices.Clear();
            foreach (var pair in usedSegments) {
                int idx = pair.Key;
                if (idx < start || idx > end) {
                    invisibleIndices.Add(idx);
                }
            }
            foreach (var index in invisibleIndices) {
                var usedSegment = usedSegments[index];
                usedSegments.Remove(index);
                segmentPool.Unspawn(usedSegment);

                PathObstacle usedObstacle = null;
                if (usedObstacles.TryGetValue(index, out usedObstacle)) {
                    usedObstacles.Remove(index);
                    UnspawnObstacle(usedObstacle);
                }
            }

            for (int i = start; i <= end; i++) {
                if (!usedSegments.ContainsKey(i)) {
                    var segment = segmentPool.Spawn();
                    segment.transform.localPosition = new Vector3(Segment.Width * i, 0, 0);
                    segment.transform.localEulerAngles = Vector3.zero;
                    segment.transform.localScale = Vector3.one;
                    usedSegments.Add(i, segment);

                    var obstacle = SpawnNextObstacle();
                    if (obstacle != null) {
                        obstacle.transform.localPosition = new Vector3(Segment.Width*(i+0.5f), 0, 0);
                        obstacle.transform.localEulerAngles = Vector3.zero;
                        obstacle.transform.localScale = Vector3.one;
                        usedObstacles.Add(i, obstacle);
                    }
                }
            }
        }

        private PathObstacle SpawnNextObstacle() {
            var prefab = obstacleChain.Current;
            obstacleChain.MoveNext();
            if (prefab == null) return null;
            else {
                var obstaclePool = obstaclePools[prefab.Id];
                return obstaclePool.Spawn();
            }
        }

        private void UnspawnObstacle(PathObstacle obstacle) {
            var pool = obstaclePools[obstacle.Id];
            pool.Unspawn(obstacle);
        }

        private IEnumerator<PathObstacle> GetNextObstacle() {
            var random = new WeightedRandom<PathObstacle>(Obstacles, x => x.Probability);

            int index = 0;
            while (true) {
                PathObstacle obstacle = random.GetRandom();
                yield return obstacle;
                ++index;

                int distance = Random.Range(ObstacleDistance.Min, ObstacleDistance.Max);
                for (int i = 0; i < distance; ++i) {
                    yield return null;
                    ++index;
                }
            }
        }
    }
}
