using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerShooting : NetworkBehaviour
{
    [SerializeField]
    private Transform bulletDirection;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _projectileLocal;
    [SerializeField] private float _cooldown = 0.5f;
   
    [SerializeField] private List<GameObject> spawnedLaser = new List<GameObject>();

    private float _lastFired = float.MinValue;
    private bool _fired;

    

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetMouseButton(0) && _lastFired + _cooldown < Time.time)
        {
            _lastFired = Time.time;
            var dir = transform.forward;


            ShootServerRpc();
            ShootLocal();

            // UI indicator
            StartCoroutine(ToggleLagIndicator());
        }
    }

    private void ShootLocal()
    {
        if(!IsServer || !IsHost)
        {
            GameObject go = Instantiate(_projectileLocal, bulletDirection.position, bulletDirection.rotation);
        }
    }


  

    [ServerRpc]
    private void ShootServerRpc()
    {
        if (IsServer)
        {
        GameObject go = Instantiate(_projectile, bulletDirection.position, bulletDirection.rotation);
        spawnedLaser.Add(go);
        go.GetComponent<Laser>().parent = this;
        go.GetComponent<NetworkObject>().Spawn();
        }

        

    }

    [ServerRpc(RequireOwnership =false)]
    public void DestroyServerRpc()
    {

        GameObject toDestroy = spawnedLaser[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedLaser.Remove(toDestroy);
        Destroy(toDestroy);
    }

    // UI Pour SreenShootInfo

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (_fired) GUILayout.Label("FIRED LOCALLY");

        GUILayout.EndArea();
    }

    private IEnumerator ToggleLagIndicator()
    {
        _fired = true;
        yield return new WaitForSeconds(0.2f);
        _fired = false;
    }


    public class PlayerShootingFxLocal : MonoBehaviour
    {
        [SerializeField]
        private Transform bulletDirection;
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _cooldown = 0.5f;

        private void Update()
        {
            GameObject go = Instantiate(_projectile, bulletDirection.position, bulletDirection.rotation);
        }
        
    }
}