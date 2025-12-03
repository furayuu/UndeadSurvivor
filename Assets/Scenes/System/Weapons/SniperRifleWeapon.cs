using UnityEngine;

public class SniperRifleWeapon : RangedWeaponBase
{
    [Header("Sniper Settings")]
    public float bulletSpeed = 20f;
    public float searchRange = 10f;

    protected override void TryAttack()
    {
        Transform target = FindFarthestEnemy();
        if (target == null) return;

        Vector3 dir = (target.position - firePoint.position).normalized;
        FireBullet(dir, bulletSpeed, penetrate: true);
    }

    private Transform FindFarthestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(firePoint.position, searchRange);
        Transform farthest = null;
        float maxDist = 0f;

        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;
            float dist = Vector2.Distance(firePoint.position, hit.transform.position);
            if (dist > maxDist)
            {
                maxDist = dist;
                farthest = hit.transform;
            }
        }
        return farthest;
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(firePoint.position, searchRange);
    }
}
