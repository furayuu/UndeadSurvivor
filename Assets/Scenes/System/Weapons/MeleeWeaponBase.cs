using UnityEngine;

public abstract class MeleeWeaponBase : WeaponBase
{
    protected bool isAttacking;
    protected Transform pivot;

    protected override void Start()
    {
        base.Start(); 
        pivot = owner != null ? owner : transform.parent;
    }

    protected override void Update()
    {
        if (Time.time >= nextAttackTime && !isAttacking)
        {
            TryAttack();
            nextAttackTime = Time.time + 1f / weaponData.attackRate;
        }
    }
}
