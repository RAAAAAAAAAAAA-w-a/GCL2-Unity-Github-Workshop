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
        scoreText.text = score.ToString("000000");
        topScoreText.text = "TOP-" + topScore.ToString("000000");
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString("000000");

    }

    void Update()
    {
        if (topScore < score)
        {
            topScore = score;
            PlayerPrefs.SetInt("TopScore", topScore);
            PlayerPrefs.Save();
        }
    }

    public int CurrentScore()
    {
        return score;
    }
}