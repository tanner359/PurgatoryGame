using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static string CurrentSave;

    public static string[] GetSaveNames()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        DirectoryInfo[] info = dir.GetDirectories();
        string[] fileNames = new string[info.Length];

        for(int i = 0; i < info.Length; i++)
        {
            fileNames[i] = info[i].Name;
        }
        return fileNames;
    }
    public static void CreateNewSave(string name)
    {
        string p_Main = Path.Combine(Application.persistentDataPath, name);
        var directory = Directory.CreateDirectory(p_Main);
        CurrentSave = directory.FullName;

        string p_Levels = Path.Combine(CurrentSave, "Levels");
        Directory.CreateDirectory(p_Levels);

        string p_Player = Path.Combine(CurrentSave, "Player");
        Directory.CreateDirectory(p_Player);
    }
    public static void SavePlayerData(PlayerData data)
    {       
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(CurrentSave, "Player")  + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("player data was successfully saved at " + path);
    }
    public static PlayerData LoadPlayerData()
    {
        string path = Path.Combine(CurrentSave, "Player") + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void SaveNPCData(NPCData data)
    {
        string level = Path.Combine(CurrentSave, "Levels", data.scene);
        if (!Directory.Exists(level)){
            Directory.CreateDirectory(level);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        string path = level + "/" + data.name + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("player data was successfully saved at " + path);
    }
    public static NPCData LoadNPCData(string scene, string name)
    {
        string level = Path.Combine(CurrentSave, "Levels", scene);
        string path = level + "/" + name + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            NPCData data = formatter.Deserialize(stream) as NPCData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void SaveLevelData(LevelData data)
    {    
        BinaryFormatter formatter = new BinaryFormatter();
        string path = CurrentSave + "/" + data.levelName + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("data for " + data.levelName + " was successfully saved at " + path);
    }
    public static LevelData LoadLevelData(string sceneName)
    {
        string path = CurrentSave + "/Levels" + "/" + sceneName + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("No save data for " + sceneName + " to load.");
            return null;
        }
    }
}
