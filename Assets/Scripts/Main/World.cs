using TempleRun.Utils;
using UnityEngine;

namespace TempleRun.Main {
    public class World : MonoBehaviour {
        public Transform SpawnPoint;
        public Vector3 Gravity = new Vector3(0, -9.8f, 0);
        public CameraFollow Camera;

        public Hero SpawnHero(HeroPrefab template) {
            var heroPrefab = Instantiate(template);
            var hero = heroPrefab.gameObject.AddComponent<Hero>();
            hero.transform.parent = transform;
            hero.transform.position = SpawnPoint.transform.position;
            hero.transform.rotation = SpawnPoint.transform.rotation;
            hero.Initialize(heroPrefab, this);
            return hero;
        }
    }
}
