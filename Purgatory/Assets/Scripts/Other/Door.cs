using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public static string lastOpened = "";
    public string levelToLoad;
    public void OpenDoor()
    {
        lastOpened = gameObject.name;
        GameManager.SaveGame(false);
        Laucher.LoadScene(levelToLoad);
    }
}
