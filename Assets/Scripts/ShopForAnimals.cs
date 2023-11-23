using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopForAnimals : MonoBehaviour
{ 
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
    "Welcome to the Animal Emporium! We've got critters of all kinds here for your farm.",

    "Ah, the joy of raising animals! Every critter has its own needs and quirks.",

    "What will you do?"
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
    Image smallChickenImage;

    [SerializeField]
    Image smallCowImage;

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

    enum animals
    {
        chiken,cow
    }
    private void Start()
    {
        ChangingUiPages(animals.chiken);
        Text text = notifier.GetComponentInChildren<Text>();
        text.text = "Press E to Talk";
    }
     private void ChangingUiPages(animals anim)
    {
        switch (anim)
        {
           case animals.chiken:
           animalName.text = "Chiken";
           animalPicture = null;
           priece.text = "400";
           smallAnimalImage = smallChickenImage;
           animalCountText.text = chikensCount + "/5";
           noStructureImage = noStructureImageChicken;

                break;

            case animals.cow:
            animalName.text = "Cow";
            animalPicture = null;
            priece.text = "600";    
            smallAnimalImage = smallCowImage;
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
            if (canvas.enabled && !cantClose)
            {
                canvas.enabled = false;
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
        animalSelectionUI.SetActive(true);
        defaultUI.SetActive(false);
        cantClose = true;
    }
    public void LeaveButtonInAnimalBuyPanelFunction()
    {
        animalSelectionUI.SetActive(false);
        defaultUI.SetActive(true);
        cantClose = false;
    }

}
