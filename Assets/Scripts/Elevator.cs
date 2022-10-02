using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D coll;
    public float topdis;
    float top;
    public float speed;
    bool rising;
    float bottom;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       bottom = transform.position.y;
        top = bottom + topdis;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rising = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rising = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rising == true)
        {
            if (transform.position.y < top)
            {
                speed = 5f;
            }
            else 
            {
                speed = 0f;
            }
        }
        else 
        {
            if (transform.position.y > bottom)
            {
                speed = -5f;
            }
            else 
            {
                speed = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }
}
