using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Data.Common;
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    protected ItemData itemToDisplay;
    public Image ItemDisplayImage;
    public int slotIndex;
    [SerializeField]
    public TextMeshProUGUI quantityTMP;
    [SerializeField]
    private Text shopTextName;
    [SerializeField]
    private Text shopTextDescription;
    [SerializeField]
    Text itemPriceText;
    int foodPrice;
    int seedPrice;
    private void OnEnable()
    {
        quantityTMP = GetComponentInChildren<TextMeshProUGUI>();
        if(this.gameObject != null)
        {
            UpdateDisplay();
        }
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
            ItemDisplayImage.enabled = true;
        }
        else
        {
            ItemDisplayImage.enabled = false;
        }
        if  (itemToDisplay == null || itemToDisplay.quantity <= 1)
        {
            quantityTMP.text = "";
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
        if (itemToDisplay != null && (Shop.Instance == null || !Shop.Instance.isShopOpen))
        {
            Inventory.Instance.InventoryToHotBar(slotIndex);
            itemToDisplay = null;
            UpdateDisplay();
        }
        else if (itemToDisplay != null && Shop.Instance.isShopOpen)
        {
            if (Inventory.Instance != null && slotIndex >= 0 && slotIndex < Inventory.Instance.inventoryItems.Length && Inventory.Instance.inventoryItems[slotIndex] != null)
            {
                var item = Inventory.Instance.inventoryItems[slotIndex]; // Cache for readability and performance.
                
                // Decrement item quantity safely.
                item.quantity--;

                // Process selling based on item type.
                if (item is FoodData foodItem)
                {                  
                    int foodPrice = foodItem.sellPrice;
                    if (Money.Instance != null) Money.Instance.moneyAmount += foodPrice;
                }
                else if (item is SeedsData seedItem)
                {
                    double sellAmount = Convert.ToDouble(seedItem.price) * 0.75;
                    sellAmount = Math.Round(sellAmount, 2);
                    int sellAmountInt = Convert.ToInt32(sellAmount);
                    if (Money.Instance != null) Money.Instance.moneyAmount += sellAmountInt;
                }

                // Remove item from inventory if quantity is less than 1.
                if (item.quantity < 1)
                {
                    for (int i = 0; i < Inventory.Instance.inventoryItems.Length; i++)
                    {
                        if (Inventory.Instance.inventoryItems[i] != null && Inventory.Instance.inventoryItems[i].name == item.name)
                        {
                            Inventory.Instance.inventoryItems[i].quantity = 0; // Set quantity to 0 or handle as needed.
                            Inventory.Instance.inventoryItems[i] = null;
                        }
                    }
                }
            }
            UImanager.Instance.RenderHotbar();
            UImanager.Instance.RenderInventory();

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemToDisplay != null && (Shop.Instance == null || !Shop.Instance.isShopOpen))
        {
            UImanager.Instance.DisplayItemInfo(itemToDisplay);
        }
        else if(itemToDisplay != null && Shop.Instance.isShopOpen)
        {
           
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
        
        if (itemToDisplay != null && (Shop.Instance == null || !Shop.Instance.isShopOpen))
        {
            UImanager.Instance.DisplayItemInfo(null);
        }
        else if (itemToDisplay != null && Shop.Instance.isShopOpen)
        {
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
