using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;
    //private Animator Anim;
    private Collider2D coll;
    public LayerMask ground;
    public Transform leftpoint, rightpoint;
    public float Speed, JumpForce;
    private float leftx, rightx;

    private bool Faceleft = true;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //Anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        SwitchAnim();
    }

    void Movement()
    {
        if (Faceleft)//面向左侧
        {
            if (coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            if ((transform.position.x <= leftx))
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else//面向右侧
        {
            if (coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }
            if ((transform.position.x >= rightx))
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }


    void SwitchAnim()//移动动画效果
    {
        if (Anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                Anim.SetBool("Jumping", false);
                Anim.SetBool("Falling", true);
            }
        }
        if (coll.IsTouchingLayers(ground) && Anim.GetBool("Falling"))
        {
            Anim.SetBool("Falling", false);
        }

    }

}
