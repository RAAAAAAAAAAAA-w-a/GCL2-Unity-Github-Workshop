using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class JumpingFeature : MonoBehaviour
{
    public float jumpForce = 15f;
    public Sprite newSprite;

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

            //changes the platform's sprite
            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }

            //to disable the platform collider
            platformCollider.enabled = false;

            hasBeenUsed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}