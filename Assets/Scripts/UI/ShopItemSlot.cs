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
    [SerializeField]
    Image HoverImage;
    [SerializeField]
    Image clickIamge;

   public void SeedAssigning(SeedsData seed)
   {
       seedData = seed;
       itemNameText.text = seedData.bulgarianName;
       itemPrieceText.text = (seedData.price).ToString();
   }
    private void Update()
    {
        UpdateItemSprite();
    }



    private void UpdateItemSprite()
    {
        if (seedData != null && itemImage != null)
        {
            itemImage.sprite = seedData.thumbnail;
        }
    }

    public void PurchaseItem()
    {
        StartCoroutine(clickAnimation());
        if (Money.Instance.moneyAmount > 0 && Money.Instance.moneyAmount - seedData.price >= 0)
        {
            Inventory.Instance.HarvestCrops(seedData);
            Money.Instance.BuingItems(seedData.price);
            Inventory.Instance.Stacks();
            UImanager.Instance.RenderHotbar();
            UImanager.Instance.RenderInventory();
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
    public void OnPointerEnter()
    {
        HoverImage.enabled = true;
    }
    public void OnPointerExit()
    {
        HoverImage.enabled = false;
    }
    IEnumerator clickAnimation()
    {
        clickIamge.enabled = true;
        yield return new WaitForSeconds(0.1f);
        clickIamge.enabled = false;
    }
}
