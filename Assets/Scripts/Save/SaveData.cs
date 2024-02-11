using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public TimerSaveData timerSaveData;
    public List<LandSaveData> landsSaveData = new List<LandSaveData>();

}

[System.Serializable]
public class TimerSaveData
{
    public int hours;
    public int minutes;
    public int day;
    public int year;
    public int seasonNum;
}
[System.Serializable]
public class LandSaveData
{
    public int id;
    public Land.LandStatus landStatus;
    public bool wasWateredYesterday;
    public bool hasSeedPlanted;
    public int currentDayProgression;
    public bool isCropInstantiated;
    public string seedDataName;
    public string cropDataName;
    public bool seedExists;
    public bool seed1Exists;
    public bool seed2Exists;
    public bool grownCropExists;
}