using System;
using System.Linq;
using EZData;
using TempleRun.Main;
using TempleRun.Utils;
using UnityEngine;

namespace TempleRun.ViewModel.MainScreen {
    public class WorldItem : EZData.Context {
        public WorldConfig Config { get; private set; }

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

        public WorldItem(WorldConfig config, Action<WorldItem> onClick) {
            Config = config;
            Name = config.Name;
            Icon = config.Icon;
            IsSelected = false;
            this.onClick = onClick;
        }

        public void OnClick() {
            if (onClick != null) {
                onClick(this);
            }
        }
    }

    public class ChooseWorldPopup : Popup {
        #region Collection Worlds
        private readonly Collection<WorldItem> _privateWorlds = new Collection<WorldItem>(false);
        public Collection<WorldItem> Worlds {
            get { return _privateWorlds; }
        }
        #endregion

        #region SelectedWorld
        public readonly VariableContext<WorldItem> SelectedWorldEzVariableContext = new VariableContext<WorldItem>(null);
        public WorldItem SelectedWorld {
            get { return SelectedWorldEzVariableContext.Value; }
            set { SelectedWorldEzVariableContext.Value = value; }
        }
        #endregion

        private readonly Action onSelectButtonClick;

        public ChooseWorldPopup(WorldConfig[] worlds, Action onSelectButtonClick) {
            Worlds.SetItems(worlds.Select(x => new WorldItem(x, OnWorldItemClick)));
            this.onSelectButtonClick = onSelectButtonClick;
            SelectWorld(Worlds.FirstOrDefault());
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
        public void OnWorldItemClick(WorldItem item) {
            SelectWorld(item);
        }

        public void OnSelectButtonClick() {
            onSelectButtonClick();
        }
        #endregion
    }
}
