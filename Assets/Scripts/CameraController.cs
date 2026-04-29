using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float followAhead;
    private Vector3 targetPosition;
    public float smoothing;
    public bool followTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        followTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)

        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            if (target.transform.localScale.x > 0.0f)
            {
                targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
            }
            else
            {
                targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
            }

            //transform.position = targetPosition;

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
