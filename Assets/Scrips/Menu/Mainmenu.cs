using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    public float speed;

    void Awake()
    {
    }

    void Start()
    {
        Info.SetResolution();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Info.score = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        if (transform.position.x < -24)
        {
            transform.Translate(Vector3.right * 24, Space.World);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
