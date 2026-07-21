using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneChanger : MonoBehaviour
{
     [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string targetSceneName;

    void OnEnable()
    {
        // Subscribe to the video finished event
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }

    void OnVideoFinished(VideoPlayer source)
    {
        // Load the new scene automatically
        SceneManager.LoadScene(targetSceneName);
    }
}