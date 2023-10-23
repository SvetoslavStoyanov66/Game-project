using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    protected ItemData itemToDisplay;
    public Image ItemDisplayImage;
    public int slotIndex;
    public TextMeshProUGUI quantityTMP;
    [SerializeField]
    private Text shopTextName;
    [SerializeField]
    private Text shopTextDescription;
    [SerializeField]
    Text itemPriceText;
    int foodPrice;
    int seedPrice;
    private void Start()
    {
        quantityTMP = GetComponentInChildren<TextMeshProUGUI>();
        UpdateDisplay();
        // Call UpdateDisplay in Start to update the display when the game starts
    }
    public void Display(ItemData itemToDisplay)
    {
        this.itemToDisplay = itemToDisplay;
        UpdateDisplay();
    }

    protected void UpdateDisplay()
    {
        if (itemToDisplay != null)
        {
            ItemDisplayImage.sprite = itemToDisplay.thumbnail;
            ItemDisplayImage.enabled = true;  // Enable the image component
        }
        else
        {
            
                ItemDisplayImage.enabled = false;  // Disable the image component
        }
        if (itemToDisplay == null || itemToDisplay.quantity <= 1)
        {
            quantityTMP.text = string.Empty;
        }
        else
        {
            
            quantityTMP.text = Convert.ToString(itemToDisplay.quantity);
        }
    }

    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (itemToDisplay != null && !Shop.Instance.isShopOpen)
        {
            Inventory.Instance.InventoryToHotBar(slotIndex);
            itemToDisplay = null;
            UpdateDisplay();
        }
        else if (itemToDisplay != null && Shop.Instance.isShopOpen)
        {
            if (Inventory.Instance.inventoryItems[slotIndex] is FoodData || Inventory.Instance.inventoryItems[slotIndex] is SeedsData)
            {
                (Inventory.Instance.inventoryItems[slotIndex]).quantity--;
                if ((Inventory.Instance.inventoryItems[slotIndex]).quantity <= 0)
                {
                    for (int i = 0; i < Inventory.Instance.inventoryItems.Length; i++)
                    {
                        if (Inventory.Instance.inventoryItems[i] != null && Inventory.Instance.inventoryItems[i].name == Inventory.Instance.inventoryItems[slotIndex].name)
                        {
                            Inventory.Instance.inventoryItems[i].quantity = 1;
                            Inventory.Instance.inventoryItems[i] = null;
                        }
                    }
                }
                if (Inventory.Instance.inventoryItems[slotIndex] is FoodData)
                {                  
                    foodPrice = (Inventory.Instance.inventoryItems[slotIndex] as FoodData).sellPrice;
                    Money.Instance.moneyAmount += foodPrice;
                }
                else if (Inventory.Instance.inventoryItems[slotIndex] is SeedsData)
                {
                    double sellAmount = Convert.ToDouble((Inventory.Instance.inventoryItems[slotIndex] as SeedsData).price) * 0.75;
                    sellAmount = Math.Round(sellAmount, 2);
                    int sellAmountInt = Convert.ToInt32(sellAmount);
                    Money.Instance.moneyAmount += sellAmountInt;
                    seedPrice = sellAmountInt;
                }
            }       
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemToDisplay != null && !Shop.Instance.isShopOpen)
        {
            UImanager.Instance.DisplayItemInfo(itemToDisplay);
        }
        else if(itemToDisplay != null && Shop.Instance.isShopOpen)
        {
            shopTextName.text = (Inventory.Instance.inventoryItems[slotIndex]).name;
            shopTextDescription.text = (Inventory.Instance.inventoryItems[slotIndex]).description;
            if ((Inventory.Instance.inventoryItems[slotIndex]) is SeedsData)
            {
                double sellAmount = Convert.ToDouble((Inventory.Instance.inventoryItems[slotIndex] as SeedsData).price) * 0.75;
                sellAmount = Math.Round(sellAmount, 2);
                int sellAmountInt = Convert.ToInt32(sellAmount);
                seedPrice = sellAmountInt;
                itemPriceText.text = "Sell price - " +seedPrice.ToString() + "G";
            }
            else if ((Inventory.Instance.inventoryItems[slotIndex]) is FoodData)
            {
                foodPrice = (Inventory.Instance.inventoryItems[slotIndex] as FoodData).sellPrice;
                itemPriceText.text = "Sell price - " + foodPrice.ToString() + "G";
            }
            else
            {
                itemPriceText.text = "Unsellable";
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        if (itemToDisplay != null && !Shop.Instance.isShopOpen)
        {
            UImanager.Instance.DisplayItemInfo(null);
        }
        else if (itemToDisplay != null && Shop.Instance.isShopOpen)
        {
            shopTextName.text = "";
            shopTextDescription.text = "";
            if ((Inventory.Instance.inventoryItems[slotIndex]) is SeedsData)
            {
                itemPriceText.text = "";
            }
            else if ((Inventory.Instance.inventoryItems[slotIndex]) is FoodData)
            {
                itemPriceText.text = "";
            }
            else
            {
                itemPriceText.text = "";
            }
        }
    }
    public ItemData Get_Item()
    {
        return Inventory.Instance.hotbarItems[slotIndex];
    }

}
