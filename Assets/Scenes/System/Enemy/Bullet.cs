using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private Vector2 direction;

    // Enemy から進行方向を設定される
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // 画面外に出たら消す（任意）
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
