// WeaponEnums.cs
using UnityEngine;

public enum WeaponType
{
    Melee,  // 近接武器
    Ranged  // 遠距離武器
}

public enum AttackMode
{
    // 近接武器
    Stab,   // 突き（草叉）
    Swing,  // 扇形振り（シャベル）
    Spin,   // 回転（鎌）

    // 遠距離武器
    SingleShot, // 単発（スナイパー）
    Burst,      // 連射（拡張可能）
    Spread      // 拡散（散弾）
}