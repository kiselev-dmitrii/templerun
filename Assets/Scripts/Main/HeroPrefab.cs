using System;
using TempleRun.Main.Acceleration;
using UnityEngine;

namespace TempleRun.Main {
    [RequireComponent(typeof(AccelerationPolicy))]
    public class HeroPrefab : MonoBehaviour {
        public String Name;
        public float MaxVelocity;
        public Vector3 JumpVelocity;
    }
}
