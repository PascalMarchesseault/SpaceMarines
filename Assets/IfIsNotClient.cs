using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class IfIsNotClient : NetworkBehaviour
{

    [SerializeField] private GameObject CharacterCamera;
    [SerializeField] private CinemachineVirtualCamera playerCamera;

    [SerializeField] private GameObject PlayerAim;
    [SerializeField] private GameObject MouseAim;

    private void Start()
    {
        if(!IsClient && !IsOwner)
        {
        }


        if (IsOwner && IsClient)
        {
            CharacterCamera.GetComponent<AudioListener>().enabled = true;
            playerCamera.Priority = 14;

            PlayerAim.GetComponent<MeshRenderer>().enabled = true;
            MouseAim.GetComponent<MeshRenderer>().enabled = true;
        }


        
    }

}
