using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;

    public float speed;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;

    public LayerMask layer;
    private bool colliding;

    public CircleCollider2D circleCollider2D;
    public BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - headPoint.position.y;

            if(height > 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                anim.SetTrigger("die");
                rig.bodyType = RigidbodyType2D.Kinematic;
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                Destroy(gameObject, 0.33f);
            }
        }        
    }
}
