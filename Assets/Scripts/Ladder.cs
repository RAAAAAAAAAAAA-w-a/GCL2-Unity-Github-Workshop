using UnityEngine;

public class Ladder : MonoBehaviour
{
    [Header("Platform Above Ladder")]
    [SerializeField] private Collider2D platformCollider;

    private Collider2D playerCollider;
    private bool ignoringPlatform = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
            return;

        playerCollider = other.GetComponent<Collider2D>();

        // Drop down through platform
        if (player.isGrounded && Input.GetAxisRaw("Vertical") < 0)
        {
            IgnorePlatform();
        }

        // Climb up through platform
        if (!player.isGrounded && player.canClimb && player.climbInput > 0)
        {
            IgnorePlatform();
        }

        // Turn collision back on once player is above platform
        if (ignoringPlatform &&
            player.transform.position.y > platformCollider.bounds.max.y)
        {
            EnablePlatform();
        }
    }

    private void IgnorePlatform()
    {
        if (!ignoringPlatform &&
            playerCollider != null &&
            platformCollider != null)
        {
            Physics2D.IgnoreCollision(
                playerCollider,
                platformCollider,
                true
            );

            ignoringPlatform = true;
        }
    }

    private void EnablePlatform()
    {
        if (playerCollider != null &&
            platformCollider != null)
        {
            Physics2D.IgnoreCollision(
                playerCollider,
                platformCollider,
                false
            );

            ignoringPlatform = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Collider2D exitingPlayer = other.GetComponent<Collider2D>();

        if (exitingPlayer != null && platformCollider != null)
        {
            Physics2D.IgnoreCollision(
                exitingPlayer,
                platformCollider,
                false
            );
        }

        ignoringPlatform = false;
        playerCollider = null;
    }
}
