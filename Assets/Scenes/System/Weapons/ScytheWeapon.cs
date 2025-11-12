using UnityEngine;

public class ScytheWeapon : MeleeWeaponBase
{
    [Header("Scythe Settings")]
    [Tooltip("镰刀生成位置相对玩家的偏移半径")]
    public float spawnRadius = 0.5f;

    protected override void TryAttack()
    {
        SpawnScythe();
    }

    private void SpawnScythe()
    {
        if (weaponData.projectilePrefab == null)
        {
            Debug.LogWarning($"ScytheWeapon: {weaponData.weaponName} 缺少 projectilePrefab！");
            return;
        }

        // 随机一个环绕位置（防止全部重叠在玩家脚下）
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
