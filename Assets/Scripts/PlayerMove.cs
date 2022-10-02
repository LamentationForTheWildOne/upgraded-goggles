using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float speed =  4f;
    float jstr = 5f;
    public float accel = 0f;
    float moveX;
    public float castDist = 0.2f;

    bool canJump;
    bool isGrounded;

    Rigidbody2D rb;
    BoxCollider2D coll;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            canJump = true;
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
            }
            if (accel < 0)
            {
                accel += 1f * Time.deltaTime;
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
    }

    void HorizontalMove()
    {
        rb.velocity = new Vector3((moveX * speed) + accel, rb.velocity.y);
        if (rb.velocity.x != 0)
        {
            //myAnim.SetBool("iswalking", true);
        }
        else
        {
            //myAnim.SetBool("iswalking", false);
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
}
