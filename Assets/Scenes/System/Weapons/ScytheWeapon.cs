using UnityEngine;

public class ScytheWeapon : MeleeWeaponBase
{
    [Header("Scythe Settings")]
    [Tooltip("プレイヤーに対する鎌生成位置のオフセット半径")]
    public float spawnRadius = 0.5f;

    protected override void TryAttack()
    {
        SpawnScythe();
    }

    private void SpawnScythe()
    {
        if (weaponData.projectilePrefab == null)
        {
            Debug.LogWarning($"ScytheWeapon: {weaponData.weaponName} に projectilePrefab が設定されていません！");
            return;
        }

        // ランダムな周回位置（すべてプレイヤーの下に重ならないようにする）
        Vector2 offset = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPos = pivot.position + new Vector3(offset.x, offset.y, 0f);

        GameObject scythe = Instantiate(weaponData.projectilePrefab, spawnPos, Quaternion.identity);
        var rotating = scythe.GetComponent<RotatingScythe>();
        if (rotating != null)
        {
            rotating.Initialize(
                weaponData.damage,
                weaponData.rotationSpeed,
                weaponData.spinDuration,
                weaponData.damageInterval
            );
        }
    }
}
