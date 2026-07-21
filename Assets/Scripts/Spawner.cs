using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float minTime = 5f;
    public float maxTime = 8f;

    public int maxBarrels = 5;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        // Count all barrels currently in the scene
        if (GameObject.FindGameObjectsWithTag("Barrel").Length < maxBarrels)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }

        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
}