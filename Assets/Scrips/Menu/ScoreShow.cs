using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShow : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = Info.score.ToString();
    }
}
