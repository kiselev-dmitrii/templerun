using UnityEngine;

namespace TempleRun.Game {
    public class Hero : MonoBehaviour {
        private HeroDef def;
        private CharacterController controller;
        private Vector3 velocity;
        private WorldDef worldDef;

        private bool isRunning;
        private bool isJumping;

        public void Initialize(HeroDef hero, WorldDef world) {
            def = hero;
            controller = hero.Controller;
            velocity = Vector3.zero;
            worldDef = world;
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
                }
            }

            if (isRunning) {
                velocity = def.AccelerationPolicy.UpdateVelocity(velocity, def);
            }
            velocity += worldDef.Gravity * Time.deltaTime;            
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
