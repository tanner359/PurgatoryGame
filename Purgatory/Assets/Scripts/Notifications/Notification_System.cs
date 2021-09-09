using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Notification_System
{
    public static void Send_ActionWindow(string message, string buttonName, ActionWindow.ButtonFunction function)
    {
        GameObject defaultOB = Resources.Load<UI_Data>("Data/UI Data").defaultActionWindow;
        GameObject newGO = Object.Instantiate(defaultOB, GameObject.FindGameObjectWithTag("Notifications").transform);
        newGO.GetComponent<ActionWindow>().SetAttributes(message, buttonName, function);
    }

    public static void Send_SystemNotify(string message)
    {
        GameObject defaultOB = Resources.Load<UI_Data>("Data/UI Data").defaultSystemNotify;
        GameObject newGO = Object.Instantiate(defaultOB, GameObject.FindGameObjectWithTag("Notifications").transform);
        newGO.GetComponent<SystemNotify>().SetAttributes(message);
    }
}

