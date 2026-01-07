using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Update()
    {
        if (WaveManager.Instance == null) return;

        float time = WaveManager.Instance.RemainingTime;

        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
