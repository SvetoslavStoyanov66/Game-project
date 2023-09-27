using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Player playerController;
    Land selectedLand = null;
    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down,out hit,1))
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
    public void Interact()
    {
        if (selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }
        else
        {
            Debug.Log("not");
        }
    }
}
