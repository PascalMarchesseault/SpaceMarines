using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraHandler : NetworkBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!IsClient)
        {
            this.gameObject.GetComponent<AudioListener>().enabled = false;
        }
    }

    
}
