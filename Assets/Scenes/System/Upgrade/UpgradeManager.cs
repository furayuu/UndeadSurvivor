using UnityEngine;
using System.Collections.Generic;

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

    public List<UpgradeOption> GetRandomUpgrades(int count = 3)
    {
        List<UpgradeOption> pool = new List<UpgradeOption>(allUpgrades);
        List<UpgradeOption> result = new List<UpgradeOption>();

        count = Mathf.Min(count, pool.Count);

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index); 
        }

        return result;
    }
}
