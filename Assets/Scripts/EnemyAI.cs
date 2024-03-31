using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform building;
    public float moveSpeed = 2f;
    public float attackRange;
    public float attackDelay = 1f;
    public float attackDamage = 10f;

    private bool isAttacking = false;

    private void Update()
    {
        if (building == null) return;

        float distanceToBuilding = Vector3.Distance(transform.position, building.position);

        // Check if the enemy is within attack range
        if (distanceToBuilding <= attackRange)
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackBuilding());
            }
        }
        else
        {
            MoveTowardsBuilding();
        }
    }

    private void MoveTowardsBuilding()
    {
        // Move towards the building if not in attack range
        Vector3 direction = (building.position - transform.position).normalized;
        // Zero out the y component to avoid affecting vertical position
        direction.y = 0;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Optionally, make the enemy face the building
        transform.LookAt(new Vector3(building.position.x, transform.position.y, building.position.z));
    }

    IEnumerator AttackBuilding()
    {
        isAttacking = true;

        // Attack logic here (e.g., reduce building health)
        Debug.Log("Attacking the building with " + attackDamage + " damage.");
        // Simulate attacking the building
        building.GetComponent<BuildingHealth>().TakeDamage(attackDamage); // Uncomment and use if you have a BuildingHealth script

        yield return new WaitForSeconds(attackDelay);

        isAttacking = false; // Reset attack state
    }

    public void SetTargetBuilding(Transform targetBuilding)
    {
        building = targetBuilding;
    }
}
