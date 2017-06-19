using UnityEngine;

namespace TempleRun.Game {
    public class GameController : MonoBehaviour {
        public CharacterController Controller;
        public Vector3 JumpVelocity = new Vector3(0, 8.0f, 0);
        public Vector3 Gravity = new Vector3(0, -9.8f);
        public Vector3 Acceleration = new Vector3(0.1f, 0, 0);
        public Vector3 Velocity;
        public float MaxVelocity;

        private float verticalSpeed;

        public void Start() {
            
        }

        public void Update() {
            if (Controller.isGrounded) {
                Velocity.y = 0;
                if (Input.GetKeyDown(KeyCode.Space)) {
                    Velocity += JumpVelocity;
                }
            }
            Velocity += (Acceleration+Gravity) * Time.deltaTime;
            Velocity = Vector3.ClampMagnitude(Velocity, MaxVelocity);
            Controller.Move(Velocity * Time.deltaTime);
        }
    }
}
