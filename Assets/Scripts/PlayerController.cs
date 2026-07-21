using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] public float jumpBoost = 1.5f;


    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Audio")]
    //[SerializeField] private AudioClip jumpClip;

    // Public so other scripts can access it
    public Rigidbody2D rb { get; private set; }


    public bool IsFacingRight => isFacingRight;

    private Animator anim;
    private AudioSource audioSource;

    public bool canMove = true;
    private float moveInput;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isJumping;
    private bool canClimb = false;

    private hammerPowerup hammer;

    // Respawn position
    private Vector3 respawnPosition;

    private hammerPowerup hammerTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    void Start()
    {
        // Set initial respawn position to player's starting position
        respawnPosition = transform.position;
    }

    void Update()
    {// Check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Reset jumping when landed
        if (isGrounded)
        {
            isJumping = false;
        }
        // Horizontal movement
        if (canMove)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
        Debug.Log("Velocity = " + rb.linearVelocity);
        Debug.Log("Grounded: " + isGrounded);
        if (canClimb & !isGrounded)
        {
            rb.gravityScale = 0;

            float climbInput = Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, climbInput * climbSpeed);
        }
        
        if(canClimb & isGrounded)
        {
            rb.gravityScale = 0;

            float climbInput = Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector2(moveInput * 0, climbInput * climbSpeed);

        }

        else
        {
            rb.gravityScale = 1;
        }

        // Flip sprite
        if (moveInput < 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (moveInput > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);



        // Flip player sprite to movement direction

        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }

        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }


        // Jump
        if (Input.GetButtonDown("Jump") &&
      isGrounded &&
      (hammerTime == null || !hammerTime.isHammerActive))
        {
            Jump();
        }


        UpdateAnimations();


    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isJumping = true;

        if (anim != null)
        {
            anim.SetBool("isJumping", true);
        }

        //if (jumpClip != null && audioSource != null)
        //{
        //    audioSource.PlayOneShot(jumpClip);
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            print("Climbing");
            canClimb = true;
        }

        else
        {
            canClimb = false;
        }
            
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void UpdateAnimations()
    {
        if (anim == null) return;

        bool isRunning = Mathf.Abs(moveInput) > 0.1f && isGrounded;
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isJumping", isJumping || !isGrounded);
        anim.SetBool("isGrounded", isGrounded);
    }

    public Vector3 GetRespawnPosition()
    {
        return respawnPosition;
    }

    

    



}
