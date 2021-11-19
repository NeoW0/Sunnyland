using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{

    private Rigidbody2D rb;
    private Collider2D coll;
    public Transform top, buttom;
    public float speed;
    private float TopY, ButtomY;

    private bool isUP;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        TopY = top.position.y;
        ButtomY = buttom.position.y;
        Destroy(top.gameObject);
        Destroy(buttom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(isUP)//面向上方
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if(transform.position.y >TopY)
            {
                isUP = false;
            }
        }else//面向下方
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if(transform.position.y < ButtomY)
            {
                isUP = true;
            }
        }
    }
}
