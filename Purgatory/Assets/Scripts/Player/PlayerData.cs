using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public string currentScene;
    public float[] position;
    public string characterID;
    public int bulletCount;

    public PlayerData(Player player, Revolver revolver)
    {
        currentScene = SceneManager.GetActiveScene().name;
        bulletCount = revolver.bulletCount;
        characterID = player.currentPlayer.GetComponent<CharacterID>().ID;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }
}
