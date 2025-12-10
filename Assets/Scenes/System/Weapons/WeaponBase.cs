using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Base Weapon Stats")]
    [SerializeField] protected WeaponData weaponData;

    protected Transform owner;
    protected float nextAttackTime;

    protected virtual void Start() { } 

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
            nextAttackTime = Time.time + 1f / weaponData.attackRate;
        }
    }

    protected abstract void TryAttack();
}
