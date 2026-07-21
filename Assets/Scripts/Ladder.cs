using UnityEngine;

public class Ladder : MonoBehaviour
{
    [Header("Platform Above Ladder")]
    [SerializeField] private Collider2D platformCollider;

    private Collider2D playerCollider;
    private bool ignorePlatform = false;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
            return;

        playerCollider = other.GetComponent<Collider2D>();


        // Player is standing on the platform and presses DOWN
        if (player.isGrounded && Input.GetAxisRaw("Vertical") < 0)
        {
            IgnorePlatform();
        }


        // Player is climbing UP and needs to pass through the platform
        if (!player.isGrounded && player.canClimb && player.climbInput > 0)
        {
            IgnorePlatform();
        }
    }


    private void IgnorePlatform()
    {
        if (!ignorePlatform && playerCollider != null && platformCollider != null)
        {
            Physics2D.IgnoreCollision(
                playerCollider,
                platformCollider,
                true
            );

            ignorePlatform = true;
        }
    }
}
