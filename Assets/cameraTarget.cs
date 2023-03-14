using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class cameraTarget : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;



    public Vector2 MousePosition;
    public Vector3 worldPosition;


    TrooperInputActions controls;
    // Update is called once per frame

    void Awake()
    {
        controls = new TrooperInputActions();
    }

    private void OnEnable()
    {
        controls.Character.Enable();
    }

    void Update()
    {
        

        MousePosition = controls.Character.Look.ReadValue<Vector2>();
        Vector2 mousePos = cam.ScreenToWorldPoint(MousePosition);

        Plane plane = new Plane(Vector3.up, 0);

        float distance;

        Ray ray = Camera.main.ScreenPointToRay(MousePosition);
        if(plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        
        Vector3 targetPos = (player.position + worldPosition) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetPos;


    }


}
