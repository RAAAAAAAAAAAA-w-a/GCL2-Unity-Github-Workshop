using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Debug.Log("BACK TO MAIN MENU CLICKED");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Opening Scene");
    }

    public void HighScores()
    {
        SceneManager.LoadScene("High Scores");
    }

}
