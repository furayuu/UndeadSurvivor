using System.Collections.Generic;
using UnityEngine;

public class SpearWeapon : MeleeWeaponBase
{
    private Vector3 startLocalPos;
    private Vector3 targetPos;
    private Transform currentTarget;
    private HashSet<EnemyBase> hitEnemies = new HashSet<EnemyBase>();

    protected override void Start()
    {
        base.Start();

        if (pivot == null)
            pivot = owner != null ? owner : transform.parent;

        transform.parent = pivot;
        startLocalPos = transform.localPosition;
    }

    protected override void TryAttack()
    {
        if (isAttacking) return;

        currentTarget = FindNearestEnemy();
        if (currentTarget != null)
        {
            StartAttack();
        }
    }

    private Transform FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(pivot.position, weaponData.range);
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;
            float dist = Vector2.Distance(pivot.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }
        return nearest;
    }

    private void StartAttack()
    {
        if (currentTarget == null) return;

        isAttacking = true;
        hitEnemies.Clear();

        Vector3 dir = (currentTarget.position - pivot.position).normalized;
        targetPos = startLocalPos + dir * weaponData.extendDistance;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);

        StartCoroutine(ExtendRoutine());
    }

    private System.Collections.IEnumerator ExtendRoutine()
    {
        while (Vector3.Distance(transform.localPosition, targetPos) > 0.05f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, targetPos, weaponData.extendSpeed * Time.deltaTime);

            DamageCheck();
            yield return null;
        }

        while (Vector3.Distance(transform.localPosition, startLocalPos) > 0.05f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, startLocalPos, weaponData.extendSpeed * Time.deltaTime);
            yield return null;
        }

        transform.localRotation = Quaternion.identity;
        isAttacking = false;
    }

    private void DamageCheck()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null && !hitEnemies.Contains(enemy))
            {
                enemy.TakeDamage(weaponData.damage);
                hitEnemies.Add(enemy);
            }
        }
    }
}
