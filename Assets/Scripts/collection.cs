using UnityEngine;

public class collection : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other) //to collect object and back the object disappear afterwards
  { 
     if (other.CompareTag("Player"))
     {
            Destroy(this.gameObject);
     }
  }
}