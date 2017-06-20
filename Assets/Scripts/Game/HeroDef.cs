using System;
using TempleRun.Game.Acceleration;
using UnityEngine;

namespace TempleRun.Game {
    public class HeroDef : MonoBehaviour {
        public String Name;
        public float MaxVelocity;
        public Vector3 JumpVelocity;
        public AccelerationPolicy AccelerationPolicy;
        public CharacterController Controller;
    }
}
