using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject slotsMenu;
    [SerializeField]
    GameObject[] sltos = new GameObject[3];

    void Start()
    {
        if (!SaveSystem.isSafeSlotEmpty(0))
        {
            GameObject textEmpty = sltos[0].transform.GetChild(2).gameObject;
            GameObject deleteSlotButton = sltos[0].transform.GetChild(3).gameObject;
            textEmpty.SetActive(false);
            deleteSlotButton.SetActive(true);
        }
        if (!SaveSystem.isSafeSlotEmpty(1))
        {
            GameObject textEmpty = sltos[1].transform.GetChild(2).gameObject;
            GameObject deleteSlotButton = sltos[1].transform.GetChild(3).gameObject;
            textEmpty.SetActive(false);
            deleteSlotButton.SetActive(true);
        }
        if (!SaveSystem.isSafeSlotEmpty(2))
        {
            GameObject textEmpty = sltos[2].transform.GetChild(2).gameObject;
            GameObject deleteSlotButton = sltos[2].transform.GetChild(3).gameObject;
            textEmpty.SetActive(false);
            deleteSlotButton.SetActive(true);
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
    }
}
