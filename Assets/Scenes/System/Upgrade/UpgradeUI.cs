using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI Instance;

    public GameObject panel;
    public Button[] optionButtons; // 三个按钮
    private System.Action<UpgradeOption> onSelectedCallback;

    private UpgradeOption[] currentOptions;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowOptions(UpgradeOption[] options, System.Action<UpgradeOption> onSelected)
    {
        GamePause.Pause();

        currentOptions = options;
        onSelectedCallback = onSelected;

        panel.SetActive(true);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].GetComponentInChildren<Text>().text = options[i].title;

            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() =>
            {
                SelectOption(index);
            });
        }
    }

    void SelectOption(int index)
    {
        onSelectedCallback?.Invoke(currentOptions[index]);
        panel.SetActive(false);
        GamePause.Resume();
    }
}

