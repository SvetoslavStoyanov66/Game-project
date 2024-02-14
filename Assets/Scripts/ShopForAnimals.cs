using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopForAnimals : MonoBehaviour
{ 
    [SerializeField]
    Canvas canvasForAnimalFood;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    GameObject notifier;
    private animals currentPage = animals.chiken;
    [SerializeField]
    Text animalName;
    [SerializeField]
    Text priece;
    [SerializeField]
    Image animalPicture;
    string[] animalShopDialog = {
    "Добре дошли в нашия магазин за животни и храни за тях! Отглеждаме и предлагаме разнообразие от животни, идеални за вашето стопанство или дом.",

    "Всички наши продукти за хранене са висококачествени, създадени да поддържат вашите животни здрави и щастливи. Нека заедно намерим най-доброто за вашите нужди.",

    "Какво ще искате?"
};
[SerializeField]
    GameObject dialogUI;
    [SerializeField]
    GameObject selection;
    [SerializeField]
    GameObject animalSelectionUI;
    [SerializeField]
    GameObject defaultUI;
    bool cantClose = false;
    bool cantUseDialog = false;
    int chikensCount = 0;

    int cowCount = 0;

    [SerializeField]
    Sprite smallChickenImage;

    [SerializeField]
    Sprite smallCowImage;

    [SerializeField]

    Text animalCountText;
    [SerializeField]

    Image smallAnimalImage;

    [SerializeField]
    Image noStructureImage;

    [SerializeField]
    Image noStructureImageChicken;
    [SerializeField]
    Image noStructureImageCow;
    [SerializeField]
    Coop coop;
    [SerializeField]
    CowShed cowShed;
    [SerializeField]
    GameObject chicken1;
    [SerializeField]
    GameObject chicken2;
    [SerializeField]
    GameObject cow;
    [SerializeField]
    InputField inputField;
    [SerializeField]
    Sprite chickenSprite;
    [SerializeField]
    Sprite cowSprite;
    enum animals
    {
        chiken,cow
    }
     void Start()
    {
        ChangingUiPages(animals.chiken);
        Text text = notifier.GetComponentInChildren<Text>();
        text.text = "Натисни Е за да приказваш";
    }
     private void ChangingUiPages(animals anim)
    {
        switch (anim)
        {
           case animals.chiken:
           animalName.text = "Кокошка";
           animalPicture.sprite = chickenSprite;
           priece.text = "150";
           smallAnimalImage.sprite = smallChickenImage;
           animalCountText.text = chikensCount + "/5";
           noStructureImage = noStructureImageChicken;

                break;

            case animals.cow:
            animalName.text = "Крава";
            animalPicture.sprite = cowSprite;
            priece.text = "300";    
            smallAnimalImage.sprite = smallCowImage;
            animalCountText.text = cowCount + "/5";
            noStructureImage = noStructureImageCow;

                break;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (selection.activeSelf)
        {
            if ((canvas.enabled || canvasForAnimalFood.enabled) && !cantClose)
            {
                canvas.enabled = false;
                canvasForAnimalFood.enabled = false;
            }
            else if(!cantUseDialog)
            {
                if (!dialogUI.activeSelf)
                {
                    notifier.SetActive(false);
                    dialogUI.SetActive(true);
                    StartCoroutine(waitForDialog());
                    
                }
                else
                {
                    Dialogs.Instance.nextPage();
                }
            }
        }
           
        }
        if(animalSelectionUI.activeSelf)
        {
            cantUseDialog = true;
        }
        else
        {
            cantUseDialog = false;
        }
    }
    private void OnTriggerEnter()
    {
        notifier.SetActive(true);
        selection.SetActive(true);
    }
     private void OnTriggerExit()
    {
        notifier.SetActive(false);
        selection.SetActive(false);

    }
    public void LeftButtonFunction()
    {
        int currentIndex = (int)currentPage;
        currentIndex = (currentIndex + 1) % Enum.GetValues(typeof(animals)).Length;
        currentPage = (animals)currentIndex;
        ChangingUiPages(currentPage);
    }
    public void RightButtonFunction()
    {
        int currentIndex = (int)currentPage;
        currentIndex = (currentIndex - 1 + Enum.GetValues(typeof(animals)).Length) % Enum.GetValues(typeof(animals)).Length;
        currentPage = (animals)currentIndex;
        ChangingUiPages(currentPage);
    }
     IEnumerator waitForDialog()
    {
        yield return new WaitForEndOfFrame();
        Dialogs.Instance.StartDialog(animalShopDialog, "AnimalsShop");
    }
    public void BrowseAnimalsutton()
    {
        Dialogs.Instance.ResetText();
        dialogUI.SetActive(false);
        canvas.enabled = true;
    }

    public void BuyButtonFunction()
    {
        if((BuildingManager.Instance.isThereActiveCoop() && currentPage == animals.chiken) || (BuildingManager.Instance.isThereActiveCowBuilding() && currentPage == animals.cow))
        {
            animalSelectionUI.SetActive(true);
            defaultUI.SetActive(false);
            cantClose = true;
        }
    }
    public void LeaveButtonInAnimalBuyPanelFunction()
    {
        animalSelectionUI.SetActive(false);
        defaultUI.SetActive(true);
        cantClose = false;
        inputField.text = "";   
    }
    public void ConfirmButtonnInAnimalBuyPanelFunction()
    {
        Money.Instance.moneyAmount -= Convert.ToInt32(priece.text);
        string name = inputField.text;
         if(currentPage == animals.chiken)
        {
        int chickenSckin = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 2f));
        GameObject chiken = chicken2;
        if(chickenSckin == 1)
        {
            chiken = chicken1;
        }
        
        if(name != "")
        {
            if(chikensCount < 5)
            {
                chikensCount++;  
            }
                
            coop.SpownChicken(chikensCount, chiken, name);
            LeaveButtonInAnimalBuyPanelFunction();  
            ChangingUiPages(animals.chiken);               
        }
        }
        else
        {
             if(cowCount < 5)
            {
                cowCount++;  
            }
           cowShed.SpownCow(cowCount,cow,name);
           LeaveButtonInAnimalBuyPanelFunction();  
           ChangingUiPages(animals.cow);  
        }
    }
    public void ButtonForAnimalFood()
    {
        canvasForAnimalFood.enabled = true;
        Dialogs.Instance.ResetText();
        dialogUI.SetActive(false);
    }

}
