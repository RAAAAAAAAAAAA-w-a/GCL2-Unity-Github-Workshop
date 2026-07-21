using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 4f;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] public float jumpBoost = 1.5f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    // Public so other scripts can access it
    public Rigidbody2D rb { get; private set; }
    public bool IsFacingRight => isFacingRight;

    private Animator anim;
    private AudioSource audioSource;

    public bool canMove = true;
    public float moveInput;
    public bool isFacingRight = true;
    public bool isGrounded;
    public bool isJumping;
    public bool canClimb = false;
    public float climbInput;

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

        hammerTime = GetComponent<hammerPowerup>();
    }

    void Start()
    {
        respawnPosition = transform.position;
    }

    void Update()
    {
        // Ground Check
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        if (isGrounded)
        {
            isJumping = false;
        }

        // Horizontal Movement
        if (canMove)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        // Ladder Climbing
        if (canClimb && (hammerTime == null || !hammerTime.isHammerActive))
        {
            climbInput = Input.GetAxisRaw("Vertical");
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, climbInput * climbSpeed);
        }
        else
        {
            rb.gravityScale = 1;
        }

        // Clean Sprite Flipping Logic
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Flips sprite to face Right
            isFacingRight = true;
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);  // Keeps default sprite facing Left
            isFacingRight = false;
        }

        // Jump Input
        if (Input.GetButtonDown("Jump") && isGrounded && (hammerTime == null || !hammerTime.isHammerActive))
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder") && (hammerTime == null || !hammerTime.isHammerActive))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
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