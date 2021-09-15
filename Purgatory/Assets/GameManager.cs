using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (this != instance) return;
        Notification_System.RunSetup();
    }

    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetSceneByBuildIndex(level).name == "MainMenu")
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
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
