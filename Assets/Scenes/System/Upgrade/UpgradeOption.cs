using UnityEngine;

public enum UpgradeType
{
    AttackUp,
    MaxHealthUp,
    MoveSpeedUp,
    AttackSpeedUp,
    Heal
}


[CreateAssetMenu(fileName = "UpgradeOption", menuName = "Upgrades/Upgrade Option")]
public class UpgradeOption : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    public UpgradeType upgradeType;
    public float value;

    public void Apply()
    {
        var player = Player.Instance;
        if (player == null)
        {
            Debug.LogWarning("Player not found");
            return;
        }

        switch (upgradeType)
        {
            case UpgradeType.AttackUp:
                WeaponManager.Instance.IncreaseAttack(value);
                break;

            case UpgradeType.MaxHealthUp:
                player.IncreaseMaxHealth((int)value);
                break;

            case UpgradeType.MoveSpeedUp:
                player.IncreaseSpeed(value);
                break;

            case UpgradeType.AttackSpeedUp:
                WeaponManager.Instance.IncreaseAttackSpeed(value);
                break;

            case UpgradeType.Heal:
                player.Heal((int)value);
                break;
        }

        Debug.Log("Apply Upgrade: " + upgradeName);
    }
}
