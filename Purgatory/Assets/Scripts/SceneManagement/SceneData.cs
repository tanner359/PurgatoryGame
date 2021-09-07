using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public string[] active_NPCs;
    public float[] positions;
    public SceneData(SceneController controller)
    {
        sceneName = controller.sceneName;
        active_NPCs = controller.GetActiveNPCs();
        positions = controller.GetActiveNPCPositions();
    }
}


// Get string[] CharactersID's and float[] positions for each => save

// record changes => apply when returned
// record current list NPC's and there POS, OnLoad: compare list of current characters to saved list, remove any that do not exist.
