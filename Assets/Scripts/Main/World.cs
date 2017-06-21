using TempleRun.Utils;
using UnityEngine;

namespace TempleRun.Main {
    public class World : MonoBehaviour {
        public Transform SpawnPoint;
        public Vector3 Gravity = new Vector3(0, -9.8f, 0);
        public CameraFollow Camera;
    }
}
