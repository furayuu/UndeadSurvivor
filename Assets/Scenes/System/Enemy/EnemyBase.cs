using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class EnemyBase : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 50f;
    protected float currentHealth;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected bool isDead = false;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        isDead = true;
        animator.SetTrigger("Dead");

        GetComponent<Collider2D>().enabled = false;
        foreach (var comp in GetComponents<MonoBehaviour>())
        {
            if (comp != this) comp.enabled = false;
        }

        Destroy(gameObject, 1.5f);
    }

    protected void FlipSprite(float directionX)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = directionX < 0;
    }
}
