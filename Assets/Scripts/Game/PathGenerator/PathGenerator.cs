using System;
using System.Collections.Generic;
using TempleRun.Assets.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TempleRun.Assets.Scripts.Game.PathGenerator {
    public class PathRenderer : MonoBehaviour {
        public class PathSegmentPool : Pool<PathSegment> {}
        public class ObstaclePool : Pool<Obstacle> { }

        public PathSegment Segment;
        public int PoolSize;
        public Camera Camera;
        public float CameraWidth;
        public Obstacle[] Obstacles;
        public IntRange ObstacleDistance = new IntRange(10, 20);

        private PathSegmentPool segmentPool;
        private Dictionary<int, Pool<Obstacle>> obstaclePools;
        private Dictionary<int, PathSegment> usedSegments;
        private Dictionary<int, Obstacle> usedObstacles;

        private List<int> invisibleIndices;
        private IEnumerator<Obstacle> obstacleChain;

        protected void Awake() {
            segmentPool = gameObject.AddComponent<PathSegmentPool>();
            segmentPool.Initialize(Segment, PoolSize);

            obstaclePools = new Dictionary<int, Pool<Obstacle>>();
            foreach (var obstacle in Obstacles) {
                var pool = gameObject.AddComponent<ObstaclePool>();
                pool.Initialize(obstacle, 10);
                obstaclePools.Add(obstacle.Id, pool);
            }

            usedSegments = new Dictionary<int, PathSegment>();
            usedObstacles = new Dictionary<Int32, Obstacle>();
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

                Obstacle usedObstacle = null;
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

        private Obstacle SpawnNextObstacle() {
            var prefab = obstacleChain.Current;
            obstacleChain.MoveNext();
            if (prefab == null) return null;
            else {
                var obstaclePool = obstaclePools[prefab.Id];
                return obstaclePool.Spawn();
            }
        }

        private void UnspawnObstacle(Obstacle obstacle) {
            var pool = obstaclePools[obstacle.Id];
            pool.Unspawn(obstacle);
        }

        private IEnumerator<Obstacle> GetNextObstacle() {
            var random = new WeightedRandom<Obstacle>(Obstacles, x => x.Probability);

            int index = 0;
            while (true) {
                Obstacle obstacle = random.GetRandom();
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
