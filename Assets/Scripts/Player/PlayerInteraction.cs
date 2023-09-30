using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Player playerController;
    Land selectedLand = null;
    public string selectedToolName;
    UImanager manager;
    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
    }

    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        if (other.tag == "Land")
        {
            Land land = other.GetComponent<Land>();
            land.Selected(true);
            SelectedLand(land);
            return;
        }
        if (selectedLand != null)
        {
            selectedLand.Selected(false);
            selectedLand = null;
        }
    }
    void SelectedLand(Land land)
    {
        if (selectedLand != null)
        {
            selectedLand.Selected(false);
        }
        selectedLand = land;
        land.Selected(true);
    }

    
    public void InteractWithLand()
    {
        if (selectedLand != null)
        {
            selectedLand.Interact("Watercan");  // Pass the selected tool name
            return;
        }
   
        else
        {
            Debug.Log("No land selected or no tool selected.");
        }
    }
}
