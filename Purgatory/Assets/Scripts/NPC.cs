using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Material material;


    private void OnMouseEnter()
    {
        Debug.Log("Mouse over something");
        ToggleOutline(true);
    }

    private void OnMouseExit()
    {
        ToggleOutline(false);
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
