using System;
using UnityEngine;
using TMPro; 

public class TimeCounter : MonoBehaviour
{
    public int countdownMinutes = 3;
    private float countdownSeconds;

    public TextMeshProUGUI timeText; 

    private void Start()
    {
        countdownSeconds = countdownMinutes * 60;
    }

    void Update()
    {
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            if (countdownSeconds < 0) countdownSeconds = 0;
        }

        int minutes = (int)(countdownSeconds / 60);
        int seconds = (int)(countdownSeconds % 60);

        timeText.text = $"{minutes:00}:{seconds:00}"; 

        if (countdownSeconds <= 0)
        {

        }
    }
}
