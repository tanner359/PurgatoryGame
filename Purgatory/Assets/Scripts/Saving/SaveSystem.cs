using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


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
        string path = Path.Combine(Application.persistentDataPath, name);
        var directory = Directory.CreateDirectory(path);
        CurrentSave = directory.FullName;
        Debug.Log(directory.FullName);
    }
    public static void SavePlayerData(PlayerData data)
    {       
        BinaryFormatter formatter = new BinaryFormatter();
        string path = CurrentSave + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("player data was successfully saved at " + path);
    }
    public static PlayerData LoadPlayerData()
    {
        string path = CurrentSave + "/player.data";
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

    public static void SaveSceneData(SceneData data)
    {    
        BinaryFormatter formatter = new BinaryFormatter();
        string path = CurrentSave + "/" + data.sceneName + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("data for " + data.sceneName + " was successfully saved at " + path);
    }
    public static SceneData LoadSceneData(string sceneName)
    {
        string path = CurrentSave + "/" + sceneName + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SceneData data = formatter.Deserialize(stream) as SceneData;
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
