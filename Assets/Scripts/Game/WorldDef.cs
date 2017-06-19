using System;
using TempleRun.Game.PathGenerator;
using UnityEngine;

namespace TempleRun.Game {
    [CreateAssetMenu]
    public class WorldDef : ScriptableObject {
        public String Name;
        public Sprite Icon;
        public PathObstacle[] Obstacles;
        public Sprite FarBackground;
        public Sprite NearBackground;
        public Vector3 Gravity = new Vector3(0f, -9.8f, 0f);

    }
}
