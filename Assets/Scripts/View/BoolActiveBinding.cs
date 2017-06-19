namespace TempleRun.Assets.Scripts.View {
    public class BoolActiveBinding : BooleanBinding {
        protected override void ApplyNewValue(bool newValue) {
            gameObject.SetActive(newValue);
        }
    }
}
