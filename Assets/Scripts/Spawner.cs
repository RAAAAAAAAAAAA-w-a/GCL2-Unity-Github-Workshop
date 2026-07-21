using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float minTime = 8f;
    public float maxTime = 10f;

 

    private void Start()
    {
        //spawn barrels
        Spawn();
    }

    private void Spawn()
    {
     
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }

        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
}