using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;



[System.Serializable]
public class MoveInputEvent : UnityEvent<float, float> { }

[System.Serializable]
public class RunInputEvent: UnityEvent<bool> { }

[System.Serializable]
public class ShootInputEvent : UnityEvent<bool> { }

[System.Serializable]
public class JumpInputEvent : UnityEvent<bool> { }

[System.Serializable]
public class AimInputEvent : UnityEvent<float, float> { }






public class InputController : MonoBehaviour
{
   TrooperInputActions controls;
   
   public MoveInputEvent moveInputEvent;
   public RunInputEvent runInputEvent;
   public ShootInputEvent shootInputEvent;
   public JumpInputEvent jumpInputEvent;

   public Vector2 MousePosition;

        void Awake()
    {
        controls = new TrooperInputActions();
        
    }

        private void Update()
        {
        Cursor.visible = false;
        MousePosition = controls.Character.Look.ReadValue<Vector2>();
        }

        private void OnEnable()
    {
        controls.Character.Enable();
        // Move
        controls.Character.WASD.performed += OnMovePerformed;
        controls.Character.WASD.canceled += OnMovePerformed;
        //Sprint
        controls.Character.Sprint.performed += OnSprintPerformed;
        controls.Character.Sprint.canceled += OnSprintPerformed;

        //Shoot
        controls.Character.Shoot.performed += OnShootPerformed;
        controls.Character.Shoot.canceled += OnShootPerformed;
        // Jump
        controls.Character.Jump.performed += OnJumpPerformed;
        controls.Character.Jump.canceled += OnJumpPerformed;


           
        }

        private void OnShootPerformed(InputAction.CallbackContext context)
        {
            bool shootInput = context.ReadValueAsButton();
            shootInputEvent.Invoke(shootInput);
           
        
        }

        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            bool runInput = context.ReadValueAsButton();
            runInputEvent.Invoke(runInput);
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            moveInputEvent.Invoke(moveInput.x, moveInput.y);
   
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            bool jumpInput = context.ReadValueAsButton();
            jumpInputEvent.Invoke(jumpInput);
        }

       

    

}
