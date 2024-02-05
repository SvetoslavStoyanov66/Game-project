
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject ESCmenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !ESCmenu.activeSelf)
        {
            ESCmenu.SetActive(true);
        }    
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            ESCmenu.SetActive(false);
        }
    }
    public void CouninueButtonFunction()
    {
        ESCmenu.SetActive(false);
    }
    public void BackToMenuButtonFunction()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGameButtonFunction()
    {
        Application.Quit();
    }
}
