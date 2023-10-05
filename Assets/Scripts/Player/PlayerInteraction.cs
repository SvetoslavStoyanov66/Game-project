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
                selectedLand.wasWateredYesterday = true;
            }
            if (selectedTool is SeedsData)
            {
                InstantiateSeed(selectedTool as SeedsData);
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
    private void InstantiateSeed(SeedsData seedData)
    {
        if (selectedLand.landStatus == Land.LandStatus.Farmland)
        {
            Vector3 landPosition = selectedLand.transform.position;

            // Set the desired Y-coordinate
            landPosition.y = 0f;
            if (selectedTool.name == "Potato Seed")
            {
                landPosition.z += -.2f;
            }
           

            // Set the desired rotation (X: -90 degrees)
            Quaternion desiredRotation = Quaternion.Euler(-90f, 0f, 0f);

            // Instantiate the seed prefab with the modified position and rotation
            GameObject seedInstance = Instantiate(seedData.gameModel, landPosition, desiredRotation);

        }
    
    }
    private void Grow()
    {

    }

    // Existing code...
}

