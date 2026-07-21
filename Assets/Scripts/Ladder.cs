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

        if (playerRb == null || playerCollider == null) return;

        float verticalInput = Input.GetAxisRaw("Vertical");

        //start climbing when W/S or Up/Down arrows are pressed
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            playerRb.gravityScale = 0f; // Turn off gravity while climbing
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, verticalInput * climbSpeed);
        }

        // automatically find any platform Mario is touching and bypass collision
        Collider2D[] nearbyColliders = Physics2D.OverlapBoxAll(playerCollider.bounds.center, playerCollider.bounds.size, 0f);

        foreach (Collider2D plat in nearbyColliders)
        {
            if (plat.CompareTag("Platform"))
            {
                //if climbing up or below the top of the platform, ignore collision
                if (verticalInput > 0 || other.transform.position.y < plat.bounds.max.y)
                {
                    Physics2D.IgnoreCollision(playerCollider, plat, true);
                }
                //once Mario's feet reach above the top edge, re-enable collision so he can stand on it
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

        // Reset gravity when stepping off the ladder
        if (playerRb != null)
        {
            playerRb.gravityScale = 1f;
        }

        // Re-enable collision with all platforms when exiting the ladder zone
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