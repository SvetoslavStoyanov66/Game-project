using System.Diagnostics;
using UnityEngine.EventSystems;

public class InventoryToHandBar : InventorySlot
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!UImanager.Instance.IsInventoryPanelActive()) // Check if the inventory panel is active
        {
            
            return;
        }

        if (itemToDisplay == null) return;

        Inventory.Instance.HotBarToInventory(slotIndex);
        itemToDisplay = null;
        UpdateDisplay();
    }
}
