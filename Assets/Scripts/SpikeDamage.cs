using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage spikes do to enemies

    private void OnTriggerEnter(Collider other)
    {
        // Log the name of the object that entered the trigger
        Debug.Log("Spike collided with: " + other.gameObject.name);

        // Check if the collided object has an EnemyHealth component
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Apply damage to the enemy
            enemyHealth.TakeDamage(damageAmount);
            Debug.Log("Applying " + damageAmount + " damage to " + other.gameObject.name);
        }
        else
        {
            Debug.Log("No EnemyHealth component found on " + other.gameObject.name);
        }
    }
}
