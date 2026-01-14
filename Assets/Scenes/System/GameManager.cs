using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startWeaponPanel;

    void Start()
    {
        GamePause.Pause();
        startWeaponPanel.SetActive(true);
    }

    public void SelectShovel()
    {
        WeaponManager.Instance.EquipWeapon("Shovel");
        StartGame();
    }

    public void SelectSpear()
    {
        WeaponManager.Instance.EquipWeapon("Spear");
        StartGame();
    }

    public void SelectScythe()
    {
        WeaponManager.Instance.EquipWeapon("Scythe");
        StartGame();
    }

    void StartGame()
    {
        startWeaponPanel.SetActive(false);
        GamePause.Resume();
        WaveManager.Instance.StartFirstWave();
    }
}
