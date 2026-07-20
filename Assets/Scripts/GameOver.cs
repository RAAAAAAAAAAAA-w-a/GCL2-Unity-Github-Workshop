using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI finalScoreText;

    public void scoreResults()
    {
        finalScoreText.text = "FINAL SCORE: " + scoreManager.instance.CurrentScore().ToString("000000");
        highScoreText.text = "YOUR HIGH SCORE: " + PlayerPrefs.GetInt("TopScore", 0).ToString("000000");
    }


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
