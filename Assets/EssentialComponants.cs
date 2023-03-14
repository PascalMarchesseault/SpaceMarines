using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EssentialComponants : NetworkBehaviour
{
    public GameObject CameraHandler;
    public GameObject CharacterAimComponents;



    private void Awake()
    {
      
        Instantiate(CameraHandler);
        Instantiate(CharacterAimComponents);
       
    }

    private void Update()
    {
       
        Destroy(this.gameObject.GetComponent<EssentialComponants>());
    }



}


