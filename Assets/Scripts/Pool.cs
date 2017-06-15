using System.Collections.Generic;
using UnityEngine;

namespace TempleRun {
    public class Pool : MonoBehaviour {
        private List<GameObject> mObjects;
        private int mLastUsedIdx;
        private int mSize;

        public void Initialize(GameObject prefab, int size) {
            mSize = size;
            mObjects = new List<GameObject>();
            mLastUsedIdx = -1;

            for (int i = 0; i < mSize; ++i) {
                var go = GameObject.Instantiate(prefab, transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localEulerAngles = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.SetActive(false);

                mObjects.Add(go);
            }
        }


        public GameObject Spawn() {
            mLastUsedIdx = (mLastUsedIdx+1)%mSize;
            var go = mObjects[mLastUsedIdx];
            go.SetActive(true);
            return go;
        }

        public void Unspawn(GameObject go) {
            go.transform.localPosition = Vector3.zero;
            go.transform.localEulerAngles = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.SetActive(false);
        }
    }
}
