using UnityEngine;

public class AxeScript : MonoBehaviour
{
    public Collider axeCollider; // Assign this in the Inspector
    public KeyCode attackKey = KeyCode.Mouse0; // Default to left mouse button
    public float damageAmount = 10f; // The amount of damage the axe does to an enemy

    void Start()
    {
        // Ensure the collider is disabled at the start
        if(axeCollider != null)
            axeCollider.enabled = false;
    }

    void Update()
    {
        // Detect attack input
        if (Input.GetKeyDown(attackKey))
        {
            StartAttack();
        }
        else if (Input.GetKeyUp(attackKey))
        {
            EndAttack();
        }
    }

    void StartAttack()
    {
        // Enable the axe's collider to detect hits
        if(axeCollider != null)
            axeCollider.enabled = true;
    }

    void EndAttack()
    {
        // Disable the collider after the attack
        if(axeCollider != null)
            axeCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy")) // Ensure your enemy GameObjects are tagged as "Enemy"
        {
            Debug.Log("Hit an enemy!");

            // Call a method to damage the enemy here
            other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
        }
    }
}
