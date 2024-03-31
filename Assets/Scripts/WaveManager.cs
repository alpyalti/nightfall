using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Transform buildingTransform; // Reference to the building Transform
    public int enemiesPerSpawnPoint = 2;
    public Text waveCooldownText; // Text for displaying the countdown to the next wave
    public Text wavesCompletedText; // Text for displaying the number of waves completed
    private int currentWave = 0; // Start at 0 to indicate no waves completed yet
    private bool isWaveInProgress = false;
    private int activeEnemies = 0; // Track the number of active enemies
    public bool WavesEnabled { get; private set; } = false; // Property to check if waves are enabled
    public GameObject invisibleWalls;

   public void StartWaves()
{
    Debug.Log("StartWaves called."); // This should log when StartWaves is invoked

    if (!WavesEnabled)
    {
        Debug.Log("Starting Waves..."); // This confirms waves are about to start
        WavesEnabled = true;
        wavesCompletedText.text = "Waves Completed: 0/3";
        invisibleWalls.SetActive(true);
        StartCoroutine(StartNextWaveWithCooldown());
    }
    else
    {
        Debug.Log("Waves already enabled."); // This logs if waves were already started
    }
}



    private void Start()
    {
        wavesCompletedText.text = "Waves Completed: " + currentWave + "/3";
        //StartCoroutine(StartNextWaveWithCooldown());
    }

    void Update()
    {
        // Check for 'P' key to skip cooldown
        if (Input.GetKeyDown(KeyCode.P) && !isWaveInProgress && currentWave < 3)
        {
            StopAllCoroutines();
            waveCooldownText.gameObject.SetActive(false); // Hide the cooldown text
            StartCoroutine(SpawnWave()); // Immediately start the next wave
        }
    }

    IEnumerator StartNextWaveWithCooldown(float cooldown = -1f)
    {
        isWaveInProgress = false;
        float waitTime = cooldown >= 0 ? cooldown : 30f; // Use the provided cooldown or the default 30 seconds

        waveCooldownText.gameObject.SetActive(true); // Show the cooldown text

        // Display the cooldown on screen
        while (waitTime > 0)
        {
            waveCooldownText.text = "Next Wave in: " + Mathf.Ceil(waitTime).ToString("F0");
            waitTime -= Time.deltaTime;
            yield return null;
        }

        waveCooldownText.gameObject.SetActive(false); // Hide the cooldown text
        StartCoroutine(SpawnWave()); // Start spawning the next wave
    }

    IEnumerator SpawnWave()
    {
        isWaveInProgress = true;
        currentWave++;
        activeEnemies = spawnPoints.Length * enemiesPerSpawnPoint; // Set the number of active enemies for this wave

        for (int i = 0; i < enemiesPerSpawnPoint; i++)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                SpawnEnemy(spawnPoint);
                yield return new WaitForSeconds(1f / spawnPoints.Length); // Adjust spawn delay based on the number of spawn points
            }
        }

        // Wait for all enemies to be defeated before considering the wave completed
        yield return new WaitUntil(() => activeEnemies == 0);
        wavesCompletedText.text = "Waves Completed: " + currentWave + "/3";

        if (currentWave >= 3)
        {
            // Handle all waves completed scenario
            wavesCompletedText.text = "All Waves Completed!";
            yield return new WaitForSeconds(5); // Keep the message for 5 seconds
            wavesCompletedText.gameObject.SetActive(false); // Optionally hide the text after showing the message
        }
        else
        {
            StartCoroutine(StartNextWaveWithCooldown()); // Wait for cooldown before the next wave
        }

        isWaveInProgress = false; // Mark the wave as completed
    }

    void SpawnEnemy(Transform spawnPoint)
    {
        if (spawnPoint == null) return;
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemyInstance.GetComponent<EnemyHealth>().onEnemyDied += EnemyDefeated; // Subscribe to the enemy's death event
        EnemyAI enemyAI = enemyInstance.GetComponent<EnemyAI>();
      if (enemyAI != null)
     {
        enemyAI.SetTargetBuilding(buildingTransform); // Ensure this line is correctly setting the target
     }
    }

    void EnemyDefeated()
    {
        activeEnemies--; // Decrement the count whenever an enemy is defeated
    }
}
