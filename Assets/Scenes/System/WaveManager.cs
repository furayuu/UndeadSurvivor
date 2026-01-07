using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [Header("Wave Settings")]
    public int currentWave = 1;
    public int totalWaves = 9;

    private float timer;
    private bool isCombatPhase = false;
    private bool waitingForNextWave = false;

    public event Action<int> OnWaveStart;
    public event Action<int> OnWaveEnd;

    public float RemainingTime => Mathf.Max(timer, 0f);

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // 开局暂停，先选武器
        GamePause.Pause();
        ShowStartWeaponSelection();
    }

    void Update()
    {
        if (!isCombatPhase || waitingForNextWave)
            return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            EndWave();
        }
    }

    #region Wave Flow

    void StartWave()
    {
        waitingForNextWave = false;
        isCombatPhase = true;

        timer = GetWaveDuration();
        GamePause.Resume();

        Debug.Log($"Wave {currentWave} Start");
        OnWaveStart?.Invoke(currentWave);
    }

    public void StartFirstWaveFromUI()
    {
        if (isCombatPhase) return;

        StartWave();
    }


    void EndWave()
    {
        isCombatPhase = false;
        waitingForNextWave = true;

        Debug.Log($"Wave {currentWave} End");
        OnWaveEnd?.Invoke(currentWave);

        // Wave 结束 → 打开升级界面
        UpgradeUI.Instance.ShowOptions(
            UpgradeManager.Instance.GetRandomOptions(),
            option =>
            {
                option.applyEffect?.Invoke();
                // 升级完成，但仍然暂停
            }
        );
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

    #endregion

    #region Start Weapon Selection

    void ShowStartWeaponSelection()
    {
        UpgradeUI.Instance.ShowOptions(
            UpgradeManager.Instance.GetStartWeaponOptions(),
            option =>
            {
                option.applyEffect?.Invoke();
                StartWave(); // 选完武器，正式开始第一关
            }
        );
    }

    #endregion

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
