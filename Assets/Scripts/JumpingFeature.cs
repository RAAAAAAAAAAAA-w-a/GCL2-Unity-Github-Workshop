using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpingFeature : MonoBehaviour
{
<<<<<<< Updated upstream
    private Animator anim;
    private PlayerController player;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Sprite JumpFeature01;
    public Sprite JumpFeature02;
=======
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private Sprite newSprite;

    // Drag the upper platform's BoxCollider2D into this field in Unity
    [SerializeField] private Collider2D upperPlatform;

    [SerializeField] private float ignoreCollisionTime = 1f;
>>>>>>> Stashed changes

    void Start()
    {
        //player.jumpFeature()
        GetComponent<SpriteRenderer>().sprite = JumpFeature01;
    }

    void jumpFeature()
    {
        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce * player.jumpBoost);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JumpFeature"))
        {
<<<<<<< Updated upstream
            print("jumping");
           
            jumpFeature();

        }
    }

    public class Jump
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

=======
            // Give the player an upward jump boost
            playerRb.linearVelocity = new Vector2(
                playerRb.linearVelocity.x,
                jumpForce
            );

            // Change the jumping machine's sprite
            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }

            // Get the player's collider
            Collider2D playerCollider = other.GetComponent<Collider2D>();

            // Let the player pass through the upper platform temporarily
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
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
        {
<<<<<<< Updated upstream
        
        }
}
=======

        }



    
}
>>>>>>> Stashed changes
