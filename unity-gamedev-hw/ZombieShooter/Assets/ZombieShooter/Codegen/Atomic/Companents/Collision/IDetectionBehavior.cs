using ZombieShooter.SceneObjects;

namespace AtomicFramework.CollisionMechanics {

    public interface IDetectionBehavior {
        Unit FindClosestUnit();
    }
}