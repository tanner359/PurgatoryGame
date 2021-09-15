using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_InputField saveNameInputField;
    public void EnableButton(Button button)
    {
        if (saveNameInputField.text.Length > 0)
        {
            button.interactable = true;
            return;
        }
        button.interactable = false;
    }

    public void CreateNewSave()
    {
        SaveSystem.CreateNewSave(saveNameInputField.text);
        Laucher.LoadScene("Living Realm");
    }

    public void LoadScene(string sceneName)
    {
        Laucher.LoadScene(sceneName);
    }

    public void SaveGame()
    {
        GameManager.instance.SaveGame();       
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OpenMenu(GameObject menu)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.activeInHierarchy)
            {
                child.SetActive(false);
            }
        }
        menu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
