using UnityEngine.EventSystems;

public class InventoryToHandBar : InventorySlot
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        Inventory.Instance.HotBarToInventory(slotIndex);
    }
}