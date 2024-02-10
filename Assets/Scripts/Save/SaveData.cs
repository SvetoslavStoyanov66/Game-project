using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public TimerSaveData timerSaveData;
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
