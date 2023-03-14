using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class CursorAim : NetworkBehaviour
{
    

    [SerializeField]  bool CharacterAim;
    public Transform objectToAim;
    public Camera gameCamera;

    public LayerMask ignoreLayers;

    [SerializeField] private float MoveSpeed = 1;

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
          

            Ray ray = gameCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;

            if (CharacterAim)
            {
                if(IsClient && IsOwner)
            {
                if (Physics.Raycast(ray, out hitInfo, ignoreLayers))
                {
                    objectToAim.position = Vector3.MoveTowards(objectToAim.position, hitInfo.point, MoveSpeed * Time.deltaTime);
                    objectToAim.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                }
            }

             
            };

            if (!CharacterAim)
            {

                if(IsClient && IsOwner)
            {
                if (Physics.Raycast(ray, out hitInfo, ignoreLayers))
                {
                    objectToAim.position = hitInfo.point;
                    objectToAim.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                }
            }
               
            }
        

    }
}
