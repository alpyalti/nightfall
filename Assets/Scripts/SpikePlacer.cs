using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpikePlacer : MonoBehaviour
{
    public GameObject spikePrefab;
    public GameObject spikeGhostPrefab;
    public int maxSpikes = 5;
    public LayerMask groundLayer; // Make sure this matches your ground layer
    public Text spikesLeftText; // UI Text to display the number of spikes left

    private GameObject currentGhost;
    private int currentSpikes;
    private Material ghostMaterial;
    private bool isGhostColliding = false; // Flag for ghost spike collision

    private void Start()
    {
        UpdateSpikesLeftText(); // Initial update of the UI text
    }

    void Update()
    {
        // Toggle ghost spike with 'X'
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentGhost == null && currentSpikes < maxSpikes)
            {
                currentGhost = Instantiate(spikeGhostPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                ghostMaterial = currentGhost.GetComponent<Renderer>().material;
                // Add a Collider component if your ghost prefab doesn't have one
                if (!currentGhost.GetComponent<Collider>())
                {
                    currentGhost.AddComponent<BoxCollider>();
                }
                currentGhost.GetComponent<Collider>().isTrigger = true; // Ensure the Collider is set as a trigger
            }
        }

        if (currentGhost != null)
        {
            UpdateGhostPosition();
            // Place the spike with left mouse click
            if (Input.GetMouseButtonDown(0) && IsPlacementValid())
            {
                PlaceSpike();
            }
        }

        // Cancel placement with right mouse click
        if (Input.GetMouseButtonDown(1) && currentGhost != null)
        {
            Destroy(currentGhost);
        }
    }

   void UpdateGhostPosition()
 {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    // First, check if the ghost spike hits the ground
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
    {
        // Then, perform an additional check to see if the ghost spike is overlapping with any non-placeable objects
        // Adjust the layerMask to include layers of objects that should block spike placement
        LayerMask nonPlaceableLayers = LayerMask.GetMask("NonPlaceable");
        bool isOverlappingNonPlaceable = Physics.CheckSphere(hit.point, 0.5f, nonPlaceableLayers);

        // If the ghost spike is not overlapping with non-placeable objects, it's a valid placement position
        if (!isOverlappingNonPlaceable)
        {
            currentGhost.transform.position = hit.point;
            SetGhostColor(Color.green);
            isGhostColliding = false;
        }
        else
        {
            SetGhostColor(Color.red);
            isGhostColliding = true;
        }
    }
    else
    {
        SetGhostColor(Color.red);
        isGhostColliding = true;
    }
 }


    bool IsPlacementValid()
    {
        // Ensure the placement is valid (green color) and the ghost is not colliding with other objects
        return ghostMaterial.color == Color.green && !isGhostColliding;
    }

    void SetGhostColor(Color color)
    {
        if (ghostMaterial != null)
        {
            ghostMaterial.color = color;
        }
    }

    void PlaceSpike()
    {
        Instantiate(spikePrefab, currentGhost.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(currentGhost); // Remove the ghost preview
        currentSpikes++; // Increment the spike count
        UpdateSpikesLeftText(); // Update the UI text to reflect the new number of spikes left
    }

    void UpdateSpikesLeftText()
    {
        spikesLeftText.text = "Spikes Left: " + (maxSpikes - currentSpikes); // Display the remaining spikes
    }

    private void OnTriggerEnter(Collider other)
    {
        // Set the flag true if the ghost spike enters a collider that is not tagged as "Ground"
        if (!other.CompareTag("Ground"))
        {
            isGhostColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the flag when the ghost spike exits a collider
        if (!other.CompareTag("Ground"))
        {
            isGhostColliding = false;
        }
    }
}
