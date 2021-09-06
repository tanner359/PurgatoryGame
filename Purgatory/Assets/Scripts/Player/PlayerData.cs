using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int bulletCount;

    public PlayerData(Revolver revolver)
    {
        bulletCount = revolver.bulletCount;
    }
}
