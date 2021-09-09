using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemNotify : MonoBehaviour
{
    public TMP_Text message;

    public void SetAttributes(string message)
    {
        this.message.text = message;
    }

    private void OnEnable()
    {
        float lifeTime = 0.2f * message.text.Length;
        if(lifeTime < 3) { lifeTime = 3; }
        Destroy(gameObject, lifeTime);
    }
}
