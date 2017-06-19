using EZData;

namespace TempleRun.Model {
    public class ApplicationModel : EZData.Context {
        public static ApplicationModel Instance { get; private set; }

        #region Player
        public readonly VariableContext<UserData> PlayerEzVariableContext = new VariableContext<UserData>(null);
        public UserData Player {
            get { return PlayerEzVariableContext.Value; }
            set { PlayerEzVariableContext.Value = value; }
        }
        #endregion

        public ApplicationModel() {
            Instance = this;
            Player = new UserData();
        }
    }
}
