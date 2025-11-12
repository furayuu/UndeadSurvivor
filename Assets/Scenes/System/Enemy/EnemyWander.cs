using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWander : EnemyBase
{
    [Header("Wander Settings")]
    public float moveSpeed = 2f;
    public float wanderRadius = 5f;
    public float idleTime = 1.5f;

    private Rigidbody2D rb;
    private Vector2 origin;
    private bool isWandering = true;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        origin = transform.position;
        StartCoroutine(WanderRoutine());
    }

    private IEnumerator WanderRoutine()
    {
        while (!isDead && isWandering)
        {
            Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
            Vector2 target = origin + randomOffset;

            while (Vector2.Distance(transform.position, target) > 0.1f && !isDead)
            {
                Vector2 dir = (target - (Vector2)transform.position).normalized;
                rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);

                animator.SetFloat("Speed", moveSpeed);
                FlipSprite(dir.x);

                yield return new WaitForFixedUpdate();
            }

            animator.SetFloat("Speed", 0f);
            yield return new WaitForSeconds(idleTime);
        }
    }

    protected override void Die()
    {
        isWandering = false;
        rb.velocity = Vector2.zero;
        base.Die();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
