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

    public static Player Instance { get; private set; }

    public float PlayerSpeed => playerSpeed;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Animator Animator => animator;

    private void Awake()
    {
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

        playerSprite.UpdateFacing(spriteRenderer, inputDir);
        playerSprite.UpdateAnimation(animator, inputDir, isDead);
    }

    // 示例：玩家死亡方法
    public void Die()
    {
        isDead = true;
        // 可以在这里禁止移动等操作
    }
}
