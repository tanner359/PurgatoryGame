using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Laucher
{  
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);        
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void CloseApplication()
    {
        Application.Quit();
    }

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
