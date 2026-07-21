using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // The scene to load
    [Header("Scene Configuration")]
    [SerializeField] private string targetSceneName;

    // Time takes for the scene to load
    [SerializeField] private float delayInSeconds = 3f;

    void Start()
    {
        // Start the countdown timer automatically when the scene loads
        StartCoroutine(WaitAndChangeScene());
    }

    IEnumerator WaitAndChangeScene()
    {
        // Wait for the number of seconds
        yield return new WaitForSeconds(delayInSeconds);

        // Load the scene
        SceneManager.LoadScene(targetSceneName);
    }
}