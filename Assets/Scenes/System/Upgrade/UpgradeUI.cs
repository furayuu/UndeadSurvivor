using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI Instance;

    public GameObject panel;
    public UpgradeButton[] buttons;

    private UpgradeOption[] currentOptions;
    private System.Action<UpgradeOption> onSelected;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowOptions(UpgradeOption[] options, System.Action<UpgradeOption> callback)
    {
        GamePause.Pause();

        currentOptions = options;
        onSelected = callback;

        panel.SetActive(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].SetOption(options[i], option =>
            {
                Select(option);
            });
        }
    }

    void Select(UpgradeOption option)
    {
        onSelected?.Invoke(option);
        panel.SetActive(false);
        GamePause.Resume();
    }
}
