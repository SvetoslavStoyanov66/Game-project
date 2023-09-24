using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float secondsPerGameMinute = 1.0f;
    public int fontSize = 24;  // Adjust this value to change the font size

    private int hours = 6;
    private int minutes = 0;
    private float elapsedTime;

    private void Start()
    {
        // Set the font size
        timerText.fontSize = fontSize;
    }

    private void Update()
    {
        UpdateGameTime();
        UpdateUI();
    }

    private void UpdateGameTime()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= secondsPerGameMinute)
        {
            elapsedTime = 0f;

            minutes++;
            if (minutes >= 60)
            {
                minutes = 0;
                hours++;
                if (hours >= 24)
                    hours = 0;
            }
        }
    }

    private void UpdateUI()
    {
        timerText.text = $"{hours:00}:{minutes:00}";
    }
}
