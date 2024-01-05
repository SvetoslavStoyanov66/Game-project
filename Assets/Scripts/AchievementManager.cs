using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    List<AchievementSlot> slots = new List<AchievementSlot>();
    [SerializeField]
    public List<ItemData>  items = new List<ItemData>();
    [SerializeField]
    GameObject achievementUI;
    void Start() 
    {
        for(int i = 0;i < items.Count; i++)
        {
            slots[i].ItemAssigning(items[i]);
            slots[i].UpdateUI();
        }
        
    }
    public void UnlockingAchievement(string itemName)
    {
        ItemData foundItem  = items.Find(item => item.name == itemName);
        if(!foundItem.achievementUnlock && foundItem != null)
        {
            foundItem.achievementUnlock = true;
        }
        foreach(AchievementSlot slot in slots)
        {
            slot.UpdateUI();
        }
    }
    public void ButtonFunction()
    {
        if(achievementUI.activeSelf)
        {
            achievementUI.SetActive(false);
        }
        else
        {
            achievementUI.SetActive(true);
        }
    }
  
}
