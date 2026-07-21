using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // the scene to load
    [Header("Scene Configuration")]
    [SerializeField] private string targetSceneName;

    // time takes for the scene to load
    [SerializeField] private float delayInSeconds = 3f;

    void Start()
    {
        // start the countdown timer automatically when the scene loads
        StartCoroutine(WaitAndChangeScene());
    }

    IEnumerator WaitAndChangeScene()
    {
        // wait for the number of seconds
        yield return new WaitForSeconds(delayInSeconds);

        // load the scene
        SceneManager.LoadScene(targetSceneName);
    }
}