using UnityEngine;
using UnityEngine.UI;

namespace TempleRun.View {
    [RequireComponent(typeof(Button))]
    public class InteractableBinding : BooleanBinding {
        private Button button;

        public override void Awake() {
            button = GetComponent<Button>();
        }

        protected override void ApplyNewValue(bool newValue) {
            button.interactable = newValue;
        }
    }
}
