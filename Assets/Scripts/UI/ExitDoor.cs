using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{

    public GameObject indicator;


    void Update()
    {
        CheckForCollision();
    }
    private void CheckForCollision()
    {
        // Check for collisions with the player
        Collider collider = GetComponent<Collider>();
        Collider[] colliders = Physics.OverlapBox(transform.position, collider.bounds.extents, transform.rotation);

        bool foundPlayer = false;

        foreach (var col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                foundPlayer = true;
                break;
            }
        }

        // Set indicator visibility based on collision with the player
        indicator.SetActive(foundPlayer);
    }
}
