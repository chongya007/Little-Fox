using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Slider bgmVolume;
    public Slider effectVolume;
    public Toggle fullScreen;
    public Dropdown resolution;
    public Dropdown rate;

    private GameObject player;
    private bool work = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        bgmVolume.value = Info.bgmVolume;
        effectVolume.value = Info.effectVolume;

        resolution.value = Info.resolutionIndex;
        rate.value = Info.rateIndex;
        fullScreen.isOn = Info.fullScreen;

        work = true;
        ChangeVolumes();
        SetResolution();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Time.timeScale = 1f - Time.timeScale;
        }
    }
    //改变声音量
    public void ChangeVolumes()
    {
        if (work)
        {
            Info.bgmVolume = bgmVolume.value;
            Info.effectVolume = effectVolume.value;

            player.GetComponent<Player>().ChangeVolume();
        }
    }

    private int toInt(string s)
    {
        int ans = 0;
        for (int i = 0; i < s.Length; i++)
            ans = ans * 10 + s[i] - '0';
        return ans;
    }

    private Vector2 getResolution(string s)
    {
        return new Vector2(toInt(s.Substring(0, s.IndexOf('*'))), toInt(s.Substring(s.IndexOf('*') + 1)));
    }
    //设置分辨率和帧率
    public void SetResolution()
    {
        if (work)
        {

            Info.resolutionIndex = resolution.value;
            Info.rateIndex = rate.value;
            Info.fullScreen = fullScreen.isOn;

            Info.resolution = getResolution(resolution.options[Info.resolutionIndex].text);
            if (Info.rateIndex > 0)
            {
                Info.rate = toInt(rate.options[Info.rateIndex].text);
            }

            Info.SetResolution();

        }
    }
    //暂停
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    //继续
    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
