using UnityEngine;

public class EnemyFollow : EnemyBase
{
    [Header("Follow Settings")]
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;

    private float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    private void Update()
    {
        if (isDead || player == null) return;

        Vector2 dir = player.position - transform.position;
        float distance = dir.magnitude;

        if (distance > attackRange)
        {
            dir.Normalize();
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);

            animator.SetFloat("Speed", moveSpeed);
            FlipSprite(dir.x);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    private void Attack()
    {
        Debug.Log(name + " attacks player!");
    }
}
