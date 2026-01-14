using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public Image icon;
    public TMP_Text title;
    public TMP_Text description;

    private UpgradeOption option;
    private UpgradeUI owner;

    public void Set(UpgradeOption option, UpgradeUI owner)
    {
        this.option = option;
        this.owner = owner;

        icon.sprite = option.icon;
        title.text = option.upgradeName;
        description.text = option.description;
    }

    public void Click()
    {
        if (option == null || owner == null)
        {
            Debug.LogError("UpgradeButton Clicked but not initialized!");
            return;
        }

        owner.Select(option);
    }

}
