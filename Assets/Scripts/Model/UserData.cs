using EZData;

namespace TempleRun.Assets.Scripts.Model {
    public class UserData : Context {
        #region Collection Heroes
        private readonly Collection<HeroData> _privateHeroes = new Collection<HeroData>(false);
        public Collection<HeroData> Heroes {
            get { return _privateHeroes; }
        }
        #endregion

        #region CurrentHero
        public readonly VariableContext<HeroData> CurrentHeroEzVariableContext = new VariableContext<HeroData>(null);
        public HeroData CurrentHero {
            get { return CurrentHeroEzVariableContext.Value; }
            set { CurrentHeroEzVariableContext.Value = value; }
        }
        #endregion
        
        public UserData() {
            Heroes.SetItems(new [] {
                new HeroData(0, "Crazy rabbit", "Rabbit"), 
                new HeroData(1, "Supper sheep", "Sheep"),
                new HeroData(2, "Fun frogg", "Frog"), 
            });
            SetCurrentHero(0);
        }

        public void SetCurrentHero(int idx) {
            CurrentHero = Heroes[idx];
        }
    }
}
