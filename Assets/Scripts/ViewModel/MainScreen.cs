using EZData;
using TempleRun.Model;

namespace TempleRun.ViewModel {
    public enum ScreenState {
        HeroScreen = 1,
        WorldScreen = 2
    }

    public class MainScreen : EZData.Context {
        #region Property IsVisible
        private readonly EZData.Property<bool> _privateIsVisibleProperty = new EZData.Property<bool>();
        public EZData.Property<bool> IsVisibleProperty { get { return _privateIsVisibleProperty; } }
        public bool IsVisible {
            get { return IsVisibleProperty.GetValue(); }
            private set { IsVisibleProperty.SetValue(value); }
        }
        #endregion

        #region User
        public readonly VariableContext<UserData> UserEzVariableContext = new VariableContext<UserData>(null);
        public UserData User {
            get { return UserEzVariableContext.Value; }
            set { UserEzVariableContext.Value = value; }
        }
        #endregion

        public MainScreen() {
            User = ApplicationModel.Instance.Player;
            IsVisible = true;
        }

        #region Handlers
        public void OnNextHeroButton() {
            int nextIndex = (User.CurrentHero.Index + 1);
            if (nextIndex >= User.Heroes.Count) nextIndex = 0;
            User.SetCurrentHero(nextIndex);
        }

        public void OnPreviousHeroButton() {
            int previousIndex = User.CurrentHero.Index - 1;
            if (previousIndex < 0) previousIndex = User.Heroes.Count - 1;
            User.SetCurrentHero(previousIndex);
        }
        #endregion

    }
}
