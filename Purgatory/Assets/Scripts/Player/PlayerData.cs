using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string characterID;
    public int bulletCount;

    public PlayerData(Player player, Revolver revolver)
    {
        bulletCount = revolver.bulletCount;
        characterID = player.currentPlayer.GetComponent<CharacterID>().ID;
    }
}
