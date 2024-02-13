using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        LoadGame();
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
                    Quaternion quaternion = Quaternion.Euler(-90,0,-90);
                    coopBuilding = Instantiate(coopBuilding,position,quaternion);
                    BuildingManager.Instance.SaveBuildingDoor(coopBuilding);
                }
                if(item2 != null && item2.buildingName == "cowShed")
                {
                    BuildingManager.Instance.cowBuildingActive = true;
                    Vector3 position = new Vector3(item2.positionX,item2.positionY,item2.positionZ);
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
        UImanager.Instance.RenderHotbar();
        UImanager.Instance.RenderInventory();
    }
    IEnumerator Spowning(int num,GameObject prefab,string name)
    {
        Coop coop = FindObjectOfType<Coop>();
        yield return new WaitForEndOfFrame();
        coop.SpownChicken(num,prefab,name);
    }
 
    

}
