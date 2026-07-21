using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //to display player high scores and final scores
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI finalScoreText;

    //updates game over screen with scores
    public void scoreResults()
    {
        finalScoreText.text = "FINAL SCORE: " + scoreManager.instance.CurrentScore().ToString("000000");
        highScoreText.text = "YOUR HIGH SCORE: " + PlayerPrefs.GetInt("TopScore", 0).ToString("000000");
    }

    //returns player back to main menu
    public void BackToMainMenu()
    {
        Debug.Log("BACK TO MAIN MENU CLICKED");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    //restarts the game
    public void Retry()
    {
        SceneManager.LoadScene("Opening Scene");
    }

    //open high scores scene
    public void HighScores()
    {
        SceneManager.LoadScene("High Scores");
    }

}
