using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Collider2D head;
    private Animator anim;

    public float delay;//无敌帧长度
    public float speed, jumpforce;
    public Transform groundCheck;
    public Transform headCheck;
    public LayerMask ground;
    public Slider blood;
    public GameObject CollectedClip;
    [Space]
    public AudioSource bgmAudio;
    public AudioSource jumpAudio;
    public AudioSource hurtAudio;
    public AudioSource collectedAudio;
    [Space]
    private Text bloodText;
    private Text Score;
    private int oriScore;
    private bool isGround, isCrouch;
    private bool isHurt, canMove = true;
    private bool isDie;
    private bool jumpPressed, crouchPressed;
    private int jumpCount;
    private int jumpMaxTimes = 1;
    private int meet;
    [SerializeField]private float life = 100;
    private float lifeMax = 100;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        head = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        ChangeVolume();
    }

    void Start()
    {
        Score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        Score.text = Info.score.ToString();
        oriScore = Info.score;
        bloodText = blood.GetComponentInChildren<Text>();
    }
    //按键检测
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0 && !isCrouch)
        {
            jumpPressed = true;
        }
        crouchPressed = Input.GetButton("Crouch");
    }
    //物理计算
    void FixedUpdate()
    {
        if (!isDie && life <= 0)
        {
            Die();
        }
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if (canMove)
        {
            Move();
            Jump();
            Crouch();
        }
        Meet();
        Recover();
        SwitchAnimation();
    }
    //角色移动
    void Move()
    {
        float dir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);

        if (dir != 0)
        {
            transform.localScale = new Vector3(dir, 1, 1);
            blood.transform.localScale = new Vector3(dir * Mathf.Abs(blood.transform.localScale.x), blood.transform.localScale.y, blood.transform.localScale.z);
        }
    }
    //角色跳跃
    void Jump()
    {
        if (isGround)
        {
            jumpCount = jumpMaxTimes;
        }
        if (jumpPressed && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount--;
            jumpPressed = false;
            jumpAudio.Play();
        }
    }
    //角色下蹲
    void Crouch()
    {
        if (!isGround)
        {
            isCrouch = false;
            head.enabled = true;
        }
        if(crouchPressed)
        {
            if (isGround)
            {
                isCrouch = true;
                head.enabled = false;
            }
        }
        else
        {
            if (isCrouch && !Physics2D.OverlapCircle(headCheck.position, 0.1f, ground))
            {
                isCrouch = false;
                head.enabled = true;
            }
        }
    }
    //物品结算
    void Meet()
    {
        switch (meet)
        {
            case 1:
                jumpMaxTimes++;
                break;
            case 2:
                Info.score++;
                Score.text = Info.score.ToString();
                break;
        }
        meet = 0;
    }
    //动画切换
    void SwitchAnimation()
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
        anim.SetBool("crouch", isCrouch);
        anim.SetBool("hurt", isHurt || isDie);

        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else
        {
            if (rb.velocity.y > 0)
            {
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
            }
            else
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
    }
    //回复血量
    void Recover()
    {
        if (!isHurt && life < lifeMax)
        {
            life += 0.5f*Time.deltaTime;
        }
        if (blood != null && bloodText != null)
        {
            blood.value = Mathf.Max(life / lifeMax, 0f);
            bloodText.text = ((int)life).ToString();
        }
    }
    //受伤
    void Hurt(float blood)
    {
        life -= blood;
        isHurt = true;
        Invoke(nameof(noHurt), delay);
        canMove = false;
        hurtAudio.Play();
    }
    //恢复硬直
    void noHurt()
    {
        isHurt = false;
        canMove = true;
    }
    //音量调整
    public void ChangeVolume()
    {
        bgmAudio.volume = Info.bgmVolume / 10f;
        jumpAudio.volume = Info.effectVolume;
        hurtAudio.volume = Info.effectVolume;
        collectedAudio.volume = Info.effectVolume;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //敌人检测
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isGround && rb.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            //踩到敌人头上
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            }
            else if(!isHurt)
            //受伤
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                if (transform.position.x < collision.transform.position.x)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                Hurt(20);
            }
        }
        /*
        else if (collision.gameObject.CompareTag("HiddenGround"))
        {
            Vector3 hitPosition = Vector3.zero;
            Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }*/
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && !isHurt)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            if (transform.position.x < collision.GetContact(0).point.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            Hurt(20);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //物品检测
        meet = 0;
        switch (collision.tag)
        {
            case "Gem":
                meet = 1;
                break;
            case "Cherry":
                meet = 2;
                break;
            case "Deadline":
                Die();
                break;
        }
        if (meet > 0)
        {
            collectedAudio.Play();
            Destroy(collision.gameObject);
            Instantiate(CollectedClip, collision.transform.position, collision.transform.rotation);
        }
    }
    //死亡
    void Die()
    {
        canMove = false;
        isDie = true;
        Invoke(nameof(Reload), 2f);
    }
    //重新加载场景
    void Reload()
    {
        Info.score = oriScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
