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
    Text woodText;
    [SerializeField]
    Text stoneText;
    [SerializeField]
    GameObject chikenBuilding;
    [SerializeField]
    GameObject cowBuilding;
    [SerializeField]
    GameObject actualchikenBuilding;
    [SerializeField]
    GameObject actualcowBuilding;
    GameObject actualBuildingToInstatntiante;
    GameObject buildingToInstantiate;
    [SerializeField]
    Canvas shopUiCanvas;
    [SerializeField]
    GameObject selection;
    string[] builderFarmingStructuresDialog = {
    "Greetings! I specialize in constructing farm buildings such as coops and barns. With a new coop, you can raise chickens for fresh eggs, and a sturdy barn will provide shelter for your livestock.",
    "If you're thinking about expanding your farm, I can help you design and build the perfect structure. Just let me know your requirements, and we'll get started on your project!"
,"What will you do?"
};
    [SerializeField]
    GameObject dialogUI;
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
                buildingImage = null;
                nameText.text = "Chiken coop";
                discriptionText.text = "Building for chikens";
                moneyText.text = "800";
                woodText.text = "300";
                stoneText.text = "100";
                buildingToInstantiate = chikenBuilding;
                actualBuildingToInstatntiante = actualchikenBuilding;
                break;
            case BuildingPageUI.CowBuilding:
                buildingImage = null;
                nameText.text = "Cowshed";
                discriptionText.text = "Building for cows";
                moneyText.text = "1200";
                woodText.text = "400";
                stoneText.text = "200";
                buildingToInstantiate = cowBuilding;
                actualBuildingToInstatntiante = actualcowBuilding;
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf)
        {
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
        BuildingManager.Instance.BuildingAssigning(buildingToInstantiate,actualBuildingToInstatntiante);
        shopUiCanvas.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            selection.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            selection.SetActive(false);
        }
    }
    IEnumerator waitForDialog()
    {
        yield return new WaitForEndOfFrame();
        Dialogs.Instance.StartDialog(builderFarmingStructuresDialog);
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
}
