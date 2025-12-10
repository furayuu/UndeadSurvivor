using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Start()
    {
        ShowInitialWeaponSelect();
    }

    public void ShowInitialWeaponSelect()
    {
        UpgradeOption[] options = new UpgradeOption[3];

        options[0] = new UpgradeOption
        {
            title = "Shovel",
            applyEffect = () => WeaponManager.Instance.EquipWeapon("Shovel")
        };

        options[1] = new UpgradeOption
        {
            title = "Spear",
            applyEffect = () => WeaponManager.Instance.EquipWeapon("Spear")
        };

        options[2] = new UpgradeOption
        {
            title = "Scythe",
            applyEffect = () => WeaponManager.Instance.EquipWeapon("Scythe")
        };

        UpgradeUI.Instance.ShowOptions(options, (selected) =>
        {
            selected.applyEffect();
        });
    }

    public void ShowSecondWeaponSelect()
    {
        UpgradeOption[] options = new UpgradeOption[3];

        options[0] = new UpgradeOption
        {
            title = "第二武器：Shovel",
            applyEffect = () => WeaponManager.Instance.EquipSecondWeapon("Shovel")
        };

        options[1] = new UpgradeOption
        {
            title = "第二武器：Spear",
            applyEffect = () => WeaponManager.Instance.EquipSecondWeapon("Spear")
        };

        options[2] = new UpgradeOption
        {
            title = "第二武器：Scythe",
            applyEffect = () => WeaponManager.Instance.EquipSecondWeapon("Scythe")
        };

        UpgradeUI.Instance.ShowOptions(options, (selected) =>
        {
            selected.applyEffect();
        });
    }


}
