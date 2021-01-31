using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{

    public Transform Up, Down;
    public float speed, dir;

    private float up, down;

    protected override void Awake()
    {
        base.Awake();
        up = Up.position.y;
        down = Down.position.y;
        Destroy(Up.gameObject);
        Destroy(Down.gameObject);
    }

    void FixedUpdate()
    {
        if (!die)
        {
            Move();
        }
    }

    void Move()
    {
        if (transform.position.y >= up || transform.position.y <= down)
        {
            dir = -dir;
        }
        rb.velocity = new Vector2(0, dir * speed);
    }

    protected override void SwitchAnimation()
    {
        
    }
}
