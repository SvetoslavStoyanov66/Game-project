using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    protected ItemData itemToDisplay;
    public Image ItemDisplayImage;
    public int slotIndex;

    private void Start()
    {
        UpdateDisplay();  // Call UpdateDisplay in Start to update the display when the game starts
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
}
