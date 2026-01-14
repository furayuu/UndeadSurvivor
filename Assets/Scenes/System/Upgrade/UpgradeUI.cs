using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI Instance;

    public GameObject panel;
    public UpgradeButton[] buttons;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show()
    {
        GamePause.Pause();
        panel.SetActive(true);

        var options = UpgradeManager.Instance.GetRandomUpgrades();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Set(options[i], this);
        }
    }

    public void Select(UpgradeOption option)
    {
        option.Apply();

        panel.SetActive(false);
        GamePause.Resume();

        WaveManager.Instance.StartNextWave();
    }
}
