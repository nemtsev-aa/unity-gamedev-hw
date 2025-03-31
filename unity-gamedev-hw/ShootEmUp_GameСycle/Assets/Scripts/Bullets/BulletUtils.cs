namespace ShootEmUp {
    public class BulletUtils {
        public void DealDamage(Bullet bullet, Unit other) {
            if (bullet.IsPlayer == other.Team.IsPlayer)
                return;

            other.HitPointCounter.TakeDamage(bullet.Damage);
        }
    }
}