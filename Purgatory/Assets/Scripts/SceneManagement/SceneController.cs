using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public string sceneName;

    public Transform ActiveCharacters;


    private void OnEnable()
    {     
        LoadScene();
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }  

    public string[] GetActiveNPCs()
    {
        string[] IDs = new string[GameObject.FindGameObjectsWithTag("NPC").Length];
        Debug.Log("NPC count:" + GameObject.FindGameObjectsWithTag("NPC").Length);

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

    public float[] GetActiveNPCPositions()
    {
        float[] positions = new float[GameObject.FindGameObjectsWithTag("NPC").Length * 3];
        int k = 0;

        for (int i = 0; i < ActiveCharacters.childCount; i++)
        {
            Transform character = ActiveCharacters.GetChild(i);
            if (character.CompareTag("NPC"))
            {
                positions[k] = character.position.x;
                positions[k + 1] = character.position.y;
                positions[k + 2] = character.position.z;

                k += 3;
            }
        }
        return positions;
    }


    public void SaveScene()
    {
        SceneData data = new SceneData(this);
        SaveSystem.SaveSceneData(data);
    }

    public void LoadScene()
    {
        SceneData data = SaveSystem.LoadSceneData(SceneManager.GetActiveScene().name);

        if (data != null)
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
            int k = 0;
            for (int i = 0; i < data.active_NPCs.Length; i++)
            {
                GameObject character = characterDatabase.GetCharacter(data.active_NPCs[i]);
                Vector3 position = new Vector3(data.positions[k], data.positions[k + 1], data.positions[k + 2]);
                Instantiate(character, position, Quaternion.identity, ActiveCharacters);
                k += 3;
            }
        }
    }
}
