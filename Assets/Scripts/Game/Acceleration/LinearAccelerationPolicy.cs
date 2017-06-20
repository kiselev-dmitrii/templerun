using UnityEngine;

namespace TempleRun.Game.Acceleration {
    [CreateAssetMenu]
    public class QuadraticAccelerationPolicy : AccelerationPolicy {
        public Vector3 LinearAcceleration;
        public Vector3 QuadraticAcceleration;
        private Vector3 acceleration;

        public override void Initialize() {
            acceleration = LinearAcceleration;
        }

        public override Vector3 UpdateVelocity(Vector3 velocity, HeroDef heroDef) {
            if (velocity.x >= heroDef.MaxVelocity / 2) {
                acceleration += QuadraticAcceleration * Time.deltaTime;
            }
            velocity += acceleration * Time.deltaTime;

            
            velocity.x = Mathf.Clamp(velocity.x, 0, heroDef.MaxVelocity);
            return velocity;
        }
    }
}
