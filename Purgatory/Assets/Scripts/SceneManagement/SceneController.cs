using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour, ISavable
{
    public Transform ActiveCharacters;
    public string[] GetActiveNPCs()
    {
        string[] IDs = new string[GameObject.FindGameObjectsWithTag("NPC").Length];

        int k = 0;
        for(int i = 0; i < ActiveCharacters.childCount; i++)
        {
            Transform character = ActiveCharacters.GetChild(i);
            if (character.CompareTag("NPC"))
            {
                IDs[k] = character.GetComponent<CharacterID>().ID;
                k++;
            }
        }

        return IDs;
    }

    public Vector3[] GetActiveNPCPositions()
    {
        Vector3[] positions = new Vector3[GameObject.FindGameObjectsWithTag("NPC").Length];
        int k = 0;

        for (int i = 0; i < ActiveCharacters.childCount; i++)
        {
            Transform character = ActiveCharacters.GetChild(i);
            if (character.CompareTag("NPC"))
            {
                positions[k] = character.position;
                k++;
            }
        }
        return positions;
    }


    public void Save()
    {
        SceneData data = new SceneData(this);
        SaveSystem.SaveSceneData(data);
    }

    public void Load()
    {
        GameObject GO = GameObject.Find("Characters");
        if (GO)
        {
            ActiveCharacters = GO.transform;
        }
        else
        {
            ActiveCharacters = new GameObject("Characters").transform;
        }
        SceneData data = SaveSystem.LoadSceneData(Laucher.GetCurrentSceneName());

        if (data != null && data.NPCData != null)
        {
            for (int i = 0; i < ActiveCharacters.childCount; i++)
            {
                Transform character = ActiveCharacters.GetChild(i);
                if (character.CompareTag("NPC"))
                {
                    Destroy(character.gameObject);
                }
            }

            CharacterDatabase characterDatabase = Resources.Load<CharacterDatabase>("Data/Character Database");
            for (int i = 0; i < data.NPCData.Length; i++)
            {
                GameObject character = characterDatabase.GetCharacter(data.NPCData[i].ID);
                Vector3 position = data.NPCData[i].GetPosition();
                Instantiate(character, position, Quaternion.identity, ActiveCharacters);
            }
        }
    }
}
