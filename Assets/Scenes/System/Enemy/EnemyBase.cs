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

    [Header("Drop Settings ")]
    public GameObject itemA;       
    public GameObject itemB;       
    public GameObject itemC;      

    [Range(0f, 1f)] public float itemAChance = 0.2f; 
    [Range(0f, 1f)] public float itemBChance = 0.1f; 
    [Range(0f, 1f)] public float itemCChance = 0.05f;

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

        animator.SetTrigger("Hit");

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

        TryDropItem();

        Destroy(gameObject, 1.5f);
    }

    /// <summary>
    /// 
    /// </summary>
    protected void TryDropItem()
    {
        float rand = Random.value;
        float totalA = itemAChance;
        float totalB = itemAChance + itemBChance;
        float totalC = itemAChance + itemBChance + itemCChance;

        // A
        if (rand < totalA)
        {
            if (itemA != null)
                Instantiate(itemA, transform.position, Quaternion.identity);

            return;
        }

        // B
        if (rand < totalB)
        {
            if (itemB != null)
                Instantiate(itemB, transform.position, Quaternion.identity);

            return;
        }

        // C
        if (rand < totalC)
        {
            if (itemC != null)
                Instantiate(itemC, transform.position, Quaternion.identity);

            return;
        }


    }

    protected void FlipSprite(float directionX)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = directionX < 0;
    }
}
