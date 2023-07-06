using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float jumpforce;
    private bool isJumping;

    private Rigidbody2D rig;
    private Animator anim;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0) 
        {
            transform.eulerAngles = new Vector3(0,0,0);
            anim.SetInteger("transition", 1);

        }

        if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            anim.SetInteger("transition", 1);

        }
        if (movement == 0)
            anim.SetInteger("transition", 0);   

    }
    void Jump() 
    {
        if (Input.GetButtonDown("Jump")) 
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                isJumping = true;
                anim.SetInteger("transition", 2);

            }

        }  
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer == 8)
        {
            isJumping = false;
        }   
    }
}
