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
    [SerializeField]
    AnimalFeeder[] animalFeeders = new AnimalFeeder[5];
    public List<AnimalMovement> chickens = new List<AnimalMovement>();
    [SerializeField]
    List<PlacesForEggs> placesForEggs = new List<PlacesForEggs>();
    [SerializeField]
    Quiz quiz;

    public int hours = 6;
    private int minutes = 0;
    private float elapsedTime;
    public int year;
    public int day = 1;
    public int seasonNum;
    private int lastDay;

    public int MinutesInDay;
    [SerializeField]
    GameObject shopInterior;
    [SerializeField]
    Coop coop;
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
        AssignLandIndex();
        timerText.fontSize = fontSize;
        LoadTimeState();
    }
    private void AssignLandIndex()
    {
        int counter = 0;
        foreach(Land land in landObjects)
        {
            land.id = counter;
            land.LoadLandState();
            counter++;
        }

    }
    public void SaveStates()
    {
        SaveData saveData = new SaveData
        {
            timerSaveData = new TimerSaveData
            {
                hours = this.hours,
                minutes = this.minutes,
                day = this.day,
                year = this.year,
                seasonNum = this.seasonNum,
            },
            landsSaveData = new List<LandSaveData>(),
            moneySaveData = new MoneySaveData(Money.Instance.moneyAmount)
            
        };

        foreach (Land land in landObjects)
        {

            saveData.landsSaveData.Add(new LandSaveData
            {
                id = land.id,
                landStatus = land.landStatus,
                wasWateredYesterday = land.wasWateredYesterday,
                hasSeedPlanted = land.hasSeedPlanted,
                currentDayProgression = land.CurrentDayProgression,
                isCropInstantiated = land.isCropInstantianted,
                cropDataName = land.crop != null ? land.crop.name : "NoCrop",
                seedDataName = land.seedData != null ? land.seedData.name : "NoSeedData",
                seedExists = land.seed != null,
                seed1Exists = land.seed1 != null,
                seed2Exists = land.seed2 != null,
                grownCropExists = land.GrownCrop != null,
                harvestedCropExist = land.seedData is CollectableSeedData,
                daysForMultyHarvestableCrops = land.daysForCollectingMultyHarvestableCrops,
                isHarvestedCropActive = land.harvestedCrop != null ? land.harvestedCrop.activeSelf : false
            });

        }
        
         foreach (var item in Inventory.Instance.hotbarItems)
    {
        if (item != null)
        {
            saveData.hotbarItems.Add(new InventoryItemSaveData(item.name, item.quantity));
        }
    }

    foreach (var item in Inventory.Instance.inventoryItems)
    {
        if (item != null)
        {
            saveData.inventoryItems.Add(new InventoryItemSaveData(item.name, item.quantity));
        }
    }
    
        SaveSystem.SaveGame(saveData);
}
    
    private void LoadTimeState()
    {
        SaveData saveData = SaveSystem.LoadGame();
        if (saveData != null && saveData.timerSaveData != null)
        {
            TimerSaveData timerData = saveData.timerSaveData;
            hours = timerData.hours;
            minutes = timerData.minutes;
            day = timerData.day;
            year = timerData.year;
            seasonNum = timerData.seasonNum;

            UpdateUI();
        }
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
            season++;
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
        float targetAngle = 0.05f * 5 * MinutesInDay - 90;

        angle = Mathf.Lerp(angle, targetAngle, 0.2f);

        SunTransform.eulerAngles = new Vector3(angle, 30, 0);
        }

    }
    private void ResetLandStatusToSoil()
    {
        foreach (Land land in landObjects)
        {
            land.ResetToSoil();
            land.grow = true;
            land.Grow();
        }
    }
    private void ResetLandStatusToWatared()
    {
        foreach (Land land in landObjects)
        {
            land.ResetToWatared();
            land.grow = true;
            land.Grow();
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
            quiz.isQuestioinForDayUsed = false;
            StartCoroutine(ShopAssigning());
            foreach (Land land in landObjects)
            {
                if (land.isWatered == true)
                {
                    land.wasWateredYesterday = true;
                    land.isWatered = false;
                }
            }
            // Generate a random number between 0 and 1
            float randomChance = Random.Range(0f, 1f);

            // Activate particle system with a 30% chance
            if (randomChance <= 0.3f)  // 30% chance
            {
                ResetLandStatusToWatared();
                ActivateParticleSystem();
                foreach (Land land in landObjects)
                {
                    land.isRaining = true;
                }
            }
            else
            {
                ResetLandStatusToSoil();
                DeactivateParticleSystem();
                foreach (Land land in landObjects)
                {
                    land.isRaining = false;
                }
            }
            foreach(AnimalMovement chicken in chickens)
            {
                foreach(AnimalFeeder feeder in animalFeeders)
                {
                    if(feeder.isFull())
                    {
                        bool spownEgg = false;
                        foreach(PlacesForEggs place in placesForEggs)
                        {
                            if(!place.isThereEgg())
                            {
                                spownEgg = true;
                            }
                        }
                        if(spownEgg)
                        {
                            while(true)
                            {
                                int num = UnityEngine.Random.Range(0,placesForEggs.Count - 1);
                                if(!placesForEggs[num].isThereEgg())
                                {
                                    placesForEggs[num].SpownEgg();
                                    coop.EggTextValueAssigning(1);
                                    break;
                                }

                            }

                        }
                        
                        feeder.EmptyingFeeder();
                        break;
                    }

                }

            }


        }
    }
    IEnumerator ShopAssigning()
    {
        shopInterior.SetActive(true);
        Shop.Instance.AssignSeedValue();
        yield return new WaitForEndOfFrame();
        shopInterior.SetActive(false);
    }
 
}
