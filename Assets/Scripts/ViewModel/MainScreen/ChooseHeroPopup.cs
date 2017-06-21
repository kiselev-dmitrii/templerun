using System;
using System.Linq;
using EZData;
using TempleRun.Main;
using TempleRun.Utils;
using UnityEngine;

namespace TempleRun.ViewModel.MainScreen {
    public class HeroItem : Context {
        public HeroPrefab Prefab { get; private set; }

        #region Property Name
        private readonly Property<String> _privateNameProperty = new Property<String>();
        public Property<String> NameProperty { get { return _privateNameProperty; } }
        public String Name {
            get { return NameProperty.GetValue(); }
            set { NameProperty.SetValue(value); }
        }
        #endregion

        #region Property GameObject
        private readonly Property<GameObject> _privateGameObjectProperty = new Property<GameObject>();
        public Property<GameObject> GameObjectProperty { get { return _privateGameObjectProperty; } }
        public GameObject GameObject {
            get { return GameObjectProperty.GetValue(); }
            set { GameObjectProperty.SetValue(value); }
        }
        #endregion

        public HeroItem(HeroPrefab prefab) {
            Prefab = prefab;
            Name = prefab.Name;
            GameObject = prefab.gameObject;
        }
    }

    public class ChooseHeroPopup : Popup {
        #region Collection Heroes
        private readonly Collection<HeroItem> _privateHeroes = new Collection<HeroItem>(false);
        public Collection<HeroItem> Heroes {
            get { return _privateHeroes; }
        }
        #endregion

        #region SelectedHero
        public readonly VariableContext<HeroItem> SelectedHeroEzVariableContext = new VariableContext<HeroItem>(null);
        public HeroItem SelectedHero {
            get { return SelectedHeroEzVariableContext.Value; }
            set { SelectedHeroEzVariableContext.Value = value; }
        }
        #endregion

        private int selectedHeroIdx;
        private readonly Action onSelectButtonClick;
        private readonly Action onTitlebarClick;

        public ChooseHeroPopup(HeroPrefab[] heroes, Action onTitlebarClick, Action onSelectButtonClick) {
            Heroes.SetItems(heroes.Select(x => new HeroItem(x)));
            this.onTitlebarClick = onTitlebarClick;
            this.onSelectButtonClick = onSelectButtonClick;
            SelectHero(0);
        }

        public void SelectHero(int index) {
            selectedHeroIdx = index;
            SelectedHero = Heroes[selectedHeroIdx];
        }

        #region
        public void OnNextButtonClick() {
            int nextIndex = (selectedHeroIdx + 1);
            if (nextIndex >= Heroes.Count) nextIndex = 0;
            SelectHero(nextIndex);
        }

        public void OnPreviousButtonClick() {
            int previousIndex = selectedHeroIdx - 1;
            if (previousIndex < 0) previousIndex = Heroes.Count - 1;
            SelectHero(previousIndex);
        }

        public void OnTitlebarClick() {
            onTitlebarClick();
        }

        public void OnSelectButtonClick() {
            onSelectButtonClick();
        }
        #endregion

    }
}
