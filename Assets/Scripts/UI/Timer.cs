using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text DaysText;
    public Text SeasonText;
    public Text DayInWeekText;
    public float secondsPerGameMinute = 1.0f;
    public int fontSize = 24;

    private int hours = 6;
    private int minutes = 0;
    private float elapsedTime;
    public int year;
    public int day;
    public int seasonNum;
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
    public enum Week
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }

    public Season season;
    public Week week;

    private void Start()
    {
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
            }
        }
        if (hours >= 24)
        {
            hours = 0;
            day++;
        }
       
            if (day >= 30)
            {
                day = 1;
                seasonNum++;
                if (seasonNum >= 4)
                {
                    seasonNum = 0;
                }
            }
        

        // Update the day of the week
        week = GetDayOfTheWeek();
    }

    public Week GetDayOfTheWeek()
    {
        int dayPassed = YearsToDays(year) + SeasonToDays(seasonNum) + day;
        int DayIndex = dayPassed % 7;
        return (Week)DayIndex;
    }

    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }

    public static int SeasonToDays(int season)
    {
        return season * 30;
    }

    private void UpdateUI()
    {
        timerText.text = $"{hours:00}:{minutes:00}";
        DaysText.text = day.ToString();
        SeasonText.text = season.ToString();
        DayInWeekText.text = week.ToString();
    }
}
