using UnityEngine;
using System.Collections;

public class SpikeDamage : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage spikes do to enemies
    public float expandedScaleZ = 500f; // The expanded scale size
    private float originalScaleZ; // To store the original scale
    private Coroutine scaleCoroutine; // Reference to the running coroutine, if any

    private void Start()
    {
        // Store the original Z scale of the spike
        originalScaleZ = transform.localScale.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spike collided with: " + other.gameObject.name);

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount);
            Debug.Log("Applying " + damageAmount + " damage to " + other.gameObject.name);

            // Start the coroutine to expand the spike
            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine); // Stop the current coroutine if it's already running
            }
            scaleCoroutine = StartCoroutine(ExpandSpike());
        }
        else
        {
            Debug.Log("No EnemyHealth component found on " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the enemy exits the spike's trigger, start shrinking the spike back to its original size
        if (other.GetComponent<EnemyHealth>() != null)
        {
            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine); // Stop the expand coroutine if it's running
            }
            scaleCoroutine = StartCoroutine(ShrinkSpike());
        }
    }

    private IEnumerator ExpandSpike()
    {
        float duration = 0.1f; // Duration over which the spike expands
        float time = 0;

        while (time < duration)
        {
            // Lerp the scale over time
            float scaleZ = Mathf.Lerp(originalScaleZ, expandedScaleZ, time / duration);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scaleZ);

            time += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the spike reaches the expanded size
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, expandedScaleZ);
    }

    private IEnumerator ShrinkSpike()
    {
        float duration = 1f; // Duration over which the spike shrinks back
        float time = 0;

        while (time < duration)
        {
            // Lerp the scale over time
            float scaleZ = Mathf.Lerp(expandedScaleZ, originalScaleZ, time / duration);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scaleZ);

            time += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the spike reaches the original size
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, originalScaleZ);
    }
}
