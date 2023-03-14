using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyOverTimeLocal : MonoBehaviour
{

    [SerializeField] private float time;


    private void Update()
    {
        Destroy(gameObject, time);
    }



}
