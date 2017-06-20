using System;
using System.Collections.Generic;
using UnityEngine;

namespace TempleRun.Main {
    public class TileBackground : MonoBehaviour {
        public Sprite Tile;
        public float Scale;
        public Color Color = Color.white;
        public int PoolSize;
        public Camera Camera;
        public float CameraWidth;

        private float tileWidth;
        private Stack<GameObject> pool;
        private Dictionary<int, GameObject> usedTiles;
        private List<int> invisibleIndices;

        public void Awake() {
            pool = InitializePool(Tile, PoolSize);
            tileWidth = Tile.rect.width * Scale / Tile.pixelsPerUnit;
            usedTiles = new Dictionary<Int32, GameObject>();
            invisibleIndices = new List<Int32>(Mathf.CeilToInt(CameraWidth / tileWidth));
        }

        public void Update() {
            float x1 = Camera.transform.position.x - CameraWidth / 2;
            float x2 = Camera.transform.position.x + CameraWidth / 2;
            int start = Mathf.FloorToInt(x1 / tileWidth);
            int end = Mathf.CeilToInt(x2 / tileWidth);

            invisibleIndices.Clear();
            foreach (var pair in usedTiles) {
                int idx = pair.Key;
                if (idx < start || idx > end) {
                    invisibleIndices.Add(idx);
                }
            }
            foreach (var index in invisibleIndices) {
                var usedTile = usedTiles[index];
                usedTiles.Remove(index);
                UnspawnTile(usedTile);
            }

            for (int i = start; i <= end; i++) {
                if (!usedTiles.ContainsKey(i)) {
                    var tile = SpawnTile();
                    tile.transform.localPosition = new Vector3(tileWidth * i, 0, 0);
                    tile.transform.localEulerAngles = Vector3.zero;
                    tile.transform.localScale = new Vector3(Scale, Scale, Scale);
                    usedTiles.Add(i, tile);
                }
            }
        }



        public Stack<GameObject> InitializePool(Sprite sprite, int size) {
            var result = new Stack<GameObject>();

            for (int i = 0; i < size; ++i) {
                var go = new GameObject(sprite.name);
                var sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;
                sr.color = Color;

                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localEulerAngles = Vector3.zero;
                go.transform.localScale = Vector3.one*Scale;
                go.gameObject.SetActive(false);
                result.Push(go);
            }
            return result;
        }


        public GameObject SpawnTile() {
            if (pool.Count == 0) {
                throw new InvalidOperationException("Pool has not enough objects");
            }

            GameObject component = pool.Pop();
            component.gameObject.SetActive(true);
            return component;
        }

        public void UnspawnTile(GameObject component) {
            component.transform.parent = transform;
            component.transform.localPosition = Vector3.zero;
            component.transform.localEulerAngles = Vector3.zero;
            component.gameObject.SetActive(false);
            pool.Push(component);
        }

    }
}
