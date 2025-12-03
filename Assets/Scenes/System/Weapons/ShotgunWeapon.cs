using UnityEngine;

public class ShotgunWeapon : RangedWeaponBase
{
    [Header("Shotgun Settings")]
    public int pelletCount = 6;
    public float spreadAngle = 30f;
    public float bulletSpeed = 12f;

    protected override void TryAttack()
    {
        FirePelletSpread();
    }

    private void FirePelletSpread()
    {
        float angleStep = spreadAngle / (pelletCount - 1);
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < pelletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 dir = Quaternion.Euler(0, 0, angle) * firePoint.right;
            FireBullet(dir, bulletSpeed, penetrate: false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;
        Gizmos.color = Color.yellow;
        Vector3 startDir = Quaternion.Euler(0, 0, -spreadAngle / 2f) * firePoint.right;
        Vector3 endDir = Quaternion.Euler(0, 0, spreadAngle / 2f) * firePoint.right;
        Gizmos.DrawRay(firePoint.position, startDir * weaponData.range);
        Gizmos.DrawRay(firePoint.position, endDir * weaponData.range);
    }
}
