﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour
{
    Player playerController;
    public Land selectedLand = null;
    public ItemData selectedTool;
    public AnimaationsPlayer player;
    public Player player1;
    UImanager manager;
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    private GameObject wateringCan;
    [SerializeField]
    WateringAnim wateringAnim;
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
        if (selectedLand != null)
        {
            if (selectedLand.isCropInstantianted == true && selectedTool == null)
            {
                Destroy(selectedLand.GrownCrop);
                ItemData crop = selectedLand.crop;
                inventory.HarvestCrops(crop);
                selectedLand.HarvestSeed();
            }
            if (selectedTool != null)
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
                if (selectedTool.name == "Axe")
                {

                }

                if (selectedTool is SeedsData)
                {
                   
                    if (selectedLand.landStatus == Land.LandStatus.Farmland && !selectedLand.HasSeedPlanted())
                    {
                        (selectedTool as ItemData).quantity--;
                        if ((selectedTool as ItemData).quantity <= 0)
                        {
                            for (int i = 0; i < Inventory.Instance.hotbarItems.Length; i++)
                            {
                                if (Inventory.Instance.hotbarItems[i] != null && Inventory.Instance.hotbarItems[i].name == selectedTool.name)
                                {
                                    Inventory.Instance.hotbarItems[i].quantity = 1;
                                    Inventory.Instance.hotbarItems[i] = null;
                                }
                            }
                        }
                        
                    }
                    
                    UImanager.Instance.RenderHotbar();
                    InstantiateSeed(selectedTool as SeedsData);
                }
                
                return;
            }
        }

        else
        {
            Debug.Log("No land selected or no tool selected.");
        }
    }
    public void InteractWithFood()
    {
        if (selectedTool != null && selectedTool is FoodData)
        {
            player1.fillAmount += (float)(selectedTool as FoodData).energyFillAmount / 100;
            (selectedTool as ItemData).quantity--;
            if ((selectedTool as ItemData).quantity <= 0)
            {
                for (int i = 0; i < Inventory.Instance.hotbarItems.Length; i++)
                {
                    if (Inventory.Instance.hotbarItems[i] != null && Inventory.Instance.hotbarItems[i].name == selectedTool.name)
                    {
                        Inventory.Instance.hotbarItems[i].quantity = 1;
                        Inventory.Instance.hotbarItems[i] = null;
                    }
                }
            }
            UImanager.Instance.RenderHotbar();
        }
    }
    IEnumerator StopMovement(float num)
    {
        player1.eneableMovememt = false;
        yield return new WaitForSeconds(num);
        player1.eneableMovememt = true;
    }
    private void InstantiateSeed(SeedsData seedData)
    {
        if (selectedLand.landStatus == Land.LandStatus.Farmland)
        {
            
            if (!selectedLand.HasSeedPlanted())  // Check if a seed is already planted
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
                selectedLand.seed = Instantiate(seedData.gameModel, landPosition, desiredRotation);
                selectedLand.seed1 = seedData.seedling1;
                selectedLand.seed2 = seedData.seedling2;
                selectedLand.GrownCrop = seedData.cropToYield.gameModel;
                selectedLand.DaysToGrowPorgression = seedData.daysToGrow;
                selectedLand.crop = seedData.cropToYield;
                selectedLand.PlantSeed();
            
            }
               
        }
        
    
    }



}

