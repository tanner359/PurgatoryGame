using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Notification_System
{
    public static void Send(string message, string buttonName, Notification.ButtonFunction function)
    {
        GameObject defaultOB = Resources.Load<UI_Data>("Data/UI Data").defaultNotification;
        GameObject newGO = Object.Instantiate(defaultOB, GameObject.FindGameObjectWithTag("Notifications").transform);
        newGO.GetComponent<Notification>().SetAttributes(message, buttonName, function);
    }
}

