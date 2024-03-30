using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    private float currentHealth;

    // Declare a delegate type for handling death
    public delegate void EnemyDied();
    // Create an event of this delegate type
    public event EnemyDied onEnemyDied;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took damage, current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        // Trigger the event
        onEnemyDied?.Invoke();
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
