using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField]
    public SeedsData seedData;
    int seedIndex = 0;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    Text itemNameText;
    [SerializeField]
    Text itemDiscriptionText;
    [SerializeField]
    Text itemPrieceText;
    [SerializeField]
    Text notEnoughMoneyNot;

   public void SeedAssigning(SeedsData seed)
   {
       seedData = seed;
   }
    private void Update()
    {
        UpdateItemSprite();
    }

   

    private void UpdateItemSprite()
    {
        itemImage.sprite = seedData.thumbnail;
    }
    public void ShowItemDetails()
    {
        itemNameText.text = seedData.name;
        itemDiscriptionText.text = seedData.description;
        itemPrieceText.text = "Buy price: " + seedData.price + "G";
    }

    public void HideItemDetails()
    {
        itemNameText.text = "";
        itemDiscriptionText.text = "";
        itemPrieceText.text = "";
    }
    public void PurchaseItem()
    {
        if (Money.Instance.moneyAmount > 0 && Money.Instance.moneyAmount - seedData.price >= 0)
        {
            Inventory.Instance.HarvestCrops(seedData);
            Money.Instance.BuingItems(seedData.price);
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
