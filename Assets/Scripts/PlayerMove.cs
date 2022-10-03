using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float speed =  10f;
    float jstr = 15f;
    public float accel = 0f;
    float moveX;
    public float castDist = 0.2f;
    float dir = -1;
    float gscal = 5f;
    float gfall = 4f;


    bool canJump;
    bool isGrounded;
    bool lookLeft = false;

    Rigidbody2D rb;
    BoxCollider2D coll;
    Animator ani;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
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
        }
        else
        {
            if (accel > 0)
            {
                accel -= 1f * Time.deltaTime;
                if (accel > 0.05)
                {
                    accel = 0;
                }
            }
            if (accel < 0)
            {
                accel += 1f * Time.deltaTime;
                if (accel < 0.05) {
                    accel = 0;
                }
            }
        }

         
    }

    private void FixedUpdate()
    {
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
    }

    void HorizontalMove()
    {
        rb.velocity = new Vector3((moveX * speed) + accel, rb.velocity.y);
        if (rb.velocity.x != 0)
        {
            ani.SetBool("isWalking", true);
        }
        else
        {
            ani.SetBool("isWalking", false);
        }

    }
    void Jump()
    {
        if (!isGrounded)
        {
            return;
        }

        isGrounded = false;
        rb.AddForce(Vector2.up * jstr, ForceMode2D.Impulse);
    }

    void Turn() {
        dir *= -1;
        gameObject.transform.localScale = new Vector3(dir, 1, 1); 
    }
}
