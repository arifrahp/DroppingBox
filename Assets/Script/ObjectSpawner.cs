using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<WeightedPrefab> weightedPrefabs; // List of weighted prefabs to spawn

    [System.Serializable]
    public class WeightedPrefab
    {
        public FallingObject fallingObject; // Reference to the FallingObject script
        public GameObject prefab;
        public float weight;
        public float minSpeed;
        public float maxSpeed;
    }

    public float spawnAreaMinX;
    public float spawnAreaMaxX;
    public float spawnY;
    public float spawnZ;

    public float initialMinSpawnInterval;
    public float initialMaxSpawnInterval;
    public float spawnIntervalDecreaseRate;
    public float minimumPossibleInterval;
    public float maxPossibleMinInterval;
    public float maxPossibleMaxInterval;

    private float currentMinSpawnInterval;
    private float currentMaxSpawnInterval;
    private float elapsedTime = 0f;

    void Start()
    {
        currentMinSpawnInterval = initialMinSpawnInterval;
        currentMaxSpawnInterval = initialMaxSpawnInterval;

        // Start the spawning process
        StartCoroutine(SpawnObjects());
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Start changing intervals only after the first 2 seconds
        if (elapsedTime > 2f)
        {
            // Decrease spawn intervals over time, but don't go below the minimum possible interval
            // and don't go above the maximum possible values for min and max intervals
            currentMinSpawnInterval = Mathf.Clamp(initialMinSpawnInterval - (elapsedTime - 2f) * spawnIntervalDecreaseRate, minimumPossibleInterval, maxPossibleMinInterval);
            currentMaxSpawnInterval = Mathf.Clamp(initialMaxSpawnInterval - (elapsedTime - 2f) * spawnIntervalDecreaseRate, minimumPossibleInterval, maxPossibleMaxInterval);
        }
    }

    private IEnumerator<WaitForSeconds> SpawnObjects()
    {
        while (true)
        {
            // Choose a random spawn interval within the current range
            float spawnInterval = Random.Range(currentMinSpawnInterval, currentMaxSpawnInterval);

            // Spawn a random object
            SpawnRandomObject();

            // Wait for the random interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandomObject()
    {
        if (weightedPrefabs.Count == 0)
        {
            Debug.LogWarning("No prefabs to spawn.");
            return;
        }

        // Choose a random prefab based on weights
        WeightedPrefab chosenWeightedPrefab = GetRandomWeightedPrefab();
        GameObject chosenPrefab = chosenWeightedPrefab.prefab;

        // Choose a random position within the spawn area
        float randomX = Random.Range(spawnAreaMinX, spawnAreaMaxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, spawnZ);

        // Instantiate the chosen prefab at the random position
        GameObject instantiatedObject = Instantiate(chosenPrefab, spawnPosition, Quaternion.identity);

        // Get the FallingObject component from the instantiated prefab
        FallingObject fallingObjectScript = instantiatedObject.GetComponent<FallingObject>();
        if (fallingObjectScript != null)
        {
            // Set the speed of the falling object to a random value between minSpeed and maxSpeed
            float randomSpeed = Random.Range(chosenWeightedPrefab.minSpeed, chosenWeightedPrefab.maxSpeed);
            fallingObjectScript.speed = randomSpeed;
        }
    }

    private WeightedPrefab GetRandomWeightedPrefab()
    {
        float totalWeight = 0f;

        // Calculate the total weight
        foreach (WeightedPrefab weightedPrefab in weightedPrefabs)
        {
            totalWeight += weightedPrefab.weight;
        }

        // Get a random value between 0 and totalWeight
        float randomWeight = Random.Range(0, totalWeight);
        float currentWeight = 0f;

        // Find the prefab corresponding to the random weight
        foreach (WeightedPrefab weightedPrefab in weightedPrefabs)
        {
            currentWeight += weightedPrefab.weight;
            if (randomWeight < currentWeight)
            {
                return weightedPrefab;
            }
        }

        // Fallback, should never reach here
        return weightedPrefabs[0];
    }
}
