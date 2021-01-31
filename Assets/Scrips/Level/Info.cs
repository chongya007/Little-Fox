using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info
{
    public static int score;
    public static float bgmVolume = 1;
    public static float effectVolume = 1;
    public static bool fullScreen = false;
    public static int resolutionIndex = 1;
    public static Vector2 resolution = new Vector2(1920, 1080);
    public static int rateIndex = 0;
    public static int rate = 60;

    public static void SetResolution()
    {
        if (rateIndex == 0)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = rate + 2;
        }
        Screen.SetResolution((int)resolution.x, (int)resolution.y, fullScreen);
    }
}
