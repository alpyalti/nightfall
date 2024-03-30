using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    private float currentHealth;
    public SkinnedMeshRenderer enemyRenderer; // Assign this in the Inspector
    private Color originalColor;
    public float flashDuration = 0.1f; // Duration of the flash effect

    void Start()
    {
        currentHealth = maxHealth;
        if (enemyRenderer == null)
            enemyRenderer = GetComponentInChildren<SkinnedMeshRenderer>(); // Automatically get the SkinnedMeshRenderer component from children if not assigned
        originalColor = enemyRenderer.material.color; // Store the original material color
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took damage, current health: " + currentHealth);
        FlashRed();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void FlashRed()
    {
        enemyRenderer.material.color = Color.red; // Change material color to red
        Invoke("ResetColor", flashDuration); // Reset color after a short duration
    }

    void ResetColor()
    {
        enemyRenderer.material.color = originalColor; // Reset to the original color
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        onEnemyDied?.Invoke();
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    // Delegate for handling death
    public delegate void EnemyDied();
    // Event for death
    public event EnemyDied onEnemyDied;
}
