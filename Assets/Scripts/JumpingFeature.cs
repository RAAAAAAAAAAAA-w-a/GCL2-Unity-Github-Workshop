using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingFeature : MonoBehaviour
{
    [Header("Jump Force Override")]
    [Tooltip("Target launch force - tuned down for lower jump height")]
    public float jumpForce = 3.5f; // Set much lower (3.5f)

    [SerializeField] private Sprite newSprite;
    [SerializeField] private Collider2D upperPlatform;
    [SerializeField] private float ignoreCollisionTime = 1f;

    private SpriteRenderer spriteRenderer;
    private bool hasBeenUsed = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || hasBeenUsed)
            return;

        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            // Reset existing vertical velocity first so forces don't stack
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);

            // Apply a controlled launch upward
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);

            if (newSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }

            Collider2D playerCollider = other.GetComponent<Collider2D>();

            if (playerCollider != null && upperPlatform != null)
            {
                Physics2D.IgnoreCollision(playerCollider, upperPlatform, true);
                StartCoroutine(ReEnableCollision(playerCollider));
            }

            hasBeenUsed = true;
        }
    }

    private IEnumerator ReEnableCollision(Collider2D playerCollider)
    {
        yield return new WaitForSeconds(ignoreCollisionTime);

        if (playerCollider != null && upperPlatform != null)
        {
            Physics2D.IgnoreCollision(playerCollider, upperPlatform, false);
        }
    }
}