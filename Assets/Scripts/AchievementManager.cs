﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    List<AchievementSlot> slots = new List<AchievementSlot>();
    [SerializeField]
    public List<ItemData>  items = new List<ItemData>();
    [SerializeField]
    GameObject achievementUI;
    public static AchievementManager Instance { get; set; }
    [SerializeField]
    Image achievementNotifier;
    [SerializeField] float displayTime = 2f; 
    private Queue<string> achievementQueue = new Queue<string>();
    private bool isDisplayingAchievement = false;
    [SerializeField]
    Image informationWindow;
    [SerializeField]
    Image exclamationMark;
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

    void Start() 
    {
        foreach(ItemData item in items)
        {
            item.achievementUnlock = false;
        }
        for(int i = 0;i < items.Count; i++)
        {
            slots[i].ItemAssigning(items[i]);
            slots[i].UpdateUI();
        }
    }
    public void UnlockingAchievement(string itemName)
    {
        ItemData foundItem  = items.Find(item => item.name == itemName);
        if(foundItem != null && !foundItem.achievementUnlock)
        {
            foundItem.achievementUnlock = true;
            achievementQueue.Enqueue(itemName);
        }
        foreach(AchievementSlot slot in slots)
        {
            slot.UpdateUI();
        }
        if (!isDisplayingAchievement)
        {
            StartCoroutine(DisplayAchievements());
        }
    }
    public void ButtonFunction()
    {
        if(achievementUI.activeSelf)
        {
            achievementUI.SetActive(false);
            informationWindow.gameObject.SetActive(false);
        }
        else
        {
            achievementUI.SetActive(true);
            informationWindow.gameObject.SetActive(false);
        }
    }
    private IEnumerator DisplayAchievements()
{
    isDisplayingAchievement = true;

    while (achievementQueue.Count > 0)
    {
        string nextAchievement = achievementQueue.Dequeue();
        ItemData foundItem = items.Find(item => item.name == nextAchievement);

        if (foundItem != null)
        {
            Image thumbnailImage = achievementNotifier.transform.GetChild(0).GetComponent<Image>();
            thumbnailImage.sprite = foundItem.thumbnail;

            Text nameText = achievementNotifier.transform.GetChild(1).GetComponent<Text>();
            nameText.text = foundItem.name;

            achievementNotifier.gameObject.SetActive(true);
            yield return new WaitForSeconds(displayTime);
            achievementNotifier.gameObject.SetActive(false);
        }
    }

    isDisplayingAchievement = false;
}
public void BackButtonFuntion()
{
    informationWindow.gameObject.SetActive(false);
}
public void ApplyExclamationMarkNotfier()
{
    bool isTrue = false;
    foreach(AchievementSlot slot in slots)
    {
        if(slot.isExlamactionMarkActive())
        {
            isTrue = true;
            break;
        }
    }
    exclamationMark.gameObject.SetActive(isTrue);
}  
public void ExlmactionMarkActivation()
{
    exclamationMark.gameObject.SetActive(true);
}
}
