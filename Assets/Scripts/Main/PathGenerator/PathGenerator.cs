using System;
using System.Collections.Generic;
using TempleRun.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TempleRun.Main.PathGenerator {
    public class PathGenerator : MonoBehaviour {
        public class PathSegmentPool : Pool<PathSegment> {}
        public class ObstaclePool : Pool<PathObstacle> { }
        public class ItemPool : Pool<PathItem> { }

        public PathSegment Segment;
        public int SegmentPoolSize;

        public PathObstacle[] Obstacles;
        public IntRange ObstacleDistance = new IntRange(10, 20);

        public PathItem Coin;
        public Vector3 CoinOffset = new Vector3(0, 4f, 0);
        public IntRange CoinChainLength = new IntRange(3, 10);
        public IntRange CoinGapLength = new IntRange(10, 20);

        public Camera Camera;
        public float CameraWidth;

        private PathSegmentPool segmentPool;
        private Dictionary<int, Pool<PathObstacle>> obstaclePools;
        private ItemPool itemPool;
        private Dictionary<int, PathSegment> usedSegments;
        private Dictionary<int, PathObstacle> usedObstacles;
        private Dictionary<int, PathItem> usedItems;

        private List<int> invisibleIndices;
        private IEnumerator<PathObstacle> obstacleGenerator;
        private IEnumerator<PathItem> itemGenerator;

        protected void Awake() {
            segmentPool = gameObject.AddComponent<PathSegmentPool>();
            segmentPool.Initialize(Segment, SegmentPoolSize);

            obstaclePools = new Dictionary<int, Pool<PathObstacle>>();
            foreach (var obstacle in Obstacles) {
                var pool = gameObject.AddComponent<ObstaclePool>();
                pool.Initialize(obstacle, 10);
                obstaclePools.Add(obstacle.Id, pool);
            }

            itemPool = gameObject.AddComponent<ItemPool>();
            itemPool.Initialize(Coin, 10);

            usedSegments = new Dictionary<Int32, PathSegment>();
            usedObstacles = new Dictionary<Int32, PathObstacle>();
            usedItems = new Dictionary<Int32, PathItem>();
            invisibleIndices = new List<int>(Mathf.RoundToInt(CameraWidth/Segment.Width));

            obstacleGenerator = GetNextObstacle();
            itemGenerator = GetNextItem();
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

                PathItem usedItem = null;
                if (usedItems.TryGetValue(index, out usedItem)) {
                    usedItems.Remove(index);
                    UnspawnItem(usedItem);
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

                    var item = SpawnNextItem();
                    if (item != null) {
                        float obstacleOffset = obstacle != null ? obstacle.Height : 0;
                        item.transform.localPosition = new Vector3(Segment.Width * (i + 0.5f), obstacleOffset, 0) + CoinOffset;
                        item.transform.localEulerAngles = Vector3.zero;
                        item.transform.localScale = Vector3.one;
                        usedItems.Add(i, item);
                    }
                }
            }
        }

        private PathObstacle SpawnNextObstacle() {
            var prefab = obstacleGenerator.Current;
            obstacleGenerator.MoveNext();
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

        private PathItem SpawnNextItem() {
            var prefab = itemGenerator.Current;
            itemGenerator.MoveNext();
            if (prefab == null) return null;
            else {
                return itemPool.Spawn();
            }
        }

        private void UnspawnItem(PathItem item) {
            item.OnUnspawn();
            itemPool.Unspawn(item);
        }

        private IEnumerator<PathItem> GetNextItem() {
            int index = 0;
            while (true) {
                int gap = Random.Range(CoinGapLength.Min, CoinGapLength.Max);
                for (int i = 0; i < gap; ++i) {
                    yield return null;
                    ++index;
                }

                int chain = Random.Range(CoinChainLength.Min, CoinChainLength.Max);
                for (int i = 0; i < chain; ++i) {
                    yield return Coin;
                    ++index;
                }
            }
        }
    }
}
