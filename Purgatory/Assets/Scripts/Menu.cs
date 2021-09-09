using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.SavePlayerData(new PlayerData(Player.instance, Player.instance.revolver));
        SaveSystem.SaveSceneData(new SceneData(SceneController.instance));
        Notification_System.Send_SystemNotify("The game has been saved");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
