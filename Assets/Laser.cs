using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


[RequireComponent(typeof(Rigidbody))]
public class Laser : NetworkBehaviour
{
    public int damage;
    public PlayerShooting parent;

    [SerializeField] private GameObject VfxHit;
    [SerializeField] private float m_Speed = 10f;   // this is the projectile's speed


    [SerializeField] private float m_Lifespan = 3f; // this is the projectile's lifespan (in seconds)


    private Rigidbody m_Rigidbody;




    // Start is called before the first frame update
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = m_Rigidbody.transform.forward * m_Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner) return;
        InstantiateHitParticlesServerRpc();
        parent.DestroyServerRpc();     
    }

    [ServerRpc]

    private void InstantiateHitParticlesServerRpc()
    {
        GameObject hitImpact = Instantiate(VfxHit, transform.position, Quaternion.identity);
        hitImpact.GetComponent<NetworkObject>().Spawn();
        hitImpact.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
    }
















}
