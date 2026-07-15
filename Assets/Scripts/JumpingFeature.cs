using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class JumpingFeature : MonoBehaviour
{
    private Animator anim;
    private PlayerController player;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Sprite JumpFeature01;
    public Sprite JumpFeature02;

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

    }

    // Update is called once per frame
    void Update()
        {
        
        }
}
