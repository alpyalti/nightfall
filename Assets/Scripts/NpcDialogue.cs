using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    public GameObject dialogueText; // Assign the Text GameObject in the Inspector

    private bool isPlayerInRange = false;
    private GameObject player;


    private void Start()
    {
        // Ensure the dialogue text is not visible at the start
        dialogueText.SetActive(false);
    }

    private void Update()
 {
    if (isPlayerInRange && player != null)
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.y = 0; // Keep the rotation in the horizontal plane
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = lookRotation;
    }
 }

 private void OnTriggerEnter(Collider other)
 {
    if (other.CompareTag("Player"))
    {
        dialogueText.SetActive(true);
        player = other.gameObject; // Assign the player reference when they enter the trigger
        isPlayerInRange = true;
    }
 }

 private void OnTriggerExit(Collider other)
 {
    if (other.CompareTag("Player"))
    {
        dialogueText.SetActive(false);
        player = null; // Clear the player reference when they exit the trigger
        isPlayerInRange = false;
    }
 }
}
