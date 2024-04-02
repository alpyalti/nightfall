using UnityEngine;

public class AxeScript : MonoBehaviour
{
    public Collider axeCollider;
    public float damageAmount = 10f;
    public AudioClip attackSound;
    
    private AudioSource audioSource;

    void Start()
    {
        axeCollider.enabled = false; // Ensure the collider is initially disabled
        audioSource = GetComponent<AudioSource>();
    }

    public void EnableAxeCollider()
    {
        axeCollider.enabled = true;
        PlayAttackSound();
    }

    public void DisableAxeCollider()
    {
        axeCollider.enabled = false;
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
}
