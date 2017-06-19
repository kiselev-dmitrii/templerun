using System;
using EZData;
using UnityEngine.UI;

namespace TempleRun.Assets.Scripts.View {
    public class StringTextBinding : Binding {
        public String Format = "{0}";
        private Property<String> property;
        private Text text;

        public override void Awake() {
            text = GetComponent<Text>();
        }

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
            if (property == null) return;
            text.text = property.GetValue();
        }
    }
}
