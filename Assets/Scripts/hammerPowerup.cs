using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class hammerPowerup : MonoBehaviour
{
    public float seconds = 5.0f;
    public bool isHammerActive;

    private IEnumerator activationTime()
    {
        isHammerActive = true;
        float timer = seconds;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        isHammerActive = false;
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
}
