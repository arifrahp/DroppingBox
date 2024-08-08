using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has a FallingObject component
        FallingObject fallingObject = other.GetComponent<FallingObject>();
        if (fallingObject != null)
        {
            Destroy(other.gameObject);
        }
    }
}
