using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Game States")]
    public bool isGameActive = false;

    [Header("Player Reference")]
    public MonoBehaviour playerMovementScript;

    [Header("Barrel Spawning")]
    public GameObject barrelPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f;

    void Start()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }
        StartCoroutine(StartLevelCountdown());
    }

    IEnumerator StartLevelCountdown()
    {
        Debug.Log("Level Loaded. Countdown started...");
        yield return new WaitForSeconds(3f);

        Debug.Log("3 seconds up! Mario can now move.");
        isGameActive = true;

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }

        StartCoroutine(SpawnBarrelRoutine());
    }

    IEnumerator SpawnBarrelRoutine()
    {
        while (isGameActive)
        {
            SpawnBarrel();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBarrel()
    {
        if (barrelPrefab != null && spawnPoint != null)
        {
            // Spawn the barrel
            GameObject newBarrel = Instantiate(barrelPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Mario hit an obstacle! Reloading level...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerWon()
    {
        Debug.Log("Mario saved the Princess! Loading Win Screen...");
        SceneManager.LoadScene("WinScreen");
    }
}