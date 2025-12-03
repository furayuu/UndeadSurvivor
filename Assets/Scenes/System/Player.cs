using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float playerSpeed = 5f;

    [Header("Components")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerSprite playerSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private bool isDead = false;

    public HPBar hpBar;  // HPバーへの参照
    public static Player Instance { get; private set; }

    public float PlayerSpeed => playerSpeed;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Animator Animator => animator;

    private void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Vector2 inputDir = playerMove.Move();

        // 向きとアニメーションの更新
        playerSprite.UpdateFacing(spriteRenderer, inputDir);
        playerSprite.UpdateAnimation(animator, inputDir, isDead);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            int damage = 10; // 或者根据敌人攻击数据设定
            hpBar.TakeDamage(damage); // 更新血条

            // 可选：死亡判定
            if (hpBar.currentHP <= 0)
            {
                Die();
            }
        }
    }

    // プレイヤー死亡メソッド
    public void Die()
    {
        isDead = true;
        // 移動などの操作を無効化できます
    }
}