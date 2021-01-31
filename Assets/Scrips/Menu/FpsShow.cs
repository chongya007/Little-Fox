using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsShow : MonoBehaviour
{

    private Text FPS;
    private float lastTime;
    private int lastFrame;

    // Start is called before the first frame update
    void Start()
    {
        FPS = GetComponent<Text>();
    }

    void Update()
    {
        if(Time.realtimeSinceStartup - lastTime > 1.0f)
        {
            ShowFPS();
        }
    }

    void ShowFPS()
    {
        FPS.text = ((int)((Time.frameCount - lastFrame) / (Time.realtimeSinceStartup - lastTime))).ToString();
        FPS.text += " fps";
        lastFrame = Time.frameCount;
        lastTime = Time.realtimeSinceStartup;
    }
}
