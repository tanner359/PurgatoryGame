using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Interaction Data",menuName = "Interaction Data")]
public class InteractionData : ScriptableObject
{
    public GameObject defaultInteractionText;
    public GameObject defaultTextCanvas;
    public Joystick_UIElements[] JoystickUI;
    public static InteractionData Get(){return Resources.Load<InteractionData>("Data/Interaction Data");}

    public Joystick_UIElements GetJoystickUI(string target_platform)
    {
        foreach(Joystick_UIElements j in JoystickUI)
        {
            if(j.joystickName == target_platform)
            {
                return j;
            }
        }
        Debug.Log("Target Platform Not Found!");
        return new Joystick_UIElements(null, null);
    }
}

[System.Serializable]
public struct Joystick_UIElements
{
    public string joystickName;
    public Sprite interact;

    public Joystick_UIElements(string name, Sprite interact)
    {
        this.joystickName = name;
        this.interact = interact;
    }
}

