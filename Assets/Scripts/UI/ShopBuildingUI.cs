using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuildingUI : MonoBehaviour
{
    enum BuildingPageUI
    {
        ChikenBuilding,CowBuilding
    }
    [SerializeField]
    Image buildingImage;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text discriptionText;
    [SerializeField]
    Text moneyText;
    [SerializeField]
    GameObject chikenBuilding;
    [SerializeField]
    GameObject cowBuilding;
    [SerializeField]
    GameObject actualchikenBuilding;
    [SerializeField]
    GameObject chikenInterior;
    [SerializeField]
    GameObject actualcowBuilding;
    GameObject actualBuildingToInstatntiante;
    GameObject buildingToInstantiate;
    GameObject interiorToInstantiate;
    [SerializeField]
    Canvas shopUiCanvas;
    [SerializeField]
    GameObject selection;
    [SerializeField]
    GameObject enterDoorChicken;
    [SerializeField]
    GameObject exitDoorChicken;
    
    GameObject enterDoor;
    
    GameObject exitDoor;
    [SerializeField]

    GameObject farm;

    [SerializeField]
    GameObject notifier;
    [SerializeField]
    Sprite cowSprite;
    [SerializeField]
    Sprite chickenSprite;
    string[] builderFarmingStructuresDialog = {
    "Здравейте, добре дошли в офиса ми за архитектура! Специализираме в проектирането на съвременни сгради за селскостопански нужди, като кокошарници и краварници."
   ,"Гарантираме иновативни и устойчиви решения, които не само отговарят на вашите нужди, но и оптимизират производителността и благосъстоянието на животните."
   ,"Какво ще искате?"
};
    [SerializeField]
    List<Image> hoverButtonImages = new List<Image>();
    [SerializeField]
    GameObject dialogUI;
    [SerializeField]
    Text notEnoughMoneyText;
    private BuildingPageUI currentPage = BuildingPageUI.ChikenBuilding;
    private void Start()
    {
        ChangingUiPages(BuildingPageUI.ChikenBuilding);
    }
    private void ChangingUiPages(BuildingPageUI buildingPageUI)
    {
        switch (buildingPageUI)
        {
           case BuildingPageUI.ChikenBuilding:
                buildingImage.sprite = chickenSprite;
                nameText.text = "Кокошарник";
                discriptionText.text = "Сграда за отглеждане на кокошки";
                moneyText.text = "800";
                buildingToInstantiate = chikenBuilding;
                actualBuildingToInstatntiante = actualchikenBuilding;
                interiorToInstantiate = chikenInterior;
                enterDoor = enterDoorChicken;
                exitDoor = exitDoorChicken;
                break;
            case BuildingPageUI.CowBuilding:
                buildingImage.sprite = cowSprite;
                nameText.text = "Краварник";
                discriptionText.text = "Сграда за отглеждане на крави";
                moneyText.text = "1200";
                buildingToInstantiate = cowBuilding;
                actualBuildingToInstatntiante = actualcowBuilding;
                interiorToInstantiate = null;
                enterDoor = null;
                exitDoor = null;
                break;
        }
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
            notifier.SetActive(false);
            if (shopUiCanvas.enabled)
            {
                shopUiCanvas.enabled = false;
            }
            else
            {
                if (!dialogUI.activeSelf)
                {
                    dialogUI.SetActive(true);
                    StartCoroutine(waitForDialog());
                    
                }
                else
                {
                    Dialogs.Instance.nextPage();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            shopUiCanvas.enabled = false;
        }
    }
    public void LeftButtonFunction()
    {
        int currentIndex = (int)currentPage;
        currentIndex = (currentIndex + 1) % Enum.GetValues(typeof(BuildingPageUI)).Length;
        currentPage = (BuildingPageUI)currentIndex;
        ChangingUiPages(currentPage);
    }
    public void RightButtonFunction()
    {
        int currentIndex = (int)currentPage;
        currentIndex = (currentIndex - 1 + Enum.GetValues(typeof(BuildingPageUI)).Length) % Enum.GetValues(typeof(BuildingPageUI)).Length;
        currentPage = (BuildingPageUI)currentIndex;
        ChangingUiPages(currentPage);
    }
    public void ButtonBuyFunction()
    {
        if(Money.Instance.moneyAmount >= Convert.ToInt32(moneyText.text) && ((currentPage == BuildingPageUI.CowBuilding && !BuildingManager.Instance.isThereActiveCowBuilding()) || (currentPage == BuildingPageUI.ChikenBuilding && !BuildingManager.Instance.isThereActiveCoop())))
        {
            
            farm.SetActive(true);
            if(currentPage == BuildingPageUI.ChikenBuilding)
            {
                BuildingManager.Instance.BuildingAssigning(buildingToInstantiate,actualBuildingToInstatntiante,"chicken",Convert.ToInt32(moneyText.text));
            }
            else
            {
                BuildingManager.Instance.BuildingAssigning(buildingToInstantiate,actualBuildingToInstatntiante,"cow",Convert.ToInt32(moneyText.text));
            }
            
            shopUiCanvas.enabled = false;
            Money.Instance.moneyAmount -= Convert.ToInt32(moneyText.text);
        }
        else if(BuildingManager.Instance.isThereActiveCoop() || BuildingManager.Instance.isThereActiveCowBuilding())
        {
             StartCoroutine(TextDuration(4, "Не можеш да построиш още една такава сграда"));
        }
        else
        {
            StartCoroutine(TextDuration(4, "Недостиг на пари"));
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            selection.SetActive(true);
            notifier.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            selection.SetActive(false);
            notifier.SetActive(false);
        }
    }
    IEnumerator waitForDialog()
    {
        yield return new WaitForEndOfFrame();
        Dialogs.Instance.StartDialog(builderFarmingStructuresDialog, "BuildingShop");
    }
    public void LeaveButtonFunction()
    {
        dialogUI.SetActive(false);
        Dialogs.Instance.ResetText();
    }
    public void BrowseButtonFunction() 
    {
        dialogUI.SetActive(false);
        Dialogs.Instance.ResetText();
        shopUiCanvas.enabled = true;
    }
    IEnumerator TextDuration(int num,string text)
    {
        notEnoughMoneyText.text = text;
        yield return new WaitForSeconds(num);
        notEnoughMoneyText.text = "";
    }
    
}
