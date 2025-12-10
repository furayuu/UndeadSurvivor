using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelWeapon : MeleeWeaponBase
{
    private Vector3 startLocalPos;
    private Quaternion startLocalRot;
    private HashSet<EnemyBase> hitEnemies = new HashSet<EnemyBase>();
    private SpriteRenderer playerSprite;

    protected override void Start()
    {
        base.Start();

        if (pivot == null)
            pivot = owner != null ? owner : transform.parent;

        transform.parent = pivot;
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;

        playerSprite = Player.Instance.SpriteRenderer;
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

        // 检测玩家面向方向
        bool facingRight = !playerSprite.flipX;
        float startAngle = facingRight ? 45f : 135f;
        float endAngle = facingRight ? -45f : 225f; // 左边旋转角度更大
        float angleDir = facingRight ? 1f : -1f;    // 控制弧线方向

        Vector3 startPos = startLocalPos;
        Vector3 targetPos = AngleToLocalPosition(startAngle, radius);
        float t = 0f;
        while (t < 0.15f)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, t / 0.15f);
            transform.localRotation = Quaternion.Euler(0f, 0f, startAngle);
            yield return null;
        }
        
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

        
        while (Vector3.Distance(transform.localPosition, startLocalPos) > 0.05f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startLocalPos, extendSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, startLocalRot, extendSpeed * Time.deltaTime);
            yield return null;
        }

        transform.localPosition = startLocalPos;
        transform.localRotation = startLocalRot;
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

    private void OnDrawGizmosSelected()
    {
        if (pivot == null) return;

        Gizmos.color = Color.green;
        float radius = weaponData != null ? weaponData.extendDistance : 1f;
        Vector3 rightUp = pivot.TransformPoint(AngleToLocalPosition(45, radius));
        Vector3 rightDown = pivot.TransformPoint(AngleToLocalPosition(-45, radius));
        Vector3 leftUp = pivot.TransformPoint(AngleToLocalPosition(135, radius));
        Vector3 leftDown = pivot.TransformPoint(AngleToLocalPosition(225, radius));

        Gizmos.DrawLine(pivot.position, rightUp);
        Gizmos.DrawLine(pivot.position, rightDown);
        Gizmos.DrawLine(pivot.position, leftUp);
        Gizmos.DrawLine(pivot.position, leftDown);
    }
}
