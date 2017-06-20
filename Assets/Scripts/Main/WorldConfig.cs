using System;
using UnityEngine;

namespace TempleRun.Main {
    [CreateAssetMenu]
    public class WorldConfig : ScriptableObject {
        public String Name;
        public Sprite Icon;
        public World World;

    }
}
