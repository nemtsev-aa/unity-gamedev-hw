using ZombieShooter.SceneObjects;

namespace AtomicFramework.CollisionMechanics {

    public class UnitScanner {
        private IDetectionBehavior _detectionBehavior;

        public UnitScanner(IDetectionBehavior detectionBehavior) {
            _detectionBehavior = detectionBehavior;
        }

        public Unit FindClosest() {
            return _detectionBehavior.FindClosestUnit();
        }
    }
}