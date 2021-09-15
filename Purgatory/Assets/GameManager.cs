using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void OnEnable()
    {
        if (this != instance) return;
        LoadGame();
        Notification_System.RunSetup();      
    }

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

        Debug.Log("Loaded Last saved level");
        PlayerData data = SaveSystem.LoadPlayerData();
        if (data != null && (data.currentScene != Laucher.GetCurrentSceneName()))
        {
            Laucher.LoadScene(data.currentScene);
            return;
        }
    }
    
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
        if(SceneManager.GetSceneByBuildIndex(level).name == "MainMenu")
        {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(LoadLevel());
    }

    public IEnumerator LoadLevel()
    {
        yield return new WaitForFixedUpdate();
        Debug.Log("Level Loaded");
        LoadGame();
        Notification_System.RunSetup();
        SaveGame();
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

    public void LoadGame()
    {
        Debug.Log("Load Game");
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>();

        for (int i = 0; i < sceneObjects.Length; i++)
        {
            ISavable target = sceneObjects[i].GetComponent<ISavable>();
            if (target != null)
            {
                Debug.Log(sceneObjects[i].name + " was loaded");
                target.Load();
            }
        }
    }
}
