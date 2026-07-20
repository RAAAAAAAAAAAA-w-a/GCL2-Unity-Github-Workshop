using UnityEngine;

public class Princess : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the object touching the princess is tagged "Player"
        if (other.CompareTag("Player"))
        {
            LevelManager manager = FindAnyObjectByType<LevelManager>();
            if (manager != null)
            {
                manager.PlayerWon();
            }
        }
    }
}