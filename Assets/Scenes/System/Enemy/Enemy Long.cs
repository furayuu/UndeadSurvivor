using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    public float lifetime = 3f;

    public int damage = 1;


    public void OnEnable()
    {
        Invoke(nameof(Deactivate), lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var playerHealth =
                collision.GetComponent<HPBar>();

            if (playerHealth != null)
                playerHealth. TakeDamage(damage);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}