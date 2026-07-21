using TMPro;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public static scoreManager instance; //makes it easier to reference this code from other files

    [Header("UI References")] // references the UI text on the canvas screen to display the scores
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI topScoreText;

    [Header("Scoring")] // stores the player's scores
    int score = 0; // score that player just got
    int topScore = 0; // highest score the player ever got


    void Awake() // to ensure an instance is made before the game starts
    {
        if (instance == null)
        { 
            instance = this;
        }
    }

    void Start()
    {
        topScore = PlayerPrefs.GetInt("TopScore", 0); //base score when a score has not been made yet

        scoreText.text = score.ToString("000000"); // base score of how it will look in text
        topScoreText.text = "TOP-" + topScore.ToString("000000");
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString("000000");

        if (score > topScore) // if higher score is lower than the score as of now, change highe score to = score for new high score
        {
            topScore = score;
            topScoreText.text = "TOP-" + topScore.ToString("000000");

            PlayerPrefs.SetInt("TopScore", topScore); // to save the new high score pernamently
            PlayerPrefs.Save();
        }
    }

    void Update() // making sure the high score will update in real time
    {
        if (topScore < score)
        {
            topScore = score; // update scoe
            topScoreText.text = "TOP-" + topScore.ToString("000000"); // update UI high score

            PlayerPrefs.SetInt("TopScore", topScore); // save the new high score
            PlayerPrefs.Save();
        }
    }

    public int CurrentScore() // for easier excess to the score information
    {
        return score;
    }
}