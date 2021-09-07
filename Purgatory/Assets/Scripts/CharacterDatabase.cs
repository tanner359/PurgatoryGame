using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Database", menuName = "Character Database")]
public class CharacterDatabase : ScriptableObject
{
    public List<GameObject> characters = new List<GameObject>();

    public GameObject GetCharacter(string ID)
    {
        for(int i = 0; i < characters.Count; i++)
        {
            if(characters[i].GetComponent<CharacterID>().ID == ID)
            {
                return characters[i];
            }
        }
        Debug.Log("Character not found");
        return null;       
    }
}

