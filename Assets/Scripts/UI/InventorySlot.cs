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
        if (itemToDisplay != null)
        {
            Inventory.Instance.InventoryToHotBar(slotIndex);
            itemToDisplay = null;
            UpdateDisplay();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UImanager.Instance.DisplayItemInfo(itemToDisplay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UImanager.Instance.DisplayItemInfo(null);
    }
    public ItemData Get_Item()
    {
        // Return the item associated with this slot
        // Assuming you have a way to get the item data
        // You might want to manage this through your Inventory class
        return Inventory.Instance.hotbarItems[slotIndex];
    }

}
