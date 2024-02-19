﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject cowShedBuilding;
    [SerializeField]
    GameObject coopBuilding;
    [SerializeField]
    GameObject chickenBrown;
    [SerializeField]
    GameObject chickenWhite;
    [SerializeField]
    public Sprite[] sprites;
    private int currentIndex = 0; 
    [SerializeField]
    public Image spriteDisplay;
    [SerializeField]
    Sprite spriteForTown;
    [SerializeField]
    GameObject prefabCow;
    void Start()
    {
        if(SaveSystem.isSafeSlotEmpty(SaveSystem.slot))
        {
            spriteDisplay.gameObject.SetActive(true);
            spriteDisplay.sprite = sprites[0];
        }
        else 
        {
            Destroy(spriteDisplay.gameObject);
        }
        LoadGame();
    }
     void Update()
    {
        if (Input.anyKeyDown && spriteDisplay!=null && spriteDisplay.sprite != spriteForTown)
        {
            ChangeSprite();
        }
        else if(Input.anyKeyDown  && spriteDisplay!=null)
        {
            Destroy(spriteDisplay.gameObject);
        }
    }
    public void EneableTownTurtorial()
    {
        spriteDisplay.gameObject.SetActive(true);
        spriteDisplay.sprite = spriteForTown;
        spriteDisplay.rectTransform.sizeDelta = new Vector2(1500,750);
    }
    void ChangeSprite()
    {
        currentIndex++;

        if (currentIndex < sprites.Length)
        {
            spriteDisplay.sprite = sprites[currentIndex];
        }
        else
        {
            spriteDisplay.gameObject.SetActive(false);
        }
    }
    public List<ItemData> allItemData;
    private GameObject ch;

    public ItemData GetItemDataByName(string name)
    {
        return allItemData.Find(item => item.name == name);
    }
    public void LoadGame()
    {
        SaveData saveData = SaveSystem.LoadGame();
        if (saveData == null)
        {
            return;
        }
         if (saveData.buildingsSaveData != null && saveData.buildingsSaveData.Count > 0)
        {

                BuildingSaveData item = saveData.buildingsSaveData.Find(x => x.buildingName == "coop");
                BuildingSaveData item2 = saveData.buildingsSaveData.Find(x => x.buildingName == "cowShed");

                if(item != null && item.buildingName == "coop")
                {
                    BuildingManager.Instance.coopActive = true;
                    Vector3 position = new Vector3(item.positionX,item.positionY,item.positionZ);
                    BuildingManager.Instance.coopVector = position;
                    Quaternion quaternion = Quaternion.Euler(-90,0,-90);
                    coopBuilding = Instantiate(coopBuilding,position,quaternion);
                    BuildingManager.Instance.SaveBuildingDoor(coopBuilding);
                }
                if(item2 != null && item2.buildingName == "cowShed")
                {
                    BuildingManager.Instance.cowBuildingActive = true;
                    Vector3 position = new Vector3(item2.positionX,item2.positionY,item2.positionZ);
                    BuildingManager.Instance.cowShedVector = position;
                    Quaternion quaternion = Quaternion.Euler(0,-90,0);
                    cowShedBuilding = Instantiate(cowShedBuilding,position,quaternion);
                    BuildingManager.Instance.SaveBuildingDoor(cowShedBuilding);
                }
        }


        if (saveData.hotbarItems != null && saveData.hotbarItems.Count > 0)
        {
            Inventory.Instance.hotbarItems = new ItemData[8];
            for (int i = 0; i < saveData.hotbarItems.Count; i++)
            {
                var itemSaveData = saveData.hotbarItems[i];
                ItemData item = GetItemDataByName(itemSaveData.itemName);
                {
                    item.quantity = itemSaveData.quantity;
                    Inventory.Instance.hotbarItems[i] = item;
                }
            }
        }

        if (saveData.inventoryItems != null && saveData.inventoryItems.Count > 0)
        {
            Inventory.Instance.inventoryItems = new ItemData[12];
            for (int i = 0; i < saveData.inventoryItems.Count; i++)
            {
                var itemSaveData = saveData.inventoryItems[i];
                ItemData item = GetItemDataByName(itemSaveData.itemName);
                {
                    item.quantity = itemSaveData.quantity;
                    Inventory.Instance.inventoryItems[i] = item;
                }

            }
        }
        if(saveData.animalsSaveData != null && saveData.animalsSaveData.Count > 0)
        {
            int coutner = 0;
            foreach(AnimalSaveData animal in saveData.animalsSaveData)
            {
                coutner++;
                GameObject prefab;
                if(animal.color == "brown")
                {
                    prefab = chickenBrown;
                }
                else
                {
                    prefab = chickenWhite;
                }
                StartCoroutine(Spowning(coutner,prefab,animal.name));
             }
        }
        if(saveData.animalsSaveData2 != null && saveData.animalsSaveData2.Count > 0)
        {
            int coutner = 0;
            foreach(AnimalSaveData animal in saveData.animalsSaveData2)
            {
                coutner++;
                StartCoroutine(Spowning2(coutner,prefabCow,animal.name));
             }
        }
 
        UImanager.Instance.RenderHotbar();
        StartCoroutine(Render());
    }
    IEnumerator Spowning(int num,GameObject prefab,string name)
    {
        Coop coop = FindObjectOfType<Coop>();
        yield return new WaitForEndOfFrame();
        coop.SpownChicken(num,prefab,name);
    }
    IEnumerator Spowning2(int num,GameObject prefab,string name)
    {
        CowShed cowShed = FindObjectOfType<CowShed>();
        yield return new WaitForEndOfFrame();
        cowShed.SpownCow(num,prefab,name);
    }
      IEnumerator Render()
    {
        UImanager.Instance.inventoryPanel.SetActive(true);
        UImanager.Instance.RenderInventory();
        yield return new WaitForEndOfFrame();
        UImanager.Instance.inventoryPanel.SetActive(false);
    }
 
    

}
