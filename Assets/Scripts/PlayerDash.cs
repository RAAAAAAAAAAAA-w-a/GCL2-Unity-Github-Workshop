using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.05f;
    public int maxDashes = 3;

    [Header("Effects")]
    public ParticleSystem dashEffect;

    private int dashRemaining;
    private bool isDashing;

    private Rigidbody2D rb;
    private PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerController>();

        dashRemaining = maxDashes;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashRemaining > 0 && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }


    IEnumerator Dash()
    {
        isDashing = true;

        dashRemaining--;

        // Stop normal movement
        player.canMove = false;

        // Save gravity
        float originalGravity = rb.gravityScale;

        // Disable gravity
        rb.gravityScale = 0;


        // Get facing direction
        float direction = player.IsFacingRight ? 1f : -1f;


        // Play dash effect
        if (dashEffect != null)
        {
            dashEffect.Play();
        }


        // Dash movement
        rb.linearVelocity = new Vector2(direction * dashSpeed, 0);


        yield return new WaitForSeconds(dashDuration);


        // Stop dash
        rb.linearVelocity = Vector2.zero;


        // Restore gravity
        rb.gravityScale = originalGravity;


        // Allow movement again
        player.canMove = true;

        isDashing = false;
    }


    public void ResetDash()
    {
        dashRemaining = maxDashes;
    }
}