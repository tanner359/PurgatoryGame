using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    public delegate void ButtonFunction();

    public TMP_Text message;
    public TMP_Text buttonText;
    public ButtonFunction buttonFunction;

    public void SetAttributes(string message, string buttonText, ButtonFunction buttonFunction)
    {
        this.message.text = message;
        this.buttonText.text = buttonText;
        this.buttonFunction = buttonFunction;
    }

    public void CustomButtonFunction()
    {
        buttonFunction.Invoke();
    }

    public void CloseNotification(float delay)
    {
        Destroy(gameObject, delay);
    }
}
