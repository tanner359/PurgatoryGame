using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, ISavable, IControllable
{
    public bool isPossessed;

    public Material material;
    public void Save()
    {
        //if (!enabled) { return; }
        NPCData data = new NPCData(this);
        SaveSystem.SaveNPCData(data);
    }
    public void EnableControl(Player player)
    {
        player.currentCharacter = gameObject;
        enabled = false;
        isPossessed = true;
    }
    public void RevokeControl(Player player)
    {
        player.currentCharacter = null;
        enabled = true;
        isPossessed = false;
    }

    private void Awake()
    {
        NPCData data = SaveSystem.LoadNPCData(Laucher.GetCurrentSceneName(), gameObject.name);
        if (data != null)
        {
            if (data.isPossessed) { Destroy(gameObject); return; }
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
    }

    private void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        if (Player.isTargeting && enabled){ ToggleOutline(true); }
    }
    private void OnMouseOver()
    {
        if (!Player.isTargeting && material.GetInt("Is_Active") == 1) { ToggleOutline(false); }
        else if (enabled && Player.isTargeting && material.GetInt("Is_Active") == 0) { ToggleOutline(true); }
    }
    private void OnMouseExit()
    {
        ToggleOutline(false);
    }

    private void OnMouseDown()
    {
        if (Player.isTargeting)
        {
            if (Player.instance.revolver.bulletCount > 0)
            {
                Player.instance.SwitchPlayer(gameObject);
                ToggleOutline(false);
                enabled = false;
            }
            else
            {
                Notification_System.Send_SystemNotify("No Ammo", Color.red);
            }
        }
    }

    public void ToggleOutline(bool state)
    {
        if (state)
        {
            material.SetInt("Is_Active", 1);
        }
        else
        {
            material.SetInt("Is_Active", 0);
        }
    }
}
