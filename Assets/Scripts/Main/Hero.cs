using TempleRun.Assets.Scripts.Main;
using TempleRun.Main.Acceleration;
using UnityEngine;

namespace TempleRun.Main {
    public class Hero : MonoBehaviour {
        private HeroPrefab def;
        private CharacterController controller;
        private AccelerationPolicy accelerationPolicy;
        private Animator animator;
        private Vector3 velocity;
        private World world;

        private bool isRunning;
        private bool isJumping;

        public void Initialize(HeroPrefab hero, World world) {
            def = hero;
            controller = hero.Controller;
            accelerationPolicy = hero.gameObject.GetComponent<AccelerationPolicy>();
            animator = hero.gameObject.GetComponent<Animator>();
            velocity = Vector3.zero;
            
            this.world = world;
        }

        public void Run() {
            isRunning = true;
            animator.SetBool("IsRunning", true);
        }

        public void Stop() {
            isRunning = false;
            velocity.x = 0;
            animator.SetBool("IsRunning", false);
        }

        public void Jump() {
            if (!controller.isGrounded) return;
            isJumping = true;
            animator.SetBool("IsRunning", false);
        }

        public void SkyJump() {
            if (controller.isGrounded) Jump();
            else velocity = def.JumpVelocity;
        }

        public void Die() {
            var detector = GetComponent<HeroCollisionDetector>();
            if (detector != null) {
                Destroy(detector);
            }

            SkyJump();
            Stop();
            gameObject.layer = LayerMask.NameToLayer("Ghost");
        }

        public float HorizontalSpeed {
            get {
                return velocity.x;
            }
        }

        public void Update() {
            if (controller.isGrounded) {
                velocity.y = 0;
                if (isJumping) {
                    velocity += def.JumpVelocity;
                    isJumping = false;
                } else {
                    animator.SetBool("IsRunning", isRunning);
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
