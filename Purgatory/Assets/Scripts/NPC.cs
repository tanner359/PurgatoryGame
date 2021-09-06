using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Material material;


    private void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        if (Controller.isTargeting && enabled){ ToggleOutline(true); }
    }

    private void OnMouseExit()
    {
        if (Controller.isTargeting && enabled) { ToggleOutline(false); }
    }

    private void OnMouseDown()
    {
        if (Controller.isTargeting) 
        {
            if(Controller.instance.revolver.bulletCount > 0)
            {
                Controller.instance.SwitchPlayer(gameObject);
                ToggleOutline(false);
                enabled = false;
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
