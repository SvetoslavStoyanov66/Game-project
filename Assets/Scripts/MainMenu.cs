using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Farm"); // Load your game scene
    }

    public void OpenSettings()
    {
        // Implement logic to open settings
        // e.g., show a settings panel within the same scene
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit the application
    }
}
