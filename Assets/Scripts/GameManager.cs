using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        LoadGame();
    }
    public List<ItemData> allItemData;
    public ItemData GetItemDataByName(string name)
    {
        return allItemData.Find(item => item.name == name);
    }
    public void LoadGame()
    {
        SaveData saveData = SaveSystem.LoadGame(0);

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
        UImanager.Instance.RenderHotbar();
        UImanager.Instance.RenderInventory();

    }

}
