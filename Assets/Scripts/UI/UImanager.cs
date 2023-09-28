using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance { get; private set; }

    [Header("Inventory System")]
    public GameObject inventoryPanel;
    public InventorySlot[] combinedSlots; // Combine both toolSlots and itemSlots into a single array
    public InventoryToHandBar EquipSLot;
    public InventorySlot[] hotbarSlots;
    public Text itemNameText;
    public Text itemDescriptionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        RenderInventory();
        AssignSlotIndexes();
    }

    public void RenderInventory()
    {
        ItemData[] inventorySlots = Inventory.Instance.inventoryItems;
        RenderInventoryPanel(inventorySlots, combinedSlots);
    }
    public void DisplayHotbarItem(ItemData item, int hotbarIndex)
    {
        if (hotbarIndex >= 0 && hotbarIndex < hotbarSlots.Length)
        {
            hotbarSlots[hotbarIndex].Display(item);
        }
    }
    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            if (i < slots.Length && slots[i] != null)
            {
                uiSlots[i].Display(slots[i]);
            }
            else
            {
                uiSlots[i].Display(null);
            }
        }
    }

    public void AssignSlotIndexes()
    {
        for (int i = 0; i < combinedSlots.Length; i++)
        {
            combinedSlots[i].AssignIndex(i);
        }

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i].AssignIndex(i); // Assign correct indexes for hotbar slots
        }
    }

    public void ToggleInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        RenderInventory();
    }

    public void DisplayItemInfo(ItemData data)
    {
        if (data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
            return;
        }
        itemDescriptionText.text = data.description;
        itemNameText.text = data.name;
    }
}
