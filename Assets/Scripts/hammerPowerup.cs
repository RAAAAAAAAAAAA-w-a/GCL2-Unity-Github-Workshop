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

    //activates hammer power up for a certain duration
    private IEnumerator activationTime()
    {
        isHammerActive = true;
        float timer = seconds;

        // Animator to switch to the hammer walk animation
        if (animator != null)
        {
            animator.SetBool("isHammerActive", true);
        }

        // countdown till power up expire
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        //deactivate power up
        isHammerActive = false;


        // animator to go back to the normal walk animation
        if (animator != null)
        {
            animator.SetBool("isHammerActive", false);
        }
        print("hammertime ended :)");
    }

    //starts hammer if it isnt active
    public void hammerTime()
    {
        if (!isHammerActive)
        {
            StartCoroutine(activationTime());
        }

    }

    //detects hammer pickups and destroy barrels
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hammer"))
        {
            hammerTime();
        }
        else if (other.CompareTag("Barrel") && isHammerActive)
        {
            Destroy(other.gameObject);
            scoreManager.instance.AddPoints(500);
        }
    }

    //destroys barrels on collision while hammer active
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel") && isHammerActive)
        {
            Destroy(collision.gameObject);
            scoreManager.instance.AddPoints(500);
        }
    }
}
