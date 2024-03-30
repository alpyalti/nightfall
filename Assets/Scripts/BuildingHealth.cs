using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    public float maxHealth = 1000f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
{
    currentHealth -= amount;
    Debug.Log("Building Health: " + currentHealth);

    if (currentHealth <= 0f)
    {
        DestroyBuilding();
    }
}

    void DestroyBuilding()
    {
        // Add logic for what happens when the building is destroyed
        Debug.Log("Building Destroyed!");
        Destroy(gameObject); // This removes the building from the scene
    }
}