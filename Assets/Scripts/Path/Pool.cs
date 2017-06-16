using System;
using System.Collections.Generic;
using UnityEngine;

namespace TempleRun {
    public class Pool<T> : MonoBehaviour where T : MonoBehaviour {
        private Stack<T> freeObjects;

        public void Initialize(T prefab, int size) {
            freeObjects = new Stack<T>();

            for (int i = 0; i < size; ++i) {
                var component = Instantiate<T>(prefab, transform);
                component.transform.localPosition = Vector3.zero;
                component.transform.localEulerAngles = Vector3.zero;
                component.transform.localScale = Vector3.one;
                component.gameObject.SetActive(false);
                freeObjects.Push(component);
            }
        }


        public T Spawn() {
            if (freeObjects.Count == 0) {
                throw new InvalidOperationException("Pool has not enough objects");
            }

            T component = freeObjects.Pop();
            component.gameObject.SetActive(true);
            return component;
        }

        public void Unspawn(T component) {
            component.transform.parent = transform;
            component.transform.localPosition = Vector3.zero;
            component.transform.localEulerAngles = Vector3.zero;
            component.transform.localScale = Vector3.one;
            component.gameObject.SetActive(false);
            freeObjects.Push(component);
        }


    }
}
