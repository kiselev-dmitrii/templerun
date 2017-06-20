using TempleRun.Main.Acceleration;
using UnityEngine;

namespace TempleRun.Main {
    public class Hero : MonoBehaviour {
        private HeroPrefab def;
        private CharacterController controller;
        private AccelerationPolicy accelerationPolicy;
        private Vector3 velocity;
        private World world;

        private bool isRunning;
        private bool isJumping;

        public void Initialize(HeroPrefab hero, World world) {
            def = hero;
            controller = hero.Controller;
            accelerationPolicy = hero.gameObject.GetComponent<AccelerationPolicy>();
            velocity = Vector3.zero;
            
            this.world = world;
        }

        public void Run() {
            isRunning = true;
        }

        public void Stop() {
            isRunning = false;
        }

        public void Jump() {
            isJumping = true;
        }

        public void Update() {
            if (controller.isGrounded) {
                velocity.y = 0;
                if (isJumping) {
                    velocity += def.JumpVelocity;
                    isJumping = false;
                }
            }

            if (isRunning) {
                velocity = accelerationPolicy.UpdateVelocity(velocity, def);
            }
            velocity += world.Gravity * Time.deltaTime;
            controller.enabled = true;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
