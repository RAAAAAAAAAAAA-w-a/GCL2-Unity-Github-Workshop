using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int startingScore = 0;   // <-- Activity 2 script conflict target
    public int lives = 1;           // <-- Activity 3 merge conflict target
    public float gameSpeed = 1.0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        startingScore += points;
        Debug.Log("Score: " + startingScore);
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Life lost! " + lives + " left.");

        if (lives <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}