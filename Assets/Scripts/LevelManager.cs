using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public float waitToRespawn;
    public PlayerController thePlayer;

    public GameObject deathSplosion;

    public int healthCount;
    public int maxHealth;

    public GameObject gameOverScreen;
    private bool respawning;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;
    

    public void UpdateHeartMeter()
    {
        switch (healthCount)
        {
            case 600:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;
            case 500:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;
            case 400:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;
            case 300:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;
            case 200:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 100:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }

    public void AddHealth (int healthToAdd)
    {
        healthCount += healthToAdd;

        if(healthCount > maxHealth)
        {
            healthCount = Mathf.Clamp(healthCount, 0, maxHealth);
        }
        UpdateHeartMeter();
    }
    
    public void HurtPlayer(int damageToTake)
    {
        healthCount -= damageToTake;
        healthCount = Mathf.Clamp(healthCount, 0, maxHealth);

        UpdateHeartMeter();
        thePlayer.Knockback();

        if (healthCount > 0 && !respawning)
        {
            respawning = true;
            StartCoroutine(RespawnCo());
        }
        else if (healthCount <= 0)
        {
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }

    public void Respawn()
    {

        if (healthCount > 0)
        {
            StartCoroutine("RespawnCo");
           
        }
        else
        {
            Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.canMove = false;
        thePlayer.GetComponent<Animator>().SetTrigger("Die");

        
        Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSecondsRealtime(waitToRespawn);

        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.canMove = true;

        //healthCount = maxHealth;
        respawning = false;
        UpdateHeartMeter();

    }

  



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thePlayer = FindFirstObjectByType<PlayerController>();

        healthCount = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //if (healthCount <= 0 && !respawning)
       // {
           // Respawn();
         //   respawning = true;
      //  }
    }
}
