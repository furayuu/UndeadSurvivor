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

    [Header("Drop Settings (個別確率)")]
    public GameObject itemA;       // ドロップアイテムA
    public GameObject itemB;       // ドロップアイテムB
    public GameObject itemC;       // ドロップアイテムC

    [Range(0f, 1f)] public float itemAChance = 0.2f; // A の確率
    [Range(0f, 1f)] public float itemBChance = 0.1f; // B の確率
    [Range(0f, 1f)] public float itemCChance = 0.05f;// C の確率
    // 残りは NoDrop

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

        // ドロップ処理
        TryDropItem();

        Destroy(gameObject, 1.5f);
    }

    /// <summary>
    /// 個別確率ドロップ処理
    /// </summary>
    protected void TryDropItem()
    {
        float rand = Random.value;
        float totalA = itemAChance;
        float totalB = itemAChance + itemBChance;
        float totalC = itemAChance + itemBChance + itemCChance;

        Debug.Log($"Drop判定 rand={rand}, A={itemAChance}, B={itemBChance}, C={itemCChance}");

        // A
        if (rand < totalA)
        {
            if (itemA != null)
                Instantiate(itemA, transform.position, Quaternion.identity);

            Debug.Log("Item A ドロップ");
            return;
        }

        // B
        if (rand < totalB)
        {
            if (itemB != null)
                Instantiate(itemB, transform.position, Quaternion.identity);

            Debug.Log("Item B ドロップ");
            return;
        }

        // C
        if (rand < totalC)
        {
            if (itemC != null)
                Instantiate(itemC, transform.position, Quaternion.identity);

            Debug.Log("Item C ドロップ");
            return;
        }

        // どれにも当たらない → NoDrop
        Debug.Log("ドロップなし");
    }

    protected void FlipSprite(float directionX)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = directionX < 0;
    }
}
