using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int currentWave = 1;
    public int totalWaves = 9;

    private float timer;
    private bool isCombatPhase;

    public float RemainingTime => Mathf.Max(timer, 0f);

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (!isCombatPhase) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            EndWave();
        }
    }

    public void StartFirstWave()
    {
        StartWave();
    }

    void StartWave()
    {
        isCombatPhase = true;
        timer = GetWaveDuration();
        GamePause.Resume();

        Debug.Log($"Wave {currentWave} Start");
    }

    void EndWave()
    {
        isCombatPhase = false;
        EnemySpawner.ClearAllEnemies();
        Debug.Log($"Wave {currentWave} End");

        // 波次结束 → 打开升级界面
        UpgradeUI.Instance.Show();
    }

    public void StartNextWave()
    {
        if (currentWave >= totalWaves)
        {
            GameClear();
            return;
        }

        currentWave++;
        StartWave();
    }

    float GetWaveDuration()
    {
        if (currentWave <= 3) return 45f;
        if (currentWave <= 6) return 60f;
        return 90f;
    }

    void GameClear()
    {
        Debug.Log("Game Clear!");
        GamePause.Pause();
    }
    public bool IsCombatPhase()
    {
        return isCombatPhase;
    }

}
