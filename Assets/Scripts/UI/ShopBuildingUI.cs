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
    private BuildingPageUI currentPage = BuildingPageUI.ChikenBuilding;
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
                break;
            case BuildingPageUI.CowBuilding:
                buildingImage = null;
                nameText.text = "Cowshed";
                discriptionText.text = "Building for cows";
                moneyText.text = "1200";
                woodText.text = "400";
                stoneText.text = "200";
                break;
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
}
