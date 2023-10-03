using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [Header("Hotbar")]
    public ItemData[] hotbarItems = new ItemData[8];

    [Header("Inventory")]
    public ItemData[] inventoryItems = new ItemData[8];

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

    public void InventoryToHotBar(int inventoryIndex)
    {
        if (!UImanager.Instance.IsInventoryPanelActive()) // Check if the inventory is active
        {
            Debug.LogError("Inventory is not active. Cannot transfer item to hotbar.");
            return;
        }

        if (inventoryIndex < 0 || inventoryIndex >= inventoryItems.Length)
        {
            Debug.LogError("Invalid inventory index.");
            return;
        }

        ItemData itemToMove = inventoryItems[inventoryIndex];
        if (itemToMove != null)
        {
            inventoryItems[inventoryIndex] = null;

            for (int i = 0; i < hotbarItems.Length; i++)
            {
                if (hotbarItems[i] == null)
                {
                    hotbarItems[i] = itemToMove;
                    UImanager.Instance.DisplayHotbarItem(itemToMove, i);  // Display the item in the hotbar slot
                    return;
                }
            }
        }

    }

    public void HotBarToInventory(int hotbarIndex)
    {
        if (UImanager.Instance.IsInventoryPanelActive()) // Check if the inventory panel is active
        {
            if (hotbarIndex < 0 || hotbarIndex >= hotbarItems.Length)
            {
                Debug.LogError("Invalid hotbar index.");
                return;
            }

            ItemData itemToMove = hotbarItems[hotbarIndex];
            if (itemToMove != null)
            {
                hotbarItems[hotbarIndex] = null;

                for (int i = 0; i < inventoryItems.Length; i++)
                {
                    if (inventoryItems[i] == null)
                    {
                        inventoryItems[i] = itemToMove;
                        UImanager.Instance.RenderInventory();
                        return;
                    }
                }
            }
            return;
        }


        
    }
   
}
