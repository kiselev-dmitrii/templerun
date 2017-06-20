using UnityEngine;

namespace TempleRun.Main.Acceleration {
    public class LinearAccelerationPolicy : AccelerationPolicy {
        public Vector3 LinearAcceleration;

        public override Vector3 UpdateVelocity(Vector3 velocity, HeroPrefab heroDef) {
            velocity += LinearAcceleration * Time.deltaTime;
            velocity.x = Mathf.Clamp(velocity.x, 0, heroDef.MaxVelocity);
            return velocity;
        }
    }
}
