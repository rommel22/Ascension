
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        CurrentLevelTrackerData currentLevelTrackerData = new CurrentLevelTrackerData();
        formatter.Serialize(stream, currentLevelTrackerData);
        stream.Close();
    }

    public static void Load()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CurrentLevelTrackerData data = formatter.Deserialize(stream) as CurrentLevelTrackerData;
            stream.Close();

            CurrentLevelTracker.latestLevel = data.latestLevel;
        }
    }
}
