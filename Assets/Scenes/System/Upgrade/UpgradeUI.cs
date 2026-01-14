using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI Instance;

    public GameObject panel;
    public UpgradeButton[] buttons;

    private void Awake()
    {
        Instance = this;

        if (panel != null)
            panel.SetActive(false);
    }

    public void Show()
    {
        GamePause.Pause();
        panel.SetActive(true);

        var options = UpgradeManager.Instance.GetRandomUpgrades();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < options.Count)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].Set(options[i], this);
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
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
