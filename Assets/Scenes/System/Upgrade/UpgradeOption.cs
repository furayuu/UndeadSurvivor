using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeOption", menuName = "Upgrades/Upgrade Option")]
public class UpgradeOption : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    public float value; // 数值用（加攻击、加血等）

    public void Apply()
    {
        // 示例：你可以之后在这里 switch 或分发
        Debug.Log("Apply Upgrade: " + upgradeName);
    }
}
