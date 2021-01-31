using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{

    public Transform Up;
    public float speed;

    private Rigidbody2D rb;
    private float up, down;
    private bool go;

    void Start()
    {
        up = Up.position.y;
        down = transform.position.y;
        Destroy(Up.gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        if (go)
        {
            if (transform.position.y <= up)
            {
                rb.velocity = Vector3.up * speed;
                //transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.y >= down)
            {
                rb.velocity = Vector3.down * speed;
                //transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            go = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            go = false;
        }
    }
}
