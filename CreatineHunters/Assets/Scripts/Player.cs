using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float jumpforce;
    private bool isJumping;
    private bool isFire;

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
        RayFire();
    }
    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0 ) 
        {
            if (!isJumping) 
            {
                anim.SetInteger("transition", 1);

            }
            transform.eulerAngles = new Vector3(0,0,0);

        }

        if (movement < 0)
        {
            if (!isJumping) 
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
            

        }
        if (movement == 0 && !isJumping && !isFire)
            anim.SetInteger("transition", 0);   

    }
    void Jump() 
    {
        if (Input.GetButtonDown("Jump")) 
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                isJumping = true;
               

            }

        }  
    }

    void RayFire()
    {

        StartCoroutine("Fire");
    }

    
    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isFire = true;
            anim.SetInteger("transition", 3);
            yield return new WaitForSeconds(0.7f);
            anim.SetInteger("transition", 0);

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
