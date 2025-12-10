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
        moveSpeed = Player.Instance.PlayerSpeed;
    }

    private void Update()
    {
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = moveInput * moveSpeed;
    }

    public Vector2 Move()
    {
        return moveInput;
    }
}
