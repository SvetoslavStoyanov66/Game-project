using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayButtonFunction()
    {
        SceneManager.LoadScene(1);
    }
    public void LeaveButtonFunction()
    {
        Application.Quit();
    }
}
