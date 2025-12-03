using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    private float moveSpeed;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 入力の取得と正規化
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
        moveSpeed = Player.Instance.PlayerSpeed;
    }

    private void FixedUpdate()
    {
        // 物理演算による移動
        rb2d.velocity = moveInput * moveSpeed;
    }

    public Vector2 Move()
    {
        return moveInput;
    }
}