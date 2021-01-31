using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    public LayerMask ground;
    public Transform Left, Right;
    public float speed, jumpforce;
    public float dir;

    private bool isJump;
    private float left, right;

    protected override void Awake()
    {
        base.Awake();
        left = Left.position.x;
        right = Right.position.x;
        Destroy(Left.gameObject);
        Destroy(Right.gameObject);
    }

    void FixedUpdate()
    {
        if (!die)
        {
            Move();
            SwitchAnimation();
        }
        if (anim.GetBool("jumping"))
            Debug.Log("1");
    }
    
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        isJump = true;
        Debug.Log("jump");
    }

    private void Move()
    {
        if (transform.position.x <= left || transform.position.x >= right)
        {
            dir = -dir;
        }
        if (isJump)
        {
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
            transform.localScale = new Vector3(-dir, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    protected override void SwitchAnimation()
    {
        if (isJump)
        {
            if (rb.velocity.y > 0)
            {
                anim.SetBool("falling", false);
                anim.SetBool("jumping", true);
            }
            else
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
            anim.SetBool("jumping", false);
            isJump = false;
            Debug.Log("idle");
        }
    }
}
