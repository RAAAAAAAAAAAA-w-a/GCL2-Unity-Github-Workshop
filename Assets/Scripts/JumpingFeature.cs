using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class JumpingFeature : MonoBehaviour
{
    public float jumpForce = 15f;
    public Sprite newSprite;

    private SpriteRenderer spriteRenderer;
    private bool hasBeenUsed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenUsed)
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // gives the player an upward jump boost
                playerRb.linearVelocity = new Vector2(
                    playerRb.linearVelocity.x,
                    jumpForce
                );

                // replace the current sprite
                if (newSprite != null)
                {
                    spriteRenderer.sprite = newSprite;
                }

                hasBeenUsed = true;
            }
        }
    }

}

    /*public class Jump
    {

        protected Collider2D collider;

        protected override void Start()
        {
            base.Start();
            collider = GetComponent<Collider2D>();
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
                return;
            collider.isTrigger = true;
            rb.linearVelocity = Vector2.zero;
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
                return;
            collider.isTrigger = false;
        }

    }

    // Update is called once per frame
    void Update()
        {
        
        }
}
   */