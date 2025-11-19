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

    void OnTriggerEnter(Collider other)
    {
        // 敵やトラップに触れたらダメージ
        if (other.CompareTag("Enemy"))
        {
            hpBar.TakeDamage(10); // 10ダメージ（自由に変更可能）
        }

    }
    // プレイヤー死亡メソッド
    public void Die()
    {
        isDead = true;
        // 移動などの操作を無効化できます
    }
}