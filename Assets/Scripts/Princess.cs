using UnityEngine;

public class Princess : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //player touches princess
        Debug.Log("Princess was touched by: " + other.gameObject.name + " with tag: " + other.tag);

        if (other.CompareTag("Player"))
        {
            LevelManager manager = FindAnyObjectByType<LevelManager>();
            if (manager != null)
            {
                manager.PlayerWon();
            }
            else
            {
                Debug.LogError("Could not find LevelManager in the scene!");
            }
        }
    }
}