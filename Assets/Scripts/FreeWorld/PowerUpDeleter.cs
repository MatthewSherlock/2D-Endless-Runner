using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDeleter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.transform.tag == "SpawnedObject")
            Destroy(collision.gameObject);
        
    }
}
