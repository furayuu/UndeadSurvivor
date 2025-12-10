using UnityEngine;

public enum UpgradeType
{
    WeaponSelect,   // 新武器选择
    WeaponUpgrade,  // 武器强化（等级+1）
    PlayerUpgrade   // 玩家BUFF，如+HP、+移动速度等
}

[CreateAssetMenu(fileName = "UpgradeOption", menuName = "Upgrades/Upgrade Option")]
public class UpgradeOption : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;
    public string title;                 // 显示名称（按钮文字）
    public System.Action applyEffect;    // 点击后执行的效果
    public UpgradeType upgradeType;

    // 对应的武器（仅在 WeaponSelect / WeaponUpgrade 下使用）
    public WeaponData weaponData;

    // 数值强化（仅在 PlayerUpgrade 使用）
    public float value;
}
