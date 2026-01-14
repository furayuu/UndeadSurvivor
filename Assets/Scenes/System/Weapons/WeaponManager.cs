using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    [Header("武器 Prefab")]
    public GameObject shovelPrefab;
    public GameObject spearPrefab;
    public GameObject scythePrefab;

    [Header("武器挂点（通常是角色身上的一个空物体）")]
    public Transform weaponHolder;

    private GameObject currentWeapon;
    private GameObject secondWeapon;

    private void Awake()
    {
        Instance = this;
    }

    // ============= 第一武器 =============
    public void EquipWeapon(string weaponName)
    {
        // 清除旧武器
        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(GetWeaponPrefab(weaponName), weaponHolder);
        Debug.Log("装备主武器: " + weaponName);

        WaveManager.Instance.StartFirstWave();
    }

    // ============= 第二武器 =============
    public void EquipSecondWeapon(string weaponName)
    {
        if (secondWeapon != null)
        {
            Destroy(secondWeapon);
        }

        secondWeapon = Instantiate(GetWeaponPrefab(weaponName), weaponHolder);
        Debug.Log("装备第二武器: " + weaponName);
    }

    private GameObject GetWeaponPrefab(string weaponName)
    {
        switch (weaponName)
        {
            case "Shovel": return shovelPrefab;
            case "Spear": return spearPrefab;
            case "Scythe": return scythePrefab;
        }

        Debug.LogError("没有这个武器名：" + weaponName);
        return null;
    }
}
