using System;
using EZData;
using TempleRun.Utils;
using UnityEngine;

namespace TempleRun.View {
    public class StringPrefabBinding : Binding {
        public String Format = "{0}";
        private Property<String> property;

        protected override void Bind() {
            var context = GetContext(Path);
            if (context == null) return;

            property = context.FindProperty<String>(Path, this);
            if (property == null) return;

            property.OnChange += OnChange;
        }

        protected override void Unbind() {
            if (property == null) return;
            property.OnChange -= OnChange;
            property = null;
        }

        protected override void OnChange() {
            transform.DestroyChildren();
            if (property == null) return;

            String path = property.GetValue();
            if (String.IsNullOrEmpty(path)) return;

            String formattedPath = String.Format(Format, path);
            GameObject prefab = Resources.Load<GameObject>(formattedPath);
            var go = Instantiate(prefab);
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
        }
    }
}
