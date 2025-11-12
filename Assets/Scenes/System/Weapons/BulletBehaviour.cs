using UnityEngine;
using System.Collections.Generic;

public class BulletBehaviour : MonoBehaviour
{
    private float damage;
    private float speed;
    private Vector3 direction;
    private bool penetrate;
    private HashSet<EnemyBase> hitEnemies = new HashSet<EnemyBase>();

    public void Initialize(float dmg, Vector3 dir, float spd, bool canPenetrate)
    {
        damage = dmg;
        direction = dir.normalized;
        speed = spd;
        penetrate = canPenetrate;
        Destroy(gameObject, 5f); 
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (enemy == null || hitEnemies.Contains(enemy)) return;

        enemy.TakeDamage(damage);
        hitEnemies.Add(enemy);

        if (!penetrate)
        {
            Destroy(gameObject);
        }
    }
}
