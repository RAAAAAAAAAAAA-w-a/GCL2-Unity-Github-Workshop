using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    private hammerPowerup hammer;

    private float currentHorizontalSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Starts the barrel rolling right
        currentHorizontalSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = currentHorizontalSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detects initial landing/hits
        HandleDirectionChange(collision.gameObject);

        // Player death check
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager manager = FindAnyObjectByType<LevelManager>();
            if (manager != null && !hammer.isHammerActive)
            {
                // Calls your manager's death function if it exists
                manager.PlayerDied();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Fallback: Keeps pushing the correct way if it hesitates on edges
        HandleDirectionChange(collision.gameObject);
    }

    // Helper method to keep the code clean and stop repetition
    private void HandleDirectionChange(GameObject platform)
    {
        if (platform.CompareTag("GoLeft"))
        {
            currentHorizontalSpeed = -moveSpeed;
        }
        else if (platform.CompareTag("GoRight"))
        {
            currentHorizontalSpeed = moveSpeed;
        }
    }
}