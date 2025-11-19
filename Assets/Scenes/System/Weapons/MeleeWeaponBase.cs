using UnityEngine;

public abstract class MeleeWeaponBase : WeaponBase
{
    protected bool isAttacking;
    protected Transform pivot;
    protected Vector3 startLocalPos;
    protected Quaternion startLocalRot;
    protected bool facingRight = true; // デフォルトで右向き

    protected override void Start()
    {
        base.Start();
        pivot = owner != null ? owner : transform.parent;

        // 初期位置と回転を保存
        transform.parent = pivot;
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;

        UpdateWeaponFacing(); // 武器の向きを初期化
    }

    protected override void Update()
    {
        // 攻撃可能かつ攻撃中でない場合に攻撃
        if (Time.time >= nextAttackTime && !isAttacking)
        {
            TryAttack();
            nextAttackTime = Time.time + 1f / weaponData.attackRate;
        }

        // 武器の向きを更新（攻撃中以外）
        if (!isAttacking)
        {
            UpdateWeaponFacing();
        }
    }

    // 武器の向きを更新
    protected virtual void UpdateWeaponFacing()
    {
        if (pivot == null) return;

        // プレイヤーのスプライトの向きを取得
        SpriteRenderer playerSprite = Player.Instance.SpriteRenderer;
        if (playerSprite != null)
        {
            bool newFacingRight = !playerSprite.flipX;

            // 向きが変更された場合
            if (newFacingRight != facingRight)
            {
                facingRight = newFacingRight;

                // 向きに応じて武器の位置と回転を調整
                if (facingRight)
                {
                    // 右向き
                    transform.localPosition = new Vector3(
                        Mathf.Abs(startLocalPos.x),
                        startLocalPos.y,
                        startLocalPos.z
                    );
                    transform.localRotation = startLocalRot;
                }
                else
                {
                    // 左向き - 水平位置を反転、垂直位置は保持
                    transform.localPosition = new Vector3(
                        -Mathf.Abs(startLocalPos.x),
                        startLocalPos.y,
                        startLocalPos.z
                    );
                    // 回転も反転が必要な場合はサブクラスで処理
                    transform.localRotation = startLocalRot;
                }
            }
        }
    }
}