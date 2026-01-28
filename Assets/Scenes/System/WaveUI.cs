using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    public TMP_Text waveText;

    void Start()
    {
        if (WaveManager.Instance != null)
        {
            UpdateWave(WaveManager.Instance.currentWave);
            WaveManager.Instance.OnWaveStart += UpdateWave;
        }
    }

    void UpdateWave(int wave)
    {
        waveText.text = $"Wave {wave}";
    }

    void OnDestroy()
    {
        if (WaveManager.Instance != null)
            WaveManager.Instance.OnWaveStart -= UpdateWave;
    }
}
