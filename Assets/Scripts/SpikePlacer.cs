using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpikePlacer : MonoBehaviour
{
    public GameObject spikePrefab; // Assign the actual spike prefab in the Inspector
    public GameObject spikeGhostPrefab; // Assign the ghost (preview) spike prefab in the Inspector
    public int maxSpikes = 5; // Maximum number of spikes the player can place
    public Text spikesLeftText;

    private GameObject currentGhost; // Current ghost instance for previewing placement
    private int currentSpikes; // Current number of spikes placed

    void Update()
    {
        // Toggle placement mode with 'X'
        if (Input.GetKeyDown(KeyCode.X) && currentSpikes < maxSpikes)
        {
            if (currentGhost == null && currentSpikes < maxSpikes)
            {
                // Instantiate the ghost spike at the player's position with a rotation of -90 degrees on the X-axis
                currentGhost = Instantiate(spikeGhostPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            }
            else if (currentGhost != null)
            {
                // Place the spike at the ghost's position with a rotation of -90 degrees on the X-axis
                Instantiate(spikePrefab, currentGhost.transform.position, Quaternion.Euler(-90f, 0f, 0f));
                Destroy(currentGhost); // Remove the ghost preview
                currentSpikes++; // Increment the spike count
                UpdateSpikesLeftText();
            }
        }

        // Cancel placement with right mouse click
        if (Input.GetMouseButtonDown(1) && currentGhost != null)
        {
            Destroy(currentGhost); // Destroy the ghost preview
        }

        // Move the ghost to follow the cursor or player aim
        if (currentGhost != null)
        {
            UpdateGhostPosition();
        }
    }

    void UpdateSpikesLeftText()
    {
    int spikesLeft = maxSpikes - currentSpikes;
    spikesLeftText.text = "Spike (X): " + spikesLeft;
    }

    void UpdateGhostPosition()
    {
        // Raycast from the camera to update the ghost spike's position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            currentGhost.transform.position = hit.point; // Move the ghost to the hit point
        }
    }
}
