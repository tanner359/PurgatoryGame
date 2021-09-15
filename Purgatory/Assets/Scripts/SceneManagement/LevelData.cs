using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public NPCData[] NPCData;

    //public string[] active_NPCs;
    //public float[] positions;

    public LevelData(LevelManager controller)
    {
        levelName = Laucher.GetCurrentSceneName();
        string[] active_NPCs = controller.GetActiveNPCs();
        Vector3[] positions = controller.GetActiveNPCPositions();

        if (active_NPCs.Length > 0)
        {
            NPCData = new NPCData[active_NPCs.Length];

            for (int i = 0; i < active_NPCs.Length; i++)
            {
                //NPCData[i] = new NPCData(active_NPCs[i], positions[i]);
            }
        }
    }
}
