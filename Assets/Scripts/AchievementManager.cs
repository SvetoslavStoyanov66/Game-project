using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    public List<ItemData>  items = new List<ItemData>();
    public void UnlockingAchievement(string itemName)
    {
        ItemData foundItem  = items.Find(item => item.name == itemName);
        if(!foundItem.achievementUnlock && foundItem != null)
        {
            foundItem.achievementUnlock = true;
        }
    }
  
}
