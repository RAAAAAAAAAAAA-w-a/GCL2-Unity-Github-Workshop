using UnityEngine;

public class MarioMovertest : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask realGround;
    public bool isGrounded;

    public bool canMove = true;
    public Vector3 respawnPosition;

    public float knockbackForce = 5f;
    public float knockbackLength = 0.5f;
    private float knockbackCounter;

    private Animator MyAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();

        respawnPosition = transform.position;
    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, realGround);

        // Knockback logic
        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;
            float direction = transform.localScale.x > 0 ? -1 : 1;
            rb.linearVelocity = new Vector2(direction * knockbackForce, knockbackForce);
            return; // skip normal movement while knocked back
        }

        if (!canMove) return;

        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip sprite
        if (moveInput > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }

        // Animator parameters
        MyAnim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        MyAnim.SetBool("Ground", isGrounded);
    }
}
