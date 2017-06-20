using UnityEngine;

namespace TempleRun.Game {
    public class GameController : MonoBehaviour {
        private HeroDef selectedHero;
        private WorldDef selectedWorld;

        public void SetHero(HeroDef heroDef) {
            selectedHero = heroDef;
        }

        public void SetWorld(WorldDef worldDef) {
            selectedWorld = worldDef;
        }

        public void Run() {
            
        }
    }
}
