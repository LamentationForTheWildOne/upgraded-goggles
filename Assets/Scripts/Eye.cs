using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public float speed = 2;
    float moveX;
    public float castDist;

    bool moveLeft;

    Rigidbody2D rb;
    BoxCollider2D coll;
    AudioSource aud;

    public AudioClip dEYEsfx;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitright = Physics2D.Raycast(new Vector3 (transform.position.x + 1,transform.position.y,transform.position.z), Vector2.down, castDist);
        Debug.DrawRay(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Vector2.down * castDist, new Color(255, 0, 255));

        if (hitright.collider == null)
        {
            moveLeft = true;
        }

        RaycastHit2D hitleft = Physics2D.Raycast(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Vector2.down, castDist);
        Debug.DrawRay(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Vector2.down * castDist, new Color(255, 0, 255));

        if (hitleft.collider == null)
        {
            moveLeft = false;
        }

        if (moveLeft)
        {
            moveX = -1;
        }
        else {
            moveX = 1;
        }
    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector3(moveX * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Blaster") {
            
            AudioSource.PlayClipAtPoint(dEYEsfx, this.gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
