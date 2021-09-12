using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string levelToLoad;
    
    public void OpenDoor()
    {
        GameManager.instance.SaveGame();
        Laucher.LoadScene(levelToLoad);
    }
}
