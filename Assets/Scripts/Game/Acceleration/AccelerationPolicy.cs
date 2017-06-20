using UnityEngine;

namespace TempleRun.Game.Acceleration {
    public abstract class AccelerationPolicy : ScriptableObject {
        public abstract void Initialize();
        public abstract Vector3 UpdateVelocity(Vector3 velocity, HeroDef heroDef);
    }
}
