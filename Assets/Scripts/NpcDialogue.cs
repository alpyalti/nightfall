using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    public GameObject dialogueText; // Assign the Text GameObject in the Inspector

    private void Start()
    {
        // Ensure the dialogue text is not visible at the start
        dialogueText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player GameObject is tagged as "Player"
        {
            // Show the dialogue text when the player is close enough
            dialogueText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the dialogue text when the player leaves
            dialogueText.SetActive(false);
        }
    }
}
