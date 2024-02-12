using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject slotsMenu;

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
}
