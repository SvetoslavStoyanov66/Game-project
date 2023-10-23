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
    [SerializeField]
    Text itemNameText;
    [SerializeField]
    Text itemDiscriptionText;
    [SerializeField]
    Text itemPrieceText;
    [SerializeField]
    Text notEnoughMoneyNot;

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
    public void ShowItemDetails()
    {
        itemNameText.text = seedDatas[seedIndex].name;
        itemDiscriptionText.text = seedDatas[seedIndex].description;
        itemPrieceText.text = "Buy price: " + seedDatas[seedIndex].price + "G";
    }

    public void HideItemDetails()
    {
        itemNameText.text = "";
        itemDiscriptionText.text = "";
        itemPrieceText.text = "";
    }
    public void PurchaseItem()
    {
        if (Money.Instance.moneyAmount > 0 && Money.Instance.moneyAmount - seedDatas[seedIndex].price > 0)
        {
            Inventory.Instance.HarvestCrops(seedDatas[seedIndex]);
            Money.Instance.BuingItems(seedDatas[seedIndex].price);
            notEnoughMoneyNot.text = "";
        }
        else 
        {
            StartCoroutine(TextDuration(4));
        }
    }
    IEnumerator TextDuration(int num)
    {
        notEnoughMoneyNot.text = "Not enough money";
        yield return new WaitForSeconds(num);
        notEnoughMoneyNot.text = "";
    }
}
