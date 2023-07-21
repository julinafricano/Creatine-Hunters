using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMush : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rig;
    public float walktime;
    public bool walkRight = true;
    private float timer;

    private Animator anim;
    public int health;

    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= walktime )
        {
            walkRight = !walkRight;
            timer = 0f;
        }
        if (walkRight)
        {
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.right * speed;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            rig.velocity = Vector2.left * speed;
    
        }
    }
    public void Damage(int dmg)
    {
        health -= dmg;
        anim.SetTrigger("hit");
        if (health <= 0)
        {
            anim.SetTrigger("death");
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "Player")
        {
            
            collision.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
}
