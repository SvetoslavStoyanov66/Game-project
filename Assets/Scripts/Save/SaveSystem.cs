using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static int slot { get; set; }
     public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + $"/save{slot}.json", json);
    }
     public static SaveData LoadGame()
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
    public static void DeleteSaveGame(int slot)
    {
        string path = Application.persistentDataPath + $"/save{slot}.json";
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
    public static bool isSafeSlotEmpty(int slot)
    {
        string path = Application.persistentDataPath + $"/save{slot}.json";

        if (!System.IO.File.Exists(path))
        {
            return true;
        }

        string content = File.ReadAllText(path);

        return string.IsNullOrWhiteSpace(content);
    }
     public static SaveData LoadGameByInt(int slot)
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
