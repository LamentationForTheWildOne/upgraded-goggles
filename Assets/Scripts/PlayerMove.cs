using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float speed = 10f;
    float jstr = 15f;
    public float accel = 0f;
    float moveX;
    public float castDist = 0.2f;
    float dir = -1;
    float gscal = 5f;
    float gfall = 4f;
    float bspeed = 600f;
    public float shotspd;


    bool canJump;
    bool isGrounded;
    bool lookLeft = false;
    bool fire;

    Rigidbody2D rb;
    public Rigidbody2D blasterPrefab;
    BoxCollider2D coll;
    Animator ani;
    AudioSource aud;

    public AudioClip jumpsfx;
    public AudioClip shootsfx;
    public AudioClip walksfx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }
    void Update()
    {
        //Inputs
        if (Input.GetKeyDown(KeyCode.W)) {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && lookLeft == true) {
            lookLeft = false;
            Turn();
        }
        if (Input.GetKeyDown(KeyCode.D) && lookLeft == false)
        {
            lookLeft = true;
            Turn();
        }
        if (Input.GetKeyDown(KeyCode.Space) && shotspd == 0) {
            fire = true;
            shotspd = 60f;
        }
        //ray
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, new Color(255, 0, 255));
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //acceleration calc

        if (accel > 5) {
            accel = 5;
        }
        if (accel < -5)
        {
            accel = -5;
        }

        moveX = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") >= 0)
            {
                accel += 1f * Time.deltaTime;
            }
            if (Input.GetAxis("Horizontal") <= 0)
            {
                accel -= 1f * Time.deltaTime;
            }
            ani.SetBool("isWalking", true);

            
        }
        else
        {
            if (accel > 0)
            {
                accel -= (3f * Time.deltaTime);
                if (accel < 0.05)
                {
                    accel = 0;
                }
            }
            if (accel < 0)
            {
                accel += (3f * Time.deltaTime);
                if (accel > -0.05) {
                    accel = 0;
                }
            }
            ani.SetBool("isWalking", false);

            
                

            

        }


    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (isGrounded == true)
            {
                if (!aud.isPlaying)
                {
                    aud.clip = walksfx;
                    aud.Play();
                }

            }
            else
            {
                if (aud.isPlaying)
                {
                    aud.Stop();
                }

            }

        }
        else {
            if (aud.isPlaying)
            {
                aud.Stop();
            }
        }

            if (fire == true)
        {
            Fire();
            fire = false;
        }
        HorizontalMove();
        if (canJump == true) {
            canJump = false;
            
            Jump();
        }
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = gscal;

        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = gfall;
        }

        if (shotspd > 0)
        {
            shotspd -= 1;
        }

    }

 

    void HorizontalMove()
    {
        rb.velocity = new Vector3((moveX * speed)+accel, rb.velocity.y);
        if (rb.velocity.x != 0)
        {
            //ani.SetBool("isWalking", true);
        }
        else
        {
            //ani.SetBool("isWalking", false);
        }

    }
    void Jump()
    {
        if (!isGrounded)
        {
            return;
        }
        AudioSource.PlayClipAtPoint(jumpsfx, this.gameObject.transform.position);
        isGrounded = false;
        rb.AddForce(Vector2.up * jstr, ForceMode2D.Impulse);
    }

    void Turn() {
        dir *= -1;
        gameObject.transform.localScale = new Vector3(dir, 1, 1); 
    }

    void Fire() {
        AudioSource.PlayClipAtPoint(shootsfx, this.gameObject.transform.position);

        Rigidbody2D star = Instantiate(blasterPrefab, transform.position, transform.rotation) as Rigidbody2D;
        
        if (lookLeft == true)
        {
            star.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bspeed);
            star.gameObject.transform.localScale = new Vector3(1, 1, 1);
            accel -= 5;
            rb.AddForce(Vector2.left * 30f, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
        else {
            star.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bspeed);
            star.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            accel += 5;
            rb.AddForce(Vector2.right * 30f, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
    }
}
