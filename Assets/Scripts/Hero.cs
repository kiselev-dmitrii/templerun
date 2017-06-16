using UnityEngine;

namespace TempleRun.Assets.Scripts {
    public interface IHero {
        void Run();
        void Stop();
        void Jump();
    }

    public enum HeroState {
        Stopped,
        Running
    }

    public class Hero : MonoBehaviour, IHero {
        public Vector3 Acceleration;
        public Vector3 JumpForce;
        public Rigidbody Rigidbody;

        private HeroState state;
        private bool isJumping;
        private Transform t;
        private Vector3 velocity;

        protected void Awake() {
            t = transform;
            Stop();
        }

        public void Run() {
            state = HeroState.Running;
        }

        public void Stop() {
            state = HeroState.Stopped;
            velocity = Vector3.zero;
        }

        public void Jump() {
            if (isJumping) return;
            isJumping = true;
            Rigidbody.AddForce(JumpForce);
        }

        protected void FixedUpdate() {
            if (state == HeroState.Running && !isJumping) {
                Rigidbody.velocity += Acceleration;
            }
        }

        private void UpdatePosition() {
            t.position += velocity*Time.fixedDeltaTime;
        }

        protected void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.tag == "Road") {
                isJumping = false;
            }   
        }
    } 
}
