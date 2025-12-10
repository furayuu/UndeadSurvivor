using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public Image icon;
    public TMP_Text title;
    public TMP_Text description;

    private UpgradeOption currentOption;
    private System.Action<UpgradeOption> onClick;

    public void SetOption(UpgradeOption option, System.Action<UpgradeOption> callback)
    {
        currentOption = option;
        onClick = callback;

        icon.sprite = option.icon;
        title.text = option.upgradeName;
        description.text = option.description;
    }

    public void Click()
    {
        onClick?.Invoke(currentOption);
    }
}
