using UnityEngine;
using System.Collections;

public class AxeScript : MonoBehaviour
{
    public Collider axeCollider;
    public KeyCode attackKey = KeyCode.Mouse0;
    public float damageAmount = 10f;
    public float attackDelay = 0.5f; // Delay before the damage is dealt
    public float attackCooldown = 1.0f; // Cooldown period after the attack

    private bool isReadyToAttack = true; // Indicates if the player can initiate an attack

    void Start()
    {
        if (axeCollider != null)
            axeCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey) && isReadyToAttack)
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        isReadyToAttack = false; // Player starts the attack, disable further attacks

        // Optionally enable the collider immediately if you want collision detection during the wind-up
        if (axeCollider != null)
            axeCollider.enabled = true;

        yield return new WaitForSeconds(attackDelay); // Wait for the attack delay

        // Perform the attack logic here. Damage is dealt after the delay.

        // Disable the collider after the delay if it was enabled at the start
        if (axeCollider != null)
            axeCollider.enabled = false;

        yield return new WaitForSeconds(attackCooldown - attackDelay); // Wait for the cooldown period after the attack

        isReadyToAttack = true; // Player can attack again after the cooldown
    }

    private void OnTriggerEnter(Collider other)
    {
        // Damage is dealt during the delay period, so check for enemy collision here
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy with the axe!");

            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
}
