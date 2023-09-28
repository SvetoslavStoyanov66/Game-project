using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    ItemData itemToDisplay;
    public Image ItemDisplayImage;
    public int slotIndex;

    public void Display(ItemData itemToDisplay)
    {
        this.itemToDisplay = itemToDisplay;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (itemToDisplay != null)
        {
            ItemDisplayImage.sprite = itemToDisplay.thumbnail;
        }
        else
        {
            ItemDisplayImage.sprite = null; // Set the sprite to null if there's no item
        }
    }

    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // Check if it's an inventory item or a hotbar item
        if (slotIndex < Inventory.Instance.hotbarItems.Length)
        {
            Inventory.Instance.InventoryToHotBar(slotIndex);
        }
        else
        {
            Inventory.Instance.HotBarToInventory(slotIndex - Inventory.Instance.hotbarItems.Length);
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
}
