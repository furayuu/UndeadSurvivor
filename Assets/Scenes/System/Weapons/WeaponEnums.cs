// WeaponEnums.cs
using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged
}

public enum AttackMode
{
    // 近战
    Stab,   // 刺击（草叉）
    Swing,  // 扇形（铁锹）
    Spin,   // 旋转（镰刀）

    // 远程
    SingleShot, // 单发（狙击）
    Burst,      // 连发（可扩展）
    Spread      // 霰弹（散射）
}
