using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("Upgrade Pools")]
    public UpgradeOption[] startWeaponOptions;
    public UpgradeOption[] weaponUpgradeOptions;
    public UpgradeOption[] playerUpgradeOptions;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 开局：选择 1 把武器（3 选 1）
    /// </summary>
    public UpgradeOption[] GetStartWeaponOptions()
    {
        return startWeaponOptions
            .OrderBy(x => Random.value)
            .Take(3)
            .ToArray();
    }

    /// <summary>
    /// 每个 Wave 结束：随机升级
    /// </summary>
    public UpgradeOption[] GetRandomOptions()
    {
        List<UpgradeOption> pool = new List<UpgradeOption>();

        pool.AddRange(weaponUpgradeOptions);
        pool.AddRange(playerUpgradeOptions);

        return pool
            .OrderBy(x => Random.value)
            .Take(3)
            .ToArray();
    }
}
