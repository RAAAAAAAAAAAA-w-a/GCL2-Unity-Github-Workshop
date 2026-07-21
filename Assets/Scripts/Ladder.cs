using UnityEngine;

public class Ladder : MonoBehaviour
{
    [Header("Phasing Through Platform")]
    [SerializeField] private Collider2D platformCollider;

    private bool playerIgnoringPlatform = false;
    private PlayerController player;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
            return;

        Collider2D playerCollider = other.GetComponent<Collider2D>();
        bool pressDown = Input.GetAxisRaw("Vertical") < 0;

        bool falling = !player.isGrounded && !player.isJumping;


        if ((pressDown && player.isGrounded) || falling)
        {
            if (!playerIgnoringPlatform)
            {
                Physics2D.IgnoreCollision(playerCollider,platformCollider,true);

                playerIgnoringPlatform = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Collider2D playerCollider = other.GetComponent<Collider2D>();

        if (playerCollider != null && platformCollider != null)
        {
            Physics2D.IgnoreCollision(
                playerCollider,
                platformCollider,
                false
            );

            playerIgnoringPlatform = false;
        }
    }
}
