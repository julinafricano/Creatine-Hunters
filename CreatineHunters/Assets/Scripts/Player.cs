using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float jumpforce;

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
            anim.SetInteger("transition", 2);

            rig.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
    }
}
