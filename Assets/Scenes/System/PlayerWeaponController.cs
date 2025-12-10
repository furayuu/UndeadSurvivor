using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerWeaponController : MonoBehaviour
{
    public static PlayerWeaponController Instance;

    private List<WeaponBase> weapons = new List<WeaponBase>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddWeapon(WeaponData data)
    {
        GameObject w = Instantiate(data.weaponModel, transform);
        WeaponBase weapon = w.GetComponent<WeaponBase>();
        weapon.Initialize(transform, data);

        weapons.Add(weapon);
    }
}
