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
    public XPBar xpBar;
    public HPBar hpBar;

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

    public void Die()
    {
        isDead = true;
    }

    public void AddExp(int amount)
    {
        if (xpBar != null)
            xpBar.AddXP(amount);
    }
    public void Heal(int amount)
    {
        if (hpBar != null)
            hpBar.Heal(amount);
    }

    public void IncreaseMaxHealth(int amount)
    {
        if (hpBar != null)
            hpBar.IncreaseMaxHP(amount);
    }

    public void IncreaseSpeed(float amount)
    {
        playerSpeed += amount;

        if (playerMove != null)
            playerMove.SetSpeed(playerSpeed);
    }
}
