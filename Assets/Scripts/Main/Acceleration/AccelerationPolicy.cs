using UnityEngine;

namespace TempleRun.Main.Acceleration {
    public abstract class AccelerationPolicy : MonoBehaviour {
        public abstract Vector3 UpdateVelocity(Vector3 velocity, HeroPrefab heroDef);
    }
}
