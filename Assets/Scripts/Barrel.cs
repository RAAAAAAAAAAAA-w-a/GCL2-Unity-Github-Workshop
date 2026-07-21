using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrel : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1f;

    private LevelManager levelManager;
    private hammerPowerup hammerTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        levelManager = FindFirstObjectByType<LevelManager>();
        hammerTime = FindFirstObjectByType<hammerPowerup>(); // finding the scripts to refer to immediately befoer game starts
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.AddForce(collision.transform.right * speed, ForceMode2D.Impulse); //ensures that a collision is made to the ground and gets pushed forward
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // if hammer time is not active and comes into contact with player, kill player
            if (hammerTime == null || !hammerTime.isHammerActive)
            {
                if (levelManager != null)
                {
                    levelManager.PlayerDied(); // game resets 
                    print("died");
                }
            }
        }
    }
}
