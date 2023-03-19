using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    // Variables pour les différents systèmes de porte
    public enum DoorType { Button, Detection, Mouse, Console }
    public DoorType doorType;

    // Variables pour les points de vie de la porte
    public int maxHealth = 100;
    public int currentHealth;

    // Variables pour le mouvement de la porte
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpen = false;

    // Variables pour le système de détection automatique
    public Transform detectionPoint;
    public float detectionRadius = 1f;
    public LayerMask detectionMask;

    // Référence au composant Animator de la porte
    private Animator animator;

    // Référence au composant AudioSource de la porte
    private AudioSource audioSource;

    // Variables pour la porte physique
    private bool isBeingPushed = false;
    public float maxPushDistance = 2f;

    // Variables pour la porte coulissante
    private bool isSliding = false;
    public Vector3 closedPosition;
    public Vector3 openPosition;
    public float slideSpeed = 2f;

    void Start()
    {
        // Initialisation des variables
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        closedPosition = transform.position;
    }

    void Update()
    {
        // Vérification du système de porte choisi
        switch (doorType)
        {
            case DoorType.Button:
                if (Input.GetButtonDown("Action"))
                {
                    ToggleDoor();
                }
                break;
            case DoorType.Detection:
                if (Physics.CheckSphere(detectionPoint.position, detectionRadius, detectionMask))
                {
                    ToggleDoor();
                }
                break;
            case DoorType.Mouse:
                float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                if (distance < 5f)
                {
                    float mouseX = Input.GetAxis("Mouse X");
                    float angle = transform.rotation.eulerAngles.y;
                    angle += mouseX * openSpeed;
                    angle = Mathf.Clamp(angle, 0f, openAngle);
                    Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
                    transform.rotation = rotation;
                    isOpen = (angle == openAngle);
                }
                break;
            case DoorType.Console:
                // Code pour le système de porte avec une console ou un bouton éloigné
                break;
        }

        // Vérification de la porte physique
        if (!isOpen && Vector3.Distance(transform.position, Camera.main.transform.position) < maxPushDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isBeingPushed = true;
            }
        }

        // Vérification de la porte coulissante
        if (isSliding)
        {
            transform.position = Vector3.Lerp(transform.position, openPosition, slideSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, closedPosition, slideSpeed * Time.deltaTime);
        }
    }

    // Fonction pour ouvrir/fermer la porte
    void ToggleDoor()
    {
        if (animator != null)
        {
            animator.SetBool("isOpen", !animator.GetBool("isOpen"));
        }
        else if (!isSliding)
        {
            isOpen = !isOpen;
            audioSource.Play();
        }
        if (isSliding)
        {
            isSliding = false;
        }
    }
}