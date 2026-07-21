using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class hammerPowerup : MonoBehaviour
{
    public float seconds = 5.0f;
    public bool isHammerActive;

    private Animator animator;
    private PlayerController playerCtrl;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerCtrl = GetComponent<PlayerController>();
    }

    private IEnumerator activationTime()
    {
        isHammerActive = true;
        float timer = seconds;

        // Animator to switch to the hammer walk animation
        if (animator != null)
        {
            animator.SetBool("isHammerActive", true);
        }

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        isHammerActive = false;


        // Animator to go back to the normal walk animation
        if (animator != null)
        {
            animator.SetBool("isHammerActive", false);
        }
        print("hammertime ended :)");
    }

    public void hammerTime()
    {
        if (!isHammerActive)
        {
            StartCoroutine(activationTime());
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hammer"))
        {
            print("Hammer time triggered");
            hammerTime();
        }
        else if (other.CompareTag("Enemy") && isHammerActive)
        {
            print("murder");
            Destroy(other.gameObject);
            scoreManager.instance.AddPoints(500);
        }
    }
             private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isHammerActive)
        {
            print("Smashed into solid enemy");
            Destroy(collision.gameObject);
            scoreManager.instance.AddPoints(500);
        }


    }
}
