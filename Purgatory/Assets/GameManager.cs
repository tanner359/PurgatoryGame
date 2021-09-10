using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void OnEnable()
    {
        instance = this;
    }

    public Revolver revolver;
    public Player player;
    public Transform activeCharacters;
}
