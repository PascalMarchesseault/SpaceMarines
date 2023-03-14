using UnityEngine;
using Cinemachine;
using Unity.Netcode;

public class CamTarget : NetworkBehaviour
{
    [SerializeField]private Camera CharacterCamera;
    public Transform playerTransform;
    public Transform aimTransform;
    public float cameraSpeed = 5f;
    public float cameraThreshold = 5f;
    public float screenWidth = 1920f;
    public float screenHeight = 1080f;
    [SerializeField] float distanceFromTop = 10f;
    [SerializeField] float cameraDistanceY = 0f;

    [SerializeField] private GameObject aimObject;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private Vector3 smoothPosition;

    void Awake()
    {
       
       
        aimObject.transform.position = playerTransform.position;
   

    }

    void Update()
    {
       
        if (IsOwner && IsClient)
        {

       
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -CharacterCamera.transform.position.z;
        mousePosition = CharacterCamera.ScreenToWorldPoint(mousePosition);

        Vector3 playerToMouse = playerTransform.position - mousePosition;
        float distance = playerToMouse.magnitude;
        Vector3 targetPosition;

        if (distance > cameraThreshold)
        {
            playerToMouse = Vector3.ClampMagnitude(playerToMouse, cameraThreshold);
            targetPosition = playerTransform.position + playerToMouse;
        }
        else
        {
            Vector3 screenCenter = new Vector3(screenWidth / 2f, screenHeight / 2f + distanceFromTop, 0f);
            Vector3 mouseToScreenCenter = screenCenter - mousePosition;
            mouseToScreenCenter = Vector3.ClampMagnitude(mouseToScreenCenter, screenWidth / 2f - cameraThreshold);
            targetPosition = screenCenter - mouseToScreenCenter;
            targetPosition.y = playerTransform.position.y + cameraDistanceY;
        }

        aimObject.transform.position = Vector3.Lerp(aimObject.transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        vcam.m_Follow = aimObject.transform;
    }

    }

}
