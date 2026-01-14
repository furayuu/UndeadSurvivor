using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int AliveEnemyCount;

    public GameObject[] enemyPrefabs;
    public float baseSpawnInterval = 2f;
    public int baseMaxEnemies = 30;
    public float spawnRadius = 15f;

    private Transform player;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        timer = baseSpawnInterval;
    }

    void Update()
    {
        if (!WaveManager.Instance.IsCombatPhase())
            return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = GetSpawnInterval();
            TrySpawnEnemy();
        }
    }

    void TrySpawnEnemy()
    {
        if (player == null) return;
        if (AliveEnemyCount >= GetMaxEnemies()) return;

        Vector2 spawnPos =
            (Vector2)player.position +
            Random.insideUnitCircle.normalized * spawnRadius;

        GameObject prefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    float GetSpawnInterval()
    {
        int wave = WaveManager.Instance.currentWave;
        return Mathf.Max(0.6f, baseSpawnInterval - wave * 0.1f);
    }

    int GetMaxEnemies()
    {
        int wave = WaveManager.Instance.currentWave;

        if (wave >= 6)
            return Mathf.RoundToInt(baseMaxEnemies * 1.6f);

        if (wave >= 3)
            return Mathf.RoundToInt(baseMaxEnemies * 1.3f);

        return baseMaxEnemies;
    }

    public static void ClearAllEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var e in enemies)
            Destroy(e);
    }
}
