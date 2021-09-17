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
    }

    private void Start()
    {
        LoadNonNativeNPC();
    }


    public void LoadNonNativeNPC()
    {
        NPCData[] data = SaveSystem.LoadAllNPCData(Laucher.GetCurrentSceneName());
        Debug.Log(data.Length);
        CharacterDatabase database = Resources.Load<CharacterDatabase>("Data/Character Database");
        if (data == null) { return; }

        for(int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[0]);
            if (GameObject.Find(data[i].name) == null)
            {
                Instantiate(database.GetCharacter(data[i].characterID), Vector3.zero, Quaternion.identity).name = data[i].name;
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
