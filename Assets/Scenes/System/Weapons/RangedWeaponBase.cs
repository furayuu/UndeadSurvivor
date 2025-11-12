using UnityEngine;

public abstract class RangedWeaponBase : WeaponBase
{
    [Header("Projectile Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    protected override void Start()
    {
        base.Start();
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    protected void FireBullet(Vector3 direction, float speed, bool penetrate = false)
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        var bullet = bulletObj.AddComponent<BulletBehaviour>();
        bullet.Initialize(weaponData.damage, direction, speed, penetrate);
    }
}
