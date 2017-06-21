using TempleRun.Assets.Scripts.Main;
using TempleRun.Main.Acceleration;
using UnityEngine;

namespace TempleRun.Main {
    [RequireComponent(typeof(HeroPrefab), typeof(AccelerationPolicy), typeof(CharacterController))]
    public class Hero : MonoBehaviour {
        public HeroPrefab Def { get; private set; }
        private CharacterController controller;
        private AccelerationPolicy accelerationPolicy;
        private HeroCollisionDetector collisionDetector;
        private Animator animator;
        private Vector3 velocity;
        private World world;

        private bool isRunning;
        private bool isJumping;

        public void Initialize(World world) {
            Def = GetComponent<HeroPrefab>();
            controller = GetComponent<CharacterController>();
            accelerationPolicy = gameObject.GetComponent<AccelerationPolicy>();
            collisionDetector = gameObject.AddComponent<HeroCollisionDetector>();
            animator = gameObject.GetComponent<Animator>();
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
            else velocity = Def.JumpVelocity;
        }

        public void Die() {
            Destroy(collisionDetector);
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
                    velocity += Def.JumpVelocity;
                    isJumping = false;
                } else {
                    animator.SetBool("IsRunning", isRunning);
                }
            }

            if (isRunning) {
                velocity = accelerationPolicy.UpdateVelocity(velocity, Def);
            }
            velocity += world.Gravity * Time.deltaTime;
            controller.enabled = true;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
