using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Base Weapon Stats")]
    [SerializeField] protected WeaponData weaponData;

    protected Transform owner;
    protected float nextAttackTime;

    protected float damageMultiplier = 1f;
    protected float attackSpeedMultiplier = 1f;
    protected virtual void Start() { }
    public void AddDamageMultiplier(float value)
    {
        damageMultiplier += value;
    }

    public void AddAttackSpeedMultiplier(float value)
    {
        attackSpeedMultiplier += value;
    }

    public virtual void Initialize(Transform ownerTransform, WeaponData data = null)
    {
        owner = ownerTransform;
        if (data != null) weaponData = data;
    }

    protected virtual void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            TryAttack();
            nextAttackTime =
                Time.time + 1f / (weaponData.attackRate * attackSpeedMultiplier);
        }
    }


    protected float GetFinalDamage()
    {
        return weaponData.damage * damageMultiplier;
    }

    protected abstract void TryAttack();
}
