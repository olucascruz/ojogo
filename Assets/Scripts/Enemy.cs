using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;

    public float speed;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;

    private bool colliding;

    public LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if(colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y );
            speed *= -1f;

        }
        
    }

    bool playerDestroyerd;
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player"){

            float height = col.contacts[0].point.y - headPoint.position.y;

            if(height > 0 && !playerDestroyerd)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                speed = 0;
                anim.SetTrigger("die");
                
                Destroy(gameObject, 0.26f);
            }else
            {
                playerDestroyerd = true;
                GameController.instance.ShowGameOver();
                Destroy(col.gameObject);
            }
        
        }
    
    
    }

}
