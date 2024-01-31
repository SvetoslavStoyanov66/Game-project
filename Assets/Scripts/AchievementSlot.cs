﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    ItemData item;
    public Text nameText;
    Image picture;
    [SerializeField]
    Image itemInformationWindow;
    Image exclamationMark;
    public bool showExclamationMark = false;

    public void ItemAssigning(ItemData data)
    {
        item = data;
    }
    public void UpdateUI()
    {
        if(item != null)
        {
            Transform transform1 = this.gameObject.transform.GetChild(1);
            Transform transform2 = this.gameObject.transform.GetChild(0);
            nameText = transform1.GetComponent<Text>();
            picture = transform2.GetComponent<Image>();
            if(item.achievementUnlock == true)
            {
                nameText.text = item.name;
                picture.sprite = item.thumbnail;
                exclamationMark = this.gameObject.transform.GetChild(2).GetComponent<Image>();
                exclamationMark.gameObject.SetActive(showExclamationMark);
            }
            else
            {
                nameText.text = "???";
                picture.sprite = item.UnUnlockedThubnail;
            }
        }      
    }
    public void ShowItemInformationWindow()
    {
        if(item.achievementUnlock)
        {
            itemInformationWindow.gameObject.SetActive(true);
            Text name = itemInformationWindow.transform.GetChild(0).GetComponent<Text>();
            Text discription = itemInformationWindow.transform.GetChild(1).GetComponent<Text>();
            Image image = itemInformationWindow.transform.GetChild(2).GetComponent<Image>();
            showExclamationMark = false;
            exclamationMark.gameObject.SetActive(showExclamationMark);
            

            name.text = item.name;
            discription.text = item.achievementDiscription;
            image.sprite = item.thumbnail;
        }

    }
}
