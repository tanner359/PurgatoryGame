using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    public static void SavePlayerData(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("player data was successfully saved at " + path);
    }
    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.data";
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
        string path = Application.persistentDataPath + "/" + data.sceneName + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("data for " + data.sceneName + " was successfully saved at " + path);
    }
    public static SceneData LoadSceneData(string sceneName)
    {
        string path = Application.persistentDataPath + "/" + sceneName + ".data";
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
