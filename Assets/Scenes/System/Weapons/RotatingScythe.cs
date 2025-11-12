using System.Collections.Generic;
using UnityEngine;


public class RotatingScythe : MonoBehaviour
{
    private float damage;
    private float rotationSpeed;
    private float lifetime;
    private float damageInterval;

    private float nextDamageTime;
    private readonly HashSet<EnemyBase> hitEnemies = new HashSet<EnemyBase>();

    public void Initialize(float dmg, float rotSpeed, float life, float dmgInterval)
    {
        damage = dmg;
        rotationSpeed = rotSpeed;
        lifetime = life;
        damageInterval = dmgInterval;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);

        if (Time.time >= nextDamageTime)
        {
            nextDamageTime = Time.time + damageInterval;
            hitEnemies.Clear();
        }

        DamageCheck();
    }

    private void DamageCheck()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null && !hitEnemies.Contains(enemy))
            {
                enemy.TakeDamage(damage);
                hitEnemies.Add(enemy);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
