using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyContactDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageInterval = 1f;

    private float nextDamageTime;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time < nextDamageTime) return;

        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null && player.hpBar != null)
            {
                player.hpBar.TakeDamage(damage);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
