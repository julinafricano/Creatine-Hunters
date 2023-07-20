using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Proj : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed;
    private Vector2 direction;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.velocity = direction * speed;
    }
    public void setup(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" )
        {
            collision.GetComponent<EnemyMush>().Damage(damage);
            Destroy(gameObject);

        }
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);

        }



    }
}
