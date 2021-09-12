using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void SaveGame()
    {
        GameManager.instance.SaveGame();       
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
