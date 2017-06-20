using UnityEngine;

namespace TempleRun.Main.Acceleration {
    public class QuadraticAccelerationPolicy : AccelerationPolicy {
        public Vector3 LinearAcceleration;
        public Vector3 QuadraticAcceleration;

        private Vector3 acceleration;

        protected void Awake() {
            acceleration = LinearAcceleration;
        }

        public override Vector3 UpdateVelocity(Vector3 velocity, HeroPrefab heroDef) {
            if (velocity.x >= heroDef.MaxVelocity / 2) {
                acceleration += QuadraticAcceleration * Time.deltaTime;
            }
            velocity += acceleration * Time.deltaTime;

            
            velocity.x = Mathf.Clamp(velocity.x, 0, heroDef.MaxVelocity);
            return velocity;
        }
    }
}
