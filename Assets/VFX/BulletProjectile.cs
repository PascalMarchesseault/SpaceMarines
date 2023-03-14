using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
 

    [SerializeField] private Transform VfxHitGreen;
    [SerializeField] private Transform VfxHitRed;
  


    
    private void OnTriggerEnter(Collider other)
    {
        

      
       Instantiate(VfxHitGreen, transform.position, Quaternion.identity);
           
       

       
        Destroy(gameObject);
    }
}
