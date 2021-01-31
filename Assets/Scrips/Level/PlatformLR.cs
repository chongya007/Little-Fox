using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLR : MonoBehaviour
{

    public Transform Left, Right;
    [SerializeField]public float speed, dir;

    [SerializeField]private float left, right;

    void Awake()
    {
        right = Right.position.x;
        left = Left.position.x;
        Destroy(Right.gameObject);
        Destroy(Left.gameObject);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (transform.position.x >= right || transform.position.x <= left)
        {
            dir = -dir;
        }
        transform.Translate(Vector3.right * dir * speed * Time.fixedDeltaTime, Space.World);
        //rb.velocity = new Vector2(0, dir * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
