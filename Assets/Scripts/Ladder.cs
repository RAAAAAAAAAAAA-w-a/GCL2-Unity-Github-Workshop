using UnityEngine;

public class Ladder : MonoBehaviour
{
    [Header("Climb Settings")]
    public float climbSpeed = 5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
        Collider2D playerCollider = other.GetComponent<Collider2D>();
        Animator playerAnim = other.GetComponent<Animator>();

        if (playerRb == null || playerCollider == null) return;

        float verticalInput = Input.GetAxisRaw("Vertical");

        // 1. Moving Up/Down on the ladder
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            playerRb.gravityScale = 0f; // Turn off gravity while climbing
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, verticalInput * climbSpeed);

            if (playerAnim != null)
            {
                // Force-suppress running/walking states from PlayerController
                playerAnim.SetFloat("speed", 0f);
                playerAnim.SetBool("isRunning", false);
                playerAnim.SetBool("isWalking", false);

                // Enable climb animation and ensure playback speed is normal
                playerAnim.SetBool("isClimbing", true);
                playerAnim.speed = 1f;
            }
        }
        // 2. Hanging stationary on the ladder (not pressing W/S or Up/Down)
        else if (playerRb.gravityScale == 0f)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);

            if (playerAnim != null)
            {
                // Keep movement parameters suppressed
                playerAnim.SetFloat("speed", 0f);
                playerAnim.SetBool("isRunning", false);
                playerAnim.SetBool("isWalking", false);

                // Hold climbing pose and pause animation frame while still
                playerAnim.SetBool("isClimbing", true);
                playerAnim.speed = 0f;
            }
        }

        // 3. Dynamic Platform Collision Bypass
        Collider2D[] nearbyColliders = Physics2D.OverlapBoxAll(playerCollider.bounds.center, playerCollider.bounds.size, 0f);

        foreach (Collider2D plat in nearbyColliders)
        {
            if (plat.CompareTag("Platform"))
            {
                // Ignore platform collision while moving up or below top edge
                if (verticalInput > 0 || other.transform.position.y < plat.bounds.max.y)
                {
                    Physics2D.IgnoreCollision(playerCollider, plat, true);
                }
                // Solidify floor once Mario's feet are above the top edge
                else if (other.transform.position.y >= plat.bounds.max.y)
                {
                    Physics2D.IgnoreCollision(playerCollider, plat, false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
        Collider2D playerCollider = other.GetComponent<Collider2D>();
        Animator playerAnim = other.GetComponent<Animator>();

        // Restore normal gravity scale
        if (playerRb != null)
        {
            playerRb.gravityScale = 1f;
        }

        // Reset animator back to default locomotion state
        if (playerAnim != null)
        {
            playerAnim.SetBool("isClimbing", false);
            playerAnim.speed = 1f; // Restore default playback speed
        }

        // Re-enable collision with all platforms upon exiting ladder area
        if (playerCollider != null)
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject p in platforms)
            {
                Collider2D pCol = p.GetComponent<Collider2D>();
                if (pCol != null)
                {
                    Physics2D.IgnoreCollision(playerCollider, pCol, false);
                }
            }
        }
    }
}