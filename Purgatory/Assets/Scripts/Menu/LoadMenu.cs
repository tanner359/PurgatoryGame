using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadMenu : MonoBehaviour
{
    public GameObject saveButton_prefab;
    public Transform savesContent;
    private void OnEnable()
    {
        LoadSaves(SaveSystem.GetSaveNames());
    }

    public void LoadSaves(string[] fileNames)
    {
        for (int i = 0; i < savesContent.childCount; i++)
        {
            Destroy(savesContent.GetChild(i).gameObject);
        }

        for (int i = 0; i < fileNames.Length; i++)
        {
            GameObject GO = Instantiate(saveButton_prefab, savesContent);
            GO.name = fileNames[i];
            GO.GetComponentInChildren<TMP_Text>().text = fileNames[i];
        }
    }
}
