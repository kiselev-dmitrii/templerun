using System;
using System.Linq;
using EZData;
using TempleRun.Game;
using UnityEngine;

namespace TempleRun.ViewModel {
    public enum ScreenState {
        HeroScreen = 1,
        WorldScreen = 2
    }

    public class WorldItem : EZData.Context {
        public WorldDef Def { get; private set; }

        #region Property Name
        private readonly Property<String> _privateNameProperty = new Property<String>();
        public Property<String> NameProperty { get { return _privateNameProperty; } }
        public String Name {
            get { return NameProperty.GetValue(); }
            set { NameProperty.SetValue(value); }
        }
        #endregion

        #region Property Icon
        private readonly Property<Sprite> _privateIconProperty = new Property<Sprite>();
        public Property<Sprite> IconProperty { get { return _privateIconProperty; } }
        public Sprite Icon {
            get { return IconProperty.GetValue(); }
            set { IconProperty.SetValue(value); }
        }
        #endregion

        #region Property IsSelected
        private readonly Property<bool> _privateIsSelectedProperty = new Property<bool>();
        public Property<bool> IsSelectedProperty { get { return _privateIsSelectedProperty; } }
        public bool IsSelected {
            get { return IsSelectedProperty.GetValue(); }
            set { IsSelectedProperty.SetValue(value); }
        }
        #endregion

        private readonly Action<WorldItem> onClick;

        public WorldItem(WorldDef def, Action<WorldItem> onClick) {
            Def = def;
            Name = def.Name;
            Icon = def.Icon;
            this.onClick = onClick;
        }

        public void OnClick() {
            if (onClick != null) {
                onClick(this);
            }
        }
    }

    public class HeroItem : Context {
        public HeroDef Def { get; private set; }

        #region Property Name
        private readonly Property<String> _privateNameProperty = new Property<String>();
        public Property<String> NameProperty { get { return _privateNameProperty; } }
        public String Name {
            get { return NameProperty.GetValue(); }
            set { NameProperty.SetValue(value); }
        }
        #endregion

        #region Property Prefab
        private readonly Property<GameObject> _privatePrefabProperty = new Property<GameObject>();
        public Property<GameObject> PrefabProperty { get { return _privatePrefabProperty; } }
        public GameObject Prefab {
            get { return PrefabProperty.GetValue(); }
            set { PrefabProperty.SetValue(value); }
        }
        #endregion

        public HeroItem(HeroDef def) {
            Def = def;
            Name = def.Name;
            Prefab = def.gameObject;
        }
    }

    public class MainScreen : Context {
        #region Collection Heroes
        private readonly Collection<HeroItem> _privateHeroes = new Collection<HeroItem>(false);
        public Collection<HeroItem> Heroes {
            get { return _privateHeroes; }
        }
        #endregion

        #region Collection Worlds
        private readonly Collection<WorldItem> _privateWorlds = new Collection<WorldItem>(false);
        public Collection<WorldItem> Worlds {
            get { return _privateWorlds; }
        }
        #endregion

        #region SelectedHero
        public readonly VariableContext<HeroItem> SelectedHeroEzVariableContext = new VariableContext<HeroItem>(null);
        public HeroItem SelectedHero {
            get { return SelectedHeroEzVariableContext.Value; }
            set { SelectedHeroEzVariableContext.Value = value; }
        }
        #endregion

        #region SelectedWorld
        public readonly VariableContext<WorldItem> SelectedWorldEzVariableContext = new VariableContext<WorldItem>(null);
        public WorldItem SelectedWorld {
            get { return SelectedWorldEzVariableContext.Value; }
            set {
                SelectedWorldEzVariableContext.Value = value;
                IsWorldSelected = value != null;
            }
        }
        #endregion

        #region Property IsWorldSelected
        private readonly Property<bool> _privateIsWorldSelectedProperty = new Property<bool>();
        public Property<bool> IsWorldSelectedProperty { get { return _privateIsWorldSelectedProperty; } }
        public bool IsWorldSelected {
            get { return IsWorldSelectedProperty.GetValue(); }
            set { IsWorldSelectedProperty.SetValue(value); }
        }
        #endregion

        #region Property State
        private readonly Property<int> _privateStateProperty = new Property<int>();
        public Property<int> StateProperty { get { return _privateStateProperty; } }
        public ScreenState State {
            get { return (ScreenState)StateProperty.GetValue(); }
            set { StateProperty.SetValue((int)value); }
        }
        #endregion

        private int selectedHeroIdx;

        public const String HeroDirectory = "Heroes";
        public const String WorldDirectory = "Worlds";

        public MainScreen() {
            HeroDef[] heroes = Resources.LoadAll<GameObject>(HeroDirectory).Select(x => x.GetComponent<HeroDef>()).ToArray();
            WorldDef[] worlds = Resources.LoadAll<WorldDef>(WorldDirectory);

            Heroes.SetItems(heroes.Select(x => new HeroItem(x)));
            Worlds.SetItems(worlds.Select(x => new WorldItem(x, OnWorldItemClick)));
            State = ScreenState.HeroScreen;
            SelectHero(0);
        }

        public void SelectHero(int index) {
            selectedHeroIdx = index;
            SelectedHero = Heroes[selectedHeroIdx];
        }

        public void SelectWorld(WorldItem item) {
            if (SelectedWorld != null) {
                SelectedWorld.IsSelected = false;
            }
            SelectedWorld = item;
            if (SelectedWorld != null) {
                SelectedWorld.IsSelected = true;
            }
        }

        #region Handlers
        public void OnNextHeroButtonClick() {
            int nextIndex = (selectedHeroIdx + 1);
            if (nextIndex >= Heroes.Count) nextIndex = 0;
            SelectHero(nextIndex);
        }

        public void OnPreviousHeroButtonClick() {
            int previousIndex = selectedHeroIdx - 1;
            if (previousIndex < 0) previousIndex = Heroes.Count - 1;
            SelectHero(previousIndex);
        }

        public void OpenHeroScreenButtonClick() {
            State = ScreenState.HeroScreen;
        }

        public void OpenWorldScreenButtonClick() {
            State = ScreenState.WorldScreen;
        }

        public void OnRunGameButtonClick() {
            
        }

        public void OnWorldItemClick(WorldItem item) {
            SelectWorld(item);
        }
        #endregion

    }
}
