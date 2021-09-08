using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public NPCData[] NPCData;

    //public string[] active_NPCs;
    //public float[] positions;

    public SceneData(SceneController controller)
    {
        sceneName = controller.sceneName;
        string[] active_NPCs = controller.GetActiveNPCs();
        Vector3[] positions = controller.GetActiveNPCPositions();

        if (active_NPCs.Length > 0)
        {
            NPCData = new NPCData[active_NPCs.Length];

            for (int i = 0; i < active_NPCs.Length; i++)
            {
                NPCData[i] = new NPCData(active_NPCs[i], positions[i]);
            }
        }
    }
}

[System.Serializable]
public struct NPCData
{
    public string ID;
    public float[] position;

    public NPCData(string ID, Vector3 position)
    {
        this.ID = ID;
        this.position = new float[3];
        this.position[0] = position.x;
        this.position[1] = position.y;
        this.position[2] = position.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

}
