using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpingFeature : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private Sprite newSprite;

    
    [SerializeField] private Collider2D upperPlatform;

    [SerializeField] private float ignoreCollisionTime = 1f;

    private SpriteRenderer spriteRenderer;
    private Collider2D platformCollider;

    private bool hasBeenUsed = false;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || hasBeenUsed)
            return;

        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            //gives the player an upward jump boost
            playerRb.linearVelocity = new Vector2(
                playerRb.linearVelocity.x,
                jumpForce
            );

            //changes the jumping machine's sprite
            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }

            
            Collider2D playerCollider = other.GetComponent<Collider2D>();

            //allows the player to pass through the upper platform temporarily
            if (playerCollider != null && upperPlatform != null)
            {
                Physics2D.IgnoreCollision(
                    playerCollider,
                    upperPlatform,
                    true
                );

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
            Physics2D.IgnoreCollision(
                playerCollider,
                upperPlatform,
                false
            );
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}