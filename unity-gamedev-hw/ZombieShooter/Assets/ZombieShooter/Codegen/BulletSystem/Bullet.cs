using ZombieShooter.SceneObjects;

namespace AtomicFramework.BulletSystem {

    public class Bullet : SceneObject {
        public bool IsInstall { get; private set; } = false;

        public void SetInstall(bool status) {
            IsInstall = status;
        }
    }
}
