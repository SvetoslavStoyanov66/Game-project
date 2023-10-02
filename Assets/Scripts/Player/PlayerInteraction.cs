using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInteraction : MonoBehaviour
{
    Player playerController;
    public Land selectedLand = null;
    public ItemData selectedTool;
    public AnimaationsPlayer player;
    public Player player1;
    
    UImanager manager;
    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.GetComponent<Player>();
        manager = UImanager.Instance;
    }
    public void UpdateSelectedTool()
    {
        selectedTool = manager.GetSelectedHotbarItem();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
        UpdateSelectedTool();

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
        if (selectedLand != null && selectedTool != null)
        {
            selectedLand.Interact(selectedTool.name);
            if (selectedTool.name == "Hoe")
            {
                player.HoeUsageAnimations();
                StartCoroutine(StopMovement(1.5f));
            }
            if (selectedTool.name == "Wateringcan")
            {
                player.Watering();
                StartCoroutine(StopMovement(2.5f));
            }
            
           return;
        }

        else
        {
            Debug.Log("No land selected or no tool selected.");
        }
    }
    IEnumerator StopMovement(float num)
    {
        player1.moveSpeed = 0;
        yield return new WaitForSeconds(num);
        player1.moveSpeed = 5;
    }
}
