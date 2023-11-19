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
                break;
            case animals.cow:
            animalName.text = "Cow";
            animalPicture = null;
            priece.text = "600";           
                break;
        }
    }
    void Update()
    {
        if(notifier.activeSelf && Input.GetKeyDown(KeyCode.E) && !canvas.enabled)
        {
            canvas.enabled = true;
            notifier.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            canvas.enabled = false;
        }
        
    }
    private void OnTriggerEnter()
    {
        notifier.SetActive(true);
    }
     private void OnTriggerExit()
    {
        notifier.SetActive(false);
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

}
