using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("General Info")]
    public string weaponName = "Unnamed Weapon";
    public WeaponType weaponType = WeaponType.Melee;
    public AttackMode attackMode = AttackMode.Stab;
    [TextArea] public string description;

    [Header("Base Stats")]
    public float damage = 10f;
    public float attackRate = 1f;
    public float range = 3f;

    [Header("Projectile Stats (Ranged Only)")]
    public float bulletSpeed = 15f;
    public int bulletCount = 1;
    public float spreadAngle = 0f;
    public bool canPenetrate = false;

    [Header("Melee Stats (Melee Only)")]
    public float extendDistance = 3f;
    public float extendSpeed = 10f;
    public float swingAngle = 90f;
    public float swingDuration = 0.25f;
    public float spinDuration = 3f;
    public float rotationSpeed = 360f;
    public float damageInterval = 0.5f;

    [Header("Prefabs & Visuals")]
    public GameObject projectilePrefab;
    public GameObject weaponModel;
    public AudioClip attackSfx;
    public ParticleSystem muzzleFlash;
}
