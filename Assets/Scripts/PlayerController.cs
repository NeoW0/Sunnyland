using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public Collider2D coll;
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
        rb.velocity = new Vector2(horizontalmove * speed*Time.deltaTime, rb.velocity.y);
        anim.SetFloat("Running", Mathf.Abs(horizontalmove));
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }//��ɫ����
        if(Input.GetButton("Jump")&& coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("Jumping", true);//
        }//��ɫ��Ծ����Ծ����
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
            Destroy(collision.gameObject);
            Cherry += 1;
            CherryNumber.text = Cherry.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (anim.GetBool("Falling"))
            { 
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("Jumping", true);
            }else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }else if(transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
    }


}
