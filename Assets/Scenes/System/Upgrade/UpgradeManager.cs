using UnityEngine;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public UpgradeOption[] allUpgrades;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public UpgradeOption[] GetRandomUpgrades(int count = 3)
    {
        return allUpgrades
            .OrderBy(x => Random.value)
            .Take(count)
            .ToArray();
    }
}
