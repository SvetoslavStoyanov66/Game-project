using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public TimerSaveData timerSaveData;
    public List<LandSaveData> landsSaveData = new List<LandSaveData>();
    public List<InventoryItemSaveData> hotbarItems = new List<InventoryItemSaveData>();
    public List<InventoryItemSaveData> inventoryItems = new List<InventoryItemSaveData>();
    public MoneySaveData moneySaveData;
    public List<ItemUnlockedAchievement> itemsUnlockedAchievement = new List<ItemUnlockedAchievement>();

}

[System.Serializable]
public class TimerSaveData
{
    public int hours;
    public int minutes;
    public int day;
    public int year;
    public int seasonNum;
}
[System.Serializable]
public class LandSaveData
{
    public int id;
    public Land.LandStatus landStatus;
    public bool wasWateredYesterday;
    public bool hasSeedPlanted;
    public int currentDayProgression;
    public bool isCropInstantiated;
    public string seedDataName;
    public string cropDataName;
    public bool seedExists;
    public bool seed1Exists;
    public bool seed2Exists;
    public bool grownCropExists;
    public bool harvestedCropExist;
    public int daysForMultyHarvestableCrops;
    public bool isHarvestedCropActive;
}
[System.Serializable]
public class InventoryItemSaveData
{
    public string itemName;
    public int quantity;
      public InventoryItemSaveData(string itemName, int quantity)
    {
        this.itemName = itemName;
        this.quantity = quantity;
    }
}
[System.Serializable]
public class MoneySaveData
{
    public int moneyAmount;
      public MoneySaveData(int moneyAmount)
    {
        this.moneyAmount = moneyAmount;
    }
}
[System.Serializable]
public class ItemUnlockedAchievement
{
    public string itemName;
    public bool UnlockedAchievement;

    public ItemUnlockedAchievement(string itemName, bool UnlockedAchievement)
    {
        this.itemName = itemName;
        this.UnlockedAchievement = UnlockedAchievement;
    }
}