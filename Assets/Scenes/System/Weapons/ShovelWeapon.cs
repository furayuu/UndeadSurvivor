using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelWeapon : MeleeWeaponBase
{
    private HashSet<EnemyBase> hitEnemies = new HashSet<EnemyBase>();

    protected override void Start()
    {
        base.Start();
        // 基本クラスで初期化と向き更新を処理
    }

    protected override void TryAttack()
    {
        if (isAttacking) return;
        StartCoroutine(DoArcSwing());
    }

    private IEnumerator DoArcSwing()
    {
        isAttacking = true;
        hitEnemies.Clear();

        float radius = Mathf.Max(0.01f, weaponData.extendDistance);
        float extendSpeed = Mathf.Max(0.01f, weaponData.extendSpeed);
        float swingDuration = Mathf.Max(0.01f, weaponData.swingDuration);
        float damage = weaponData.damage;

        // 基本クラスの facingRight を使用して方向を決定
        float startAngle = facingRight ? 45f : 135f;
        float endAngle = facingRight ? -45f : 225f;

        Vector3 startPos = transform.localPosition; // 現在のローカル位置を使用
        Vector3 targetPos = AngleToLocalPosition(startAngle, radius);

        // 開始位置へ素早く移動
        float t = 0f;
        while (t < 0.15f)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, t / 0.15f);
            transform.localRotation = Quaternion.Euler(0f, 0f, startAngle);
            yield return null;
        }

        // 振り動作を実行
        float elapsed = 0f;
        while (elapsed < swingDuration)
        {
            elapsed += Time.deltaTime;
            float p = Mathf.Clamp01(elapsed / swingDuration);
            float angle = Mathf.Lerp(startAngle, endAngle, p);

            transform.localPosition = AngleToLocalPosition(angle, radius);
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);

            ArcDamageCheck(angle, radius, damage);
            yield return null;
        }

        // 初期位置に戻る
        while (Vector3.Distance(transform.localPosition, startPos) > 0.05f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPos, extendSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation,
                facingRight ? startLocalRot : Quaternion.Euler(0f, 0f, 180f), // 左向きの場合は反転状態を保持
                extendSpeed * Time.deltaTime);
            yield return null;
        }

        transform.localPosition = startPos;
        transform.localRotation = facingRight ? startLocalRot : Quaternion.Euler(0f, 0f, 180f);
        isAttacking = false;
    }

    private Vector3 AngleToLocalPosition(float angleDeg, float radius)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius, 0f);
    }

    private void ArcDamageCheck(float angle, float radius, float damage)
    {
        Vector2 worldPos = pivot.TransformPoint(transform.localPosition);
        Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, 0.3f);
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
}