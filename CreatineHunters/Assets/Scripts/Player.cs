using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public int health = 3;
    public float speed;
    public float jumpforce;
    private bool isJumping;
    private bool isFire;

    public Transform firepoint;
    public GameObject proj; 

    private Rigidbody2D rig;
    private Animator anim;

    private float lookdir;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lookdir = 1;

        GameController.instance.UpdateLives(health);    

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
            lookdir = 1;
            if (!isJumping) 
            {
                anim.SetInteger("transition", 1);

            }
            transform.eulerAngles = new Vector3(0,0,0);

        }

        if (movement < 0)
        {   
            lookdir = -1;
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
        if (isFire) return;
        if (Input.GetKeyDown(KeyCode.E))   
        {
            
            StartCoroutine("Fire");
        }
        
    }

    IEnumerator Fire()
    {
       
        isFire = true;
        anim.SetInteger("transition", 3);
        Instantiate(proj, firepoint.position, Quaternion.identity).GetComponent<Proj>().setup(Vector2.right * lookdir) ;
        yield return new WaitForSeconds(0.7f);
        anim.SetInteger("transition", 0);
        isFire = false;

    
    }
    public void Damage(int dmg)
    {
        health -= dmg;
        GameController.instance.UpdateLives(health);
        anim.SetTrigger("hit");

        if (transform.rotation.y == 0)
        {
            transform.position += new Vector3(-1f, 0,0);
        }

        else
        {
            transform.position += new Vector3(1f, 0, 0);
        }

        if (health <= 0) 
        {
                   
        }
    }

    public void IncreaseLife(int value)

    {
        health += value;
        GameController.instance.UpdateLives(health);
    }


private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer == 8)
        {
            isJumping = false;
        }   
    }
}
