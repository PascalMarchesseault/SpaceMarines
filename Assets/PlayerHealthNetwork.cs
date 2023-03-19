using UnityEngine;
using System.Collections;
using Unity.Netcode;

public class PlayerHealthNetwork : NetworkBehaviour
{
    public GameObject DestoryPlayer;
    public int startingHealth = 100; // The starting health of the object
    public int currentHealth; // The current health of the object
    public bool isDead = false; // Flag indicating whether the object is dead or not

    void Start()
    {
        currentHealth = startingHealth; // Set the initial health to the starting health
    }


    [ServerRpc(RequireOwnership = false)]
    // Function to take damage
    public void TakeDamageServerRpc(int damageAmount)
    {
        if (isDead)
        {
            return; // If the object is already dead, do nothing
        }

        currentHealth -= damageAmount; // Subtract damage from current health
        if (currentHealth <= 0)
        {
            DieServerRpc(); // If current health reaches 0 or less, call the Die function
        }
    }

    [ServerRpc(RequireOwnership = false)]

    // Function to handle death
    void DieServerRpc()
    {
        isDead = true;
        // Add any death-related logic here, such as disabling the object or playing an animation
        Debug.Log(gameObject.name + " has died."); // Log the death to the console
        Destroy(DestoryPlayer);
    }

}
