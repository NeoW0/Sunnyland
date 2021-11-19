using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public Collider2D coll;
    public Collider2D Discoll;
    public Transform CellingCheck;
    public AudioSource JumpAudio,HurtAudio,CherryAudio,MoveAudio;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public int Cherry;

    public Text CherryNumber;
    private bool isHurt;//Ĭ����false
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
    }

    void Movement()//��ɫ�ƶ�
    {
       
        float horizontalmove;
        float facedirection;
        horizontalmove=Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalmove * speed*Time.fixedDeltaTime, rb.velocity.y);
        anim.SetFloat("Running", Mathf.Abs(horizontalmove));
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }//��ɫ����
        if(Input.GetButtonDown("Jump")&& coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            JumpAudio.Play();
            anim.SetBool("Jumping", true);//
        }//��ɫ��Ծ����Ծ����

        Crouch();
    }

    void SwitchAnim()//��Ծ���䶯���л�
    {
        anim.SetBool("Idle", false);

        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("Falling", true);
        }
        if (anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }else if(isHurt)//���˶����л�������״̬�л�
        {
            anim.SetBool("Hurt", true);
            anim.SetFloat("Running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("Hurt", false);
                anim.SetBool("Idle", true);
                isHurt = false;
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Idle", true);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)//�ռ�
    {
        if(collision.tag == "Collection")
        {
            CherryAudio.Play();
            Destroy(collision.gameObject);
            Cherry += 1;
            CherryNumber.text = Cherry.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//������˼�����
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("Falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("Jumping", true);
            }else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                HurtAudio.Play();
                isHurt = true;
            }else if(transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                HurtAudio.Play();
                isHurt = true;
            }
        }
    }

    void Crouch()//��ɫ�¶׺���
    {
        if (!Physics2D.OverlapCircle(CellingCheck.position,0.4f,ground)) {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("Crouching", true);
                Discoll.enabled = false;
            } else if (Input.GetButtonUp("Crouch"))
            {
                anim.SetBool("Crouching", false);
                Discoll.enabled = true;
            }
        }

    }
}
