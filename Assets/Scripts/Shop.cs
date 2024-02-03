using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }
    [SerializeField]
    GameObject selection;
    [SerializeField]
    Canvas shopUI;
    public bool isShopOpen = false;
    [SerializeField]
    Button invButton;
    bool isInventoryActive;
    string[] shopFarmingNPCDialog = {
    "Добре дошли в нашия магазин за семена! Имаме всичко, от което се нуждаете за вашата градина.",
    "Всички наши семена са внимателно подбрани за високо качество и добив, за да осигурим вашата удовлетвореност и успех в градинарството.",
    "Какво ще искате?"
};
    [SerializeField]
    GameObject dialogUI;
    [SerializeField]
    List<ShopItemSlot> shopItemSlots = new List<ShopItemSlot>();
    [SerializeField]
    List<SeedsData> seeds = new List<SeedsData>();
    [SerializeField]
    List<Image> hoverButtonImages = new List<Image>();

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
        AssignSeedValue();
    }
    private void Update()
    {
        if (!dialogUI.activeSelf)
        {
            foreach (Image item in hoverButtonImages)
            {
                item.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf)
        {
            if (shopUI.enabled)
            {
                UImanager.Instance.inventoryPanel.SetActive(false);
                shopUI.enabled = false;
                invButton.gameObject.SetActive(true);
                Text notifiText = selection.GetComponentInChildren<Text>();
                notifiText.text = "Press E to talk";
            }
            else
            {
                if (!dialogUI.activeSelf)
                {
                    dialogUI.SetActive(true);
                    StartCoroutine(waitForDialog());
                    Text notifiText = selection.GetComponentInChildren<Text>();
                    notifiText.text = "Press E to close the shop";
                }
                else
                {
                    Dialogs.Instance.nextPage();
                }
            }         
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (shopUI.enabled)
            {
                UImanager.Instance.inventoryPanel.SetActive(false);  
            }
            shopUI.enabled = false;
            invButton.gameObject.SetActive(true);
        }
        isShopOpen = shopUI.enabled;
        
    }
    public void AssignSeedValue()
{
    int num = seeds.Count;

    List<int> uniqueIndices = new List<int>();
    while (uniqueIndices.Count < 3)
    {
        int randomIndex = Random.Range(0, num);
        if (!uniqueIndices.Contains(randomIndex))
        {
            uniqueIndices.Add(randomIndex);
        }
    }

    int index1 = uniqueIndices[0];
    int index2 = uniqueIndices[1];
    int index3 = uniqueIndices[2];

    shopItemSlots[0].SeedAssigning(seeds[index1]);
    shopItemSlots[1].SeedAssigning(seeds[index2]);
    shopItemSlots[2].SeedAssigning(seeds[index3]);

}
    private void ToggleShopUI()
    {
       
        if (!UImanager.Instance.IsInventoryPanelActive())
        {
            UImanager.Instance.ToggleInventoryPanel(-189);
        }
        else
        {
            UImanager.Instance.ChangeInventoryPosition(-189);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selection.SetActive(true);
            Text notifiText = selection.GetComponentInChildren<Text>();
            notifiText.text = "Press E to talk";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selection.SetActive(false);
        }
    }
    IEnumerator waitForDialog()
    {
        yield return new WaitForEndOfFrame();
        Dialogs.Instance.StartDialog(shopFarmingNPCDialog, "SeedShop");
    }
    public void BrowseSeedsButtomFunction()
    {
        dialogUI.SetActive(false);
        Dialogs.Instance.ResetText();
        shopUI.enabled = true;
        ToggleShopUI();
        invButton.gameObject.SetActive(false);
    }
    public void LeaveButtonFunction()
    {
        dialogUI.SetActive(false);
        Dialogs.Instance.ResetText();
    }
}
