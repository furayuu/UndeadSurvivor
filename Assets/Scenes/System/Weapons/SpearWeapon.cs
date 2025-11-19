using System.Collections.Generic;
using UnityEngine;

public class SpearWeapon : MeleeWeaponBase
{
    private Vector3 targetPos;
    private Transform currentTarget;
    private HashSet<EnemyBase> hitEnemies = new HashSet<EnemyBase>();

    protected override void Start()
    {
        base.Start();
        // 基本クラスで初期化と向き更新を処理
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
        Vector3 attackStartPos = transform.localPosition;

        // 前方に伸ばす
        while (Vector3.Distance(transform.localPosition, targetPos) > 0.05f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, targetPos, weaponData.extendSpeed * Time.deltaTime);

            DamageCheck();
            yield return null;
        }

        // 元の位置に戻る
        while (Vector3.Distance(transform.localPosition, attackStartPos) > 0.05f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, attackStartPos, weaponData.extendSpeed * Time.deltaTime);
            yield return null;
        }

        // 戻った後に正しい向きを復元
        UpdateWeaponFacing();
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

    // 武器の向き更新メソッドをオーバーライドして槍用の特別な処理を追加
    protected override void UpdateWeaponFacing()
    {
        if (pivot == null || isAttacking) return;

        // プレイヤーのスプライトの向きを取得
        SpriteRenderer playerSprite = Player.Instance.SpriteRenderer;
        if (playerSprite != null)
        {
            bool newFacingRight = !playerSprite.flipX;

            // 向きが変更された場合
            if (newFacingRight != facingRight)
            {
                facingRight = newFacingRight;

                // 槍の場合は位置と回転の両方を調整
                if (facingRight)
                {
                    // 右向き - 通常の位置と0度回転
                    transform.localPosition = new Vector3(
                        Mathf.Abs(startLocalPos.x),
                        startLocalPos.y,
                        startLocalPos.z
                    );
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }
                else
                {
                    // 左向き - 水平反転と180度回転
                    transform.localPosition = new Vector3(
                        -Mathf.Abs(startLocalPos.x),
                        startLocalPos.y,
                        startLocalPos.z
                    );
                    transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
                }
            }
        }
    }
}