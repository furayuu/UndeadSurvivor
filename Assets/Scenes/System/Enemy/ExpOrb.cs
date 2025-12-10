using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public int expAmount = 1;

    private Transform player;
    private float attractSpeed = 0f;

    private const float pickupDistance = 0.5f;
    private const float attractStartDistance = 3f;
    private const float attractAcceleration = 8f;

    private void Start()
    {
        player = Player.Instance.transform;
    }

    private void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        // 开始吸附
        if (dist <= attractStartDistance)
        {
            attractSpeed += attractAcceleration * Time.deltaTime;
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                attractSpeed * Time.deltaTime
            );
        }

        // 完成拾取
        if (dist <= pickupDistance)
        {
            Player.Instance.AddExp(expAmount);
            Destroy(gameObject);

        }
    }
}
