using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool initialVisibility = true;
    public GameObject inventory;
    void Start()
    {
        initialVisibility = gameObject.activeSelf;  // Store the initial visibility state
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            initialVisibility = !initialVisibility;  // Toggle the stored visibility state
            inventory.SetActive(initialVisibility);  // Set the visibility based on the stored state
        }


    }
}
