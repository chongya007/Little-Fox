using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUD : MonoBehaviour
{

    public Transform Up, Down;
    public float speed, dir;

    private float up, down;

    void Awake()
    {
        up = Up.position.y;
        down = Down.position.y;
        Destroy(Up.gameObject);
        Destroy(Down.gameObject);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (transform.position.y >= up || transform.position.y <= down)
        {
            dir = -dir;
        }
        transform.Translate(Vector3.up * dir * speed * Time.fixedDeltaTime, Space.World);
        //rb.velocity = new Vector2(0, dir * speed);
    }
}
