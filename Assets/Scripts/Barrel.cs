using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrel : MonoBehaviour
{

    protected Rigidbody2D rb;
    public float speed = 3f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Push barrels in the direction the platform is facing.
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            BarrelPath path = collision.gameObject.GetComponent<BarrelPath>();

            if (path != null)
            {
                Vector2 direction = path.moveRight ? Vector2.right : Vector2.left;
                rb.AddForce(direction * speed, ForceMode2D.Impulse);
            }
            return;
        }

        // Restart the scene if the player is hit.
        if (collision.gameObject.TryGetComponent(out PlayerController p))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}