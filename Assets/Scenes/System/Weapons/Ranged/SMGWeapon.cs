using UnityEngine;

public class SMGWeapon : RangedWeaponBase
{
    [Header("SMG Settings")]
    public float bulletSpeed = 15f;
    public float searchRange = 5f;

    protected override void TryAttack()
    {
        Transform target = FindNearestEnemy();
        if (target == null) return;

        Vector3 dir = (target.position - firePoint.position).normalized;
        FireBullet(dir, bulletSpeed, penetrate: false);
    }

    private Transform FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(firePoint.position, searchRange);
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;
            float dist = Vector2.Distance(firePoint.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }
        return nearest;
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(firePoint.position, searchRange);
    }
}
