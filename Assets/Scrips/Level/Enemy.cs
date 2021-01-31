using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected AudioSource audioSource;
    protected bool die;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void JumpOn()
    {
        audioSource.Play();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
        coll.enabled = false;
        anim.SetTrigger("death");
        die = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    protected abstract void SwitchAnimation();
}
