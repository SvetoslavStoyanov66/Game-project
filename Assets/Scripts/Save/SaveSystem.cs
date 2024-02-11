﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
     public static void SaveGame(SaveData data, int slot)
    {
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + $"/save{slot}.json", json);
    }
     public static SaveData LoadGame(int slot)
    {
        string path = Application.persistentDataPath + $"/save{slot}.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data;
        }
        return null;
    }
}