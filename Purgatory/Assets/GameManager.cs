using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Awake()
    {
        Notification_System.RunSetup(); // setup required pre-requisites.

        GameObject GO = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        GO.name = "Player";

        LoadNonNativeNPC();
    }

    public void LoadNonNativeNPC()
    {
        NPCData[] data = SaveSystem.LoadAllNPCData(Laucher.GetCurrentSceneName());
        CharacterDatabase database = Resources.Load<CharacterDatabase>("Data/Character Database");
        if (data == null) { return; }

        for(int i = 0; i < data.Length; i++)
        {
            if (GameObject.Find(data[i].name) == null && !data[i].isPossessed)
            {
                Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
                GameObject go = Instantiate(database.GetCharacter(data[i].characterID), pos, Quaternion.identity);
                go.name = data[i].name;
            }
        }
    }


    public static void SaveGame()
    {
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>();
        for(int i = 0; i < sceneObjects.Length; i++)
        {
            if(sceneObjects[i].TryGetComponent(out ISavable obj))
            {
                obj.Save();
            }
        }

        Notification_System.Send_SystemNotify("The game has been saved");
    }
}
