using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public static bool IsPaused = false;

    public static void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
}
