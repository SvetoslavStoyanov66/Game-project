using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<ItemData> allItemData;
    public ItemData GetItemDataByName(string name)
    {
        return allItemData.Find(item => item.name == name);
    }
}
