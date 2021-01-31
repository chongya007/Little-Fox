using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{

    public float fadTime;

    private Renderer rend;
    [SerializeField]private bool isFad;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isFad)
        {
            if (rend.material.color.a > 0)
            {
                rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, Mathf.Max(0, rend.material.color.a - Time.deltaTime / fadTime));
                //rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, Mathf.Max(0, rend.material.color.a - 0.1f));
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFad = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFad = false;
        }
    }
}
