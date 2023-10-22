using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField]
    List<SeedsData> seedDatas = new List<SeedsData>();
    int seedIndex = 0;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    List<ShopItemSlot> shopItemSlots = new List<ShopItemSlot>();

    private void Start()
    {
        if (seedDatas.Count > 0)
        {
            UpdateItemSprite();
        }
        ShopItem();
    }

    private void Update()
    {
        UpdateItemSprite();
    }

    public void ShopItem()
    {
        if (seedDatas.Count > 0)
        {
            // Generate a list of available seed indexes (not used in other slots)
            List<int> availableIndexes = new List<int>();
            for (int i = 0; i < seedDatas.Count; i++)
            {
                bool isUsed = false;
                foreach (var item in shopItemSlots)
                {
                    if (item.seedIndex == i)
                    {
                        isUsed = true;
                        break;
                    }
                }
                if (!isUsed)
                {
                    availableIndexes.Add(i);
                }
            }

            // If there are available indexes, pick one randomly
            if (availableIndexes.Count > 0)
            {
                seedIndex = availableIndexes[Random.Range(0, availableIndexes.Count)];
            }
        }
    }

    private void UpdateItemSprite()
    {
        itemImage.sprite = seedDatas[seedIndex].thumbnail;
    }
}
