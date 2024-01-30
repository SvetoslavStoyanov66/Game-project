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
    public static AchievementManager Instance { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

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
