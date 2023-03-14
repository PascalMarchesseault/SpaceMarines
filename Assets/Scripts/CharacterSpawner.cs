using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterSpawner : NetworkBehaviour
{

    [SerializeField] Transform []Spawns;
    [SerializeField] private CharacterDatabase characterDatabase;
    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        foreach (var client in HostManager.Instance.ClientData)
        {
            var character = characterDatabase.GetCharacterById(client.Value.characterId);
            if(character != null)
            {
                int randomIndex = Random.Range(0, Spawns.Length);
                Transform Spawn = Spawns[randomIndex];
                
                var characterInstance = Instantiate(character.GameplayPrefab, Spawn.position, Quaternion.identity);
                characterInstance.SpawnAsPlayerObject(client.Value.clientId);
            }
        }
    }
}
