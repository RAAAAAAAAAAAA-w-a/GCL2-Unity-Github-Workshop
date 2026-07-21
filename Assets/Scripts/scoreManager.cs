using TMPro;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI topScoreText;

    [Header("Scoring")]
    int score = 0;
    int topScore = 0;


    void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        }
    }

    void Start()
    {
        topScore = PlayerPrefs.GetInt("TopScore", 0);

        scoreText.text = score.ToString("000000");
        topScoreText.text = "TOP-" + topScore.ToString("000000");
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString("000000");

        if (score > topScore)
        {
            topScore = score;
            topScoreText.text = "TOP-" + topScore.ToString("000000");

            PlayerPrefs.SetInt("TopScore", topScore);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        if (topScore < score)
        {
            topScore = score;
            topScoreText.text = "TOP-" + topScore.ToString("000000");

            PlayerPrefs.SetInt("TopScore", topScore);
            PlayerPrefs.Save();
        }
    }

    public int CurrentScore()
    {
        return score;
    }
}