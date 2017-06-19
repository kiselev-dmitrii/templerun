using System;
using EZData;

namespace TempleRun.Assets.Scripts.Model {
    public class HeroData : EZData.Context {
        public int Index { get; private set; }

        #region Property Name
        private readonly Property<String> _privateNameProperty = new Property<String>();
        public Property<String> NameProperty { get { return _privateNameProperty; } }
        public String Name {
            get { return NameProperty.GetValue(); }
            private set { NameProperty.SetValue(value); }
        }
        #endregion

        #region Property Prefab
        private readonly Property<String> _privatePrefabProperty = new Property<String>();
        public Property<String> PrefabProperty { get { return _privatePrefabProperty; } }
        public String Prefab {
            get { return PrefabProperty.GetValue(); }
            private set { PrefabProperty.SetValue(value); }
        }
        #endregion

        public HeroData(int index, String name, String prefab) {
            Index = index;
            Name = name;
            Prefab = prefab;
        }
    }
}
