using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, ISavable
{
    public string[] GetActiveNPCs()
    {
        GameObject[] AllNPC = GameObject.FindGameObjectsWithTag("NPC");
        string[] IDs = new string[AllNPC.Length];

        for(int i = 0; i < IDs.Length ; i++)
        {
            IDs[i] = AllNPC[i].GetComponent<CharacterID>().characterID;
        }

        return IDs;
    }

    public Vector3[] GetActiveNPCPositions()
    {
        GameObject[] AllNPC = GameObject.FindGameObjectsWithTag("NPC");
        Vector3[] positions = new Vector3[AllNPC.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = AllNPC[i].transform.position;
        }
        return positions;
    }


    public void Save()
    {
        LevelData data = new LevelData(this);
        SaveSystem.SaveLevelData(data);
    }

    public void Load()
    {       
        LevelData data = SaveSystem.LoadLevelData(Laucher.GetCurrentSceneName());

        if (data != null && data.NPCData != null)
        {
            GameObject[] AllNPC = GameObject.FindGameObjectsWithTag("NPC");

            for (int i = 0; i < AllNPC.Length; i++)
            {
                Destroy(AllNPC[i]);
            }

            CharacterDatabase characterDatabase = Resources.Load<CharacterDatabase>("Data/Character Database");
            for (int i = 0; i < data.NPCData.Length; i++)
            {
                GameObject character = characterDatabase.GetCharacter(data.NPCData[i].characterID);
                //Vector3 position = data.NPCData[i].GetPosition();
                //Instantiate(character, position, Quaternion.identity);
            }
        }
    }
}
