using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;   // Bullet のPrefab
    public Transform firePoint;        // 発射位置
    public float shootInterval = 1.5f; // 発射間隔（秒）

    private Transform player;
    private float timer;

    void Start()
    {
        // Player タグを持つオブジェクトを取得
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

}
