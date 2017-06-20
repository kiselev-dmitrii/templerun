using System;
using TempleRun.Main;
using UnityEngine;
using UnityEngine.UI;

namespace TempleRun.View {
    public class VelocityIndicator : MonoBehaviour {
        [SerializeField]
        private Text widget;
        private Hero hero;

        public void Initialize(Hero hero) {
            this.hero = hero;
            enabled = true;
        }

        public void Update() {
            if (hero == null) enabled = false;
            widget.text = String.Format("Speed: {0:0.00} m/s", hero.HorizontalSpeed);
        }

    }
}
