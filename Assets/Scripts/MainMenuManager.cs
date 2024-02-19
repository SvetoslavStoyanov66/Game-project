using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject slotsMenu;
    [SerializeField]
    GameObject[] sltos = new GameObject[3];
    [SerializeField]
    GameObject[] buutonsObjects = new GameObject[3];
    [SerializeField]
    GameObject settingsUI;
    [SerializeField]
    AudioMixer audioMixer;
    

    void Start()
    {
        SlotStateAssigning(0);
        SlotStateAssigning(1);
        SlotStateAssigning(2);
    }
    private void SlotStateAssigning(int slot)
    {
         if (!SaveSystem.isSafeSlotEmpty(slot))
        {
            GameObject textEmpty = sltos[slot].transform.GetChild(2).gameObject;
            GameObject deleteSlotButton = sltos[slot].transform.GetChild(3).gameObject;
            textEmpty.SetActive(false);
            deleteSlotButton.SetActive(true);
            GameObject textGameObject = sltos[slot].transform.GetChild(4).gameObject;
            textGameObject.SetActive(true);
            Text textMoneyAmount = textGameObject.GetComponent<Text>();
            Text yearText = textGameObject.transform.GetChild(3).GetComponent<Text>();
            Text seasonText = textGameObject.transform.GetChild(4).GetComponent<Text>();
            Text dateText = textGameObject.transform.GetChild(5).GetComponent<Text>();
            Text achievementText = textGameObject.transform.GetChild(1).GetComponent<Text>();
            SaveData saveData = SaveSystem.LoadGameByInt(slot);
            if (saveData != null && saveData.timerSaveData != null)
            {
                TimerSaveData timerData = saveData.timerSaveData;
                yearText.text = "Година: " + timerData.year;
                string season = "";
                switch (timerData.seasonNum)
                {
                    case 0:
                        season = "пролет";
                        break;
                    case 1:
                        season = "лято";
                        break;
                    case 2:
                        season = "есен";
                        break;
                    case 3:
                        season = "зима";
                        break;
                }
                seasonText.text = "Сезон: " + season;
                dateText.text = "Дата: " + timerData.day;
            }
             if (saveData != null && saveData.itemsUnlockedAchievement != null)
            {
                int counter = 0;
                foreach(ItemUnlockedAchievement item in saveData.itemsUnlockedAchievement)
                {
                    if(item.UnlockedAchievement)
                    {
                        counter++;
                    }
                }
                achievementText.text = counter.ToString();
            }
            if (saveData != null && saveData.moneySaveData != null)
            {
                textMoneyAmount.text = saveData.moneySaveData.moneyAmount.ToString();
            }

        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && slotsMenu.activeSelf)
        {
            slotsMenu.SetActive(false);
        }
    }
    public void PlayButtonFunction()
    {
        slotsMenu.SetActive(true);
    }

    public void OpenSettingsButton()
    {
        settingsUI.SetActive(true);
        buutonsObjects[0].SetActive(false);
        buutonsObjects[1].SetActive(false);
        buutonsObjects[2].SetActive(false);
    }
     public void CloseSettingsButton()
    {
        settingsUI.SetActive(false);
        buutonsObjects[0].SetActive(true);
        buutonsObjects[1].SetActive(true);
        buutonsObjects[2].SetActive(true);
    }
    
    public void ExitGameButton()
    {
        Application.Quit(); 
    }

    public void SlotButtonFunction(int num)
    {
        SaveSystem.slot = num;
        SceneManager.LoadScene(1);
    }
    public void DeleteButtonFunction(int slot)
    {
        SaveSystem.DeleteSaveGame(slot);
        GameObject textEmpty = sltos[slot].transform.GetChild(2).gameObject;
        GameObject deleteSlotButton = sltos[slot].transform.GetChild(3).gameObject;
        textEmpty.SetActive(true);
        deleteSlotButton.SetActive(false);
        GameObject textGameObject = sltos[slot].transform.GetChild(4).gameObject;
        textGameObject.SetActive(false);
    }
    public void SetVolume(float volume)
    {
       audioMixer.SetFloat("Volume",volume); 
    }
}
