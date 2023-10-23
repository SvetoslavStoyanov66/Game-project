using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance { get; private set; }

    [Header("Inventory System")]
    public PlayerInteraction playerInteraction;
    public GameObject inventoryPanel;
    public InventorySlot[] combinedSlots;
    public InventoryToHandBar EquipSLot;
    public InventorySlot[] hotbarSlots;
    public Text itemNameText;
    public Text itemDescriptionText;
    public int selectedSlotIndex = 0;
    [SerializeField]
    private GameObject hoe;
    [SerializeField]
    private GameObject axe;
    [SerializeField]
    private GameObject pickaxe;
    [SerializeField]
    private GameObject wateringCan;
    public Player player;  
    
   
    Land selectedLand;

    [Header("Selection Marker")]
    public Image selectionMarkerImage;  // Assign the selection marker image in the inspector

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
    public void RenderHotbar()
    {
        ItemData[] hotBarSlots = Inventory.Instance.hotbarItems;
        RenderHotBarPanel(hotBarSlots, hotbarSlots);
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
    void RenderHotBarPanel(ItemData[] slots, InventorySlot[] uiSlots)
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

    public void ToggleInventoryPanel(int y)
    {   RectTransform desiredPosition = inventoryPanel.GetComponent<RectTransform>();
        desiredPosition.anchoredPosition = new Vector2(0,y);
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
    public ItemData GetSelectedHotbarItem()
    {
        if (selectedSlotIndex >= 0 && selectedSlotIndex < hotbarSlots.Length)
        {
            return hotbarSlots[selectedSlotIndex].Get_Item();
        }
        return null;
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
                if (selectedItem.name == "Hoe")
                {
                    hoe.SetActive(true);
                }
                if (selectedItem.name != "Hoe")
                {
                    hoe.SetActive(false);
                }
                if (selectedItem.name == "Axe")
                {
                    axe.SetActive(true);
                }
                if (selectedItem.name != "Axe")
                {
                    axe.SetActive(false);
                }
                if (selectedItem.name == "Pickaxe")
                {
                    pickaxe.SetActive(true);
                }
                if (selectedItem.name != "Pickaxe")
                {
                    pickaxe.SetActive(false);
                }
                if (selectedItem.name == "Wateringcan")
                {
                    wateringCan.SetActive(true);
                }
                if (selectedItem.name != "Wateringcan")
                {
                    wateringCan.SetActive(false);
                }
                if (selectedItem.name == "Carrot")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        player.fillAmount += .1f;
                    }
                }
                // Pass the selected item name to the land's Interact method
                if (selectedLand != null)
                {
                    selectedLand.Interact(selectedItem.name);
                    if (selectedItem is SeedsData)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            playerInteraction.InteractWithLand();  // Call the InteractWithLand method when pressing E
                        }
                    }
                }
                
            }
            else
            {
                hoe.SetActive(false);
                axe.SetActive(false);
                pickaxe.SetActive(false);
            }

            // Update the position of the selection marker to the selected slot
            RectTransform selectedSlotTransform = hotbarSlots[selectedSlotIndex].GetComponent<RectTransform>();
            selectionMarkerImage.rectTransform.position = selectedSlotTransform.position;
        }
        else
        {
            Debug.LogError("Invalid slot index: " + selectedSlotIndex);
        }
    }

    void UpdateSlotUI()
    {
        // Update the color of the selected slot (if needed)
    }


}
