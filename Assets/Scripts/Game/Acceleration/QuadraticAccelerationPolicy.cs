using UnityEngine;

namespace TempleRun.Game.Acceleration {
    [CreateAssetMenu]
    public class LinearAccelerationPolicy : AccelerationPolicy {
        public Vector3 LinearAcceleration;


        public override void Initialize() {
        }

        public override Vector3 UpdateVelocity(Vector3 velocity, HeroDef heroDef) {
            velocity += LinearAcceleration * Time.deltaTime;
            velocity.x = Mathf.Clamp(velocity.x, 0, heroDef.MaxVelocity);
            return velocity;
        }
    }
}
