using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public string name;
    public string scene;
    public string characterID;
    public float[] position;

    public NPCData(NPC npc)
    {
        this.name = npc.name;
        this.scene = Laucher.GetCurrentSceneName();

        this.characterID = npc.GetComponent<CharacterID>().characterID;
        this.position = new float[3];

        this.position[0] = npc.transform.position.x;
        this.position[1] = npc.transform.position.y;
        this.position[2] = npc.transform.position.z;
    }
}

//[System.Serializable]
//public struct NPCData
//{
//    public string ID;
//    public float[] position;

//    public NPCData(string ID, Vector3 position)
//    {
//        this.ID = ID;
//        this.position = new float[3];
//        this.position[0] = position.x;
//        this.position[1] = position.y;
//        this.position[2] = position.z;
//    }

//    public Vector3 GetPosition()
//    {
//        return new Vector3(position[0], position[1], position[2]);
//    }

//}

