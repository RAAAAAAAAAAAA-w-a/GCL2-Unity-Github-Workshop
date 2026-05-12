using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour

{
    public float jumpSpeed;
    public float moveSpeed;
    public Rigidbody2D rb;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask realGround;
    public bool isGrounded;


    public bool canMove = true;
    public Vector3 respawnPosition;

    public LevelManager theLevelManager;

    public float knockbackForce;
    public float knockbackLength;
    private float knockbackCounter;



    private Animator MyAnim;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      

        rb = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();

        respawnPosition = transform.position;
        theLevelManager = FindFirstObjectByType<LevelManager>();


    }



    public void Knockback()
    {
        knockbackCounter = knockbackLength;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillPlane"))
        {
            theLevelManager.HurtPlayer(100);
        }

        if (other.CompareTag("Enemy"))
        {
            theLevelManager.HurtPlayer(100);
        }

        if (other.tag == "Checkpoint")
        {
            respawnPosition = other.transform.position;
        }

    }


    // Update is called once per frame
    void Update()
    {

       

        float moveInput = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, realGround);

        

        if (moveInput > 0)
        {
            rb.linearVelocityX = moveSpeed;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (moveInput < 0)
        {
            rb.linearVelocityX = -moveSpeed;
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            rb.linearVelocityX = 0;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocityY = jumpSpeed;
        }
        

        MyAnim.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));
        MyAnim.SetBool("Ground", isGrounded);


        if (knockbackCounter <= 0 && canMove)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocityY);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocityY);
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpSpeed);
            }

           
        }
        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;

            if (transform.localScale.x > 0.0f)
            {
                rb.linearVelocity = new Vector3(-knockbackForce, knockbackForce, 0.0f);
            }
            else
            {
                rb.linearVelocity = new Vector3(knockbackForce, knockbackForce, 0.0f);
            }
        }

    }

   



}
