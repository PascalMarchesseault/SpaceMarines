using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DestroyOverTime : NetworkBehaviour
{

    [SerializeField] private float time;

  

    [ServerRpc(RequireOwnership = false)]

     private void DestroyParticlesServerRpc()
     {
         GetComponent<NetworkObject>().Despawn();
         Destroy(gameObject, time);
     }
}
