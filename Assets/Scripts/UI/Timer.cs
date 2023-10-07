using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Timer : MonoBehaviour
{
    Player player1;
    AnimaationsPlayer player;
    public List<Land> landObjects;
    public Transform SunTransform;
    public Text timerText;
    public Text DaysText;
    public Text SeasonText;
    public Text DayInWeekText;
    public float secondsPerGameMinute = 1.0f;
    public int fontSize = 24;
    public float angle;
    public ParticleSystem particleSystem;
    private bool particleSystemActivated = false;

    public int hours = 6;
    private int minutes = 0;
    private float elapsedTime;
    public int year;
    public int day = 1;
    public int seasonNum;
    private int lastDay;
    

    public int MinutesInDay;
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
        SunMovement();
        LandAndWeatherChanging();
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
    public void SunMovement()
    {
        if (SunTransform != null)
        {
            MinutesInDay = (hours * 60) + minutes;
            angle = .25f * MinutesInDay - 90;
            SunTransform.eulerAngles = new Vector3(angle, 0, 0);
        }

    }
    private void ResetLandStatusToSoil()
    {
        foreach (Land land in landObjects)
        {
            land.ResetToSoil();
            
        }
    }
    private void ResetLandStatusToWatared()
    {
        foreach (Land land in landObjects)
        {
            land.ResetToWatared();
        }
    }
    private void ActivateParticleSystem()
    {
        if (particleSystem != null)
        {
            particleSystem.gameObject.SetActive(true);
        }
    }
    private void DeactivateParticleSystem()
    {
        if (particleSystem != null)
        {
            particleSystem.gameObject.SetActive(false);
        }
    }
    private void LandAndWeatherChanging()
    {
        if (lastDay != day)
        {
            lastDay = day;

            // Generate a random number between 0 and 1
            float randomChance = Random.Range(0f, 1f);

            // Activate particle system with a 30% chance
            if (randomChance <= 0.3f)  // 30% chance
            {
                ResetLandStatusToWatared();
                ActivateParticleSystem();
            }
            else
            {
                ResetLandStatusToSoil();
                DeactivateParticleSystem();
            }
            

        }
    }

}