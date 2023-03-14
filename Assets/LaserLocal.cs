using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class LaserLocal : MonoBehaviour
{
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
       
        InstantiateHitParticles();
        Destroy(this); 
         
    }

    

    private void InstantiateHitParticles()
    {
        GameObject hitImpact = Instantiate(VfxHit, transform.position, Quaternion.identity);
        hitImpact.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
    }
















}
