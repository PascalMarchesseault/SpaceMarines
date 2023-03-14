using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class PlayerController : NetworkBehaviour
{
    [SerializeField] private Camera CharacterCamera;
    [SerializeField] private bool IsRunning;
    [SerializeField] private float RunSpeed;

    [SerializeField] private List<GameObject> spawnedLaser = new List<GameObject>();
   


    // Aim components //
    TrooperInputActions controls;
    Vector2 MousePosition;

    //Player health
    public float playerHealth;

    [SerializeField]
    float moveSpeed = 5.0f;

    float horizontal;
    float vertical;

    [SerializeField]
    private float movementVelocity = 3f;
        
    [SerializeField] LayerMask _aimLayerMask;

    Animator _animator;

    private void Awake()
    {
        controls = new TrooperInputActions();
        _animator = GetComponent<Animator>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            this.enabled = false;
            GetComponent<PlayerController>().enabled = false;
          
        }
        
    }

    private void OnEnable()
    {
        controls.Character.Enable();
    }

    private void Update()
    {
        if (!IsOwner) return;
        
        

        MousePosition = controls.Character.Look.ReadValue<Vector2>();
        AimTowardMouse();


        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        //Animation
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);

        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);

        

    }


    public void OnMoveInput(float horizontal, float vertical)
    {
        if (!IsOwner) return;
        this.vertical = vertical;
        this.horizontal = horizontal;
        Debug.Log($"player controller Move INput:{vertical}, {horizontal} ");
        
       
    }

    public void OnShoot(bool shootInput)
    {
      
       
    }

    public void OnJump(bool jumpInput)
    {
        if (!IsOwner) return;
        Debug.Log("Jumped");
    }

    public void OnRun(bool runInput)
    {
        if (!IsOwner) return;
        PlayerRun();
        Debug.Log("Run");
    }

    private void FixedUpdate()
    {
        if (!IsRunning)
        {
            _animator.SetBool("isRunning", false);
            Vector3 moveDirection = Vector3.forward * vertical + Vector3.right * horizontal;

            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        if (IsRunning)
        {
            _animator.SetBool("isRunning", true);
            Vector3 moveDirection = Vector3.forward * vertical * RunSpeed + Vector3.right * horizontal * RunSpeed;

            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

    }

   void AimTowardMouse()
    {
        if (!IsOwner) return;
        Ray ray = CharacterCamera.ScreenPointToRay(MousePosition);
        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            var _direction = hitInfo.point - transform.position;
            _direction.y = 0f;
            _direction.Normalize();
            transform.forward = _direction;
        }
    }
             
    private void PlayerRun()
    {
        IsRunning = !IsRunning;

        if (IsRunning)
        {

        }
        Debug.Log("Running");
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0) Invoke(nameof(DestroyPlayer), 0.5f);
    }
    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }


  
}
