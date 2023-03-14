using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;


public class CameraPriority : NetworkBehaviour
{

    [SerializeField] private CinemachineVirtualCamera playerCamera;
   



    private void Start()
    {
        playerCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (IsOwner && IsClient)
        {
            playerCamera.Priority = 14;

        }
    }
}
