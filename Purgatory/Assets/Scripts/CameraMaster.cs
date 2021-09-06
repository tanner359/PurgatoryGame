using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMaster : MonoBehaviour
{
    public static CameraMaster instance;

    public CinemachineVirtualCamera playerCam;

    private void Awake()
    {
        instance = this;
    }

    public void SetPlayerCamTarget(Transform transform)
    {
        playerCam.Follow = transform;
    }

    public Transform GetPlayerCamTarget()
    {
        return playerCam.Follow;
    }
}
