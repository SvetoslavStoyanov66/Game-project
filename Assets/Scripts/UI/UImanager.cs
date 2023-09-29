using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance { get; private set; }

    [Header("Inventory System")]
    public GameObject inventoryPanel;
    public InventorySlot[] combinedSlots;
    public InventoryToHandBar EquipSLot;
    public InventorySlot[] hotbarSlots;
    public Text itemNameText;
    public Text itemDescriptionText;
    public int selectedSlotIndex = 0;
    [SerializeField] private Image slotHighlightImage;

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
    private void Update()
    {
        HandleInput();
        UpdateSlotUI();
    }

    public void AssignSlotIndexes()
    {
        for (int i = 0; i < combinedSlots.Length; i++)
        {
            combinedSlots[i].AssignIndex(i);
        }

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i].AssignIndex(i);
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

    public bool IsInventoryPanelActive()
    {
        return inventoryPanel.activeSelf;
    }
    void HandleInput()
    {
        int slotNumber = selectedSlotIndex + 1;  // Adjusting to start from 1-based index

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSlotIndex = 0;


        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSlotIndex = 1;


        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedSlotIndex = 2;


        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedSlotIndex = 3;


        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedSlotIndex = 4;


        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedSlotIndex = 5;


        }


        // Check if the selected slot is within the hotbar slots
        if (selectedSlotIndex >= 0 && selectedSlotIndex < hotbarSlots.Length)
        {
            InventorySlot selectedSlot = hotbarSlots[selectedSlotIndex];
            ItemData selectedItem = selectedSlot.Get_Item();

            if (selectedItem != null)
            {
                Debug.Log("Slot " + slotNumber + ": Item Name - " + selectedItem.name);
            }
            else
            {
                Debug.Log("Slot " + slotNumber + ": No item.");
            }
        }
        else
        {
            Debug.LogError("Invalid slot index: " + selectedSlotIndex);
        }

    }
    void UpdateSlotUI()
    {
        if (slotHighlightImage != null)
        {
            // Check if the selected slot is valid
            if (selectedSlotIndex >= 0 && selectedSlotIndex < hotbarSlots.Length)
            {
                RectTransform slotTransform = hotbarSlots[selectedSlotIndex].GetComponent<RectTransform>();
                RectTransform highlightTransform = slotHighlightImage.GetComponent<RectTransform>();

                // Position the highlight image at the selected slot
                highlightTransform.position = slotTransform.position;

                // Update the color of the selected slot
                for (int i = 0; i < hotbarSlots.Length; i++)
                {
                    Image image = hotbarSlots[i].GetComponent<Image>();
                    image.color = (i == selectedSlotIndex) ? Color.yellow : Color.white;
                }

                // Ensure the highlight image is visible
                slotHighlightImage.enabled = true;
            }
            else
            {
                // Hide the highlight image if the selected slot is invalid
                slotHighlightImage.enabled = false;
            }
        }
    }
}

