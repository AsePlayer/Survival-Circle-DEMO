using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of the enemy object
    public float spawnInterval = 2f; // Time interval between enemy spawns
    public float spawnDistance = 10f; // Distance from the scene bounds where enemies will spawn

    private float spawnTimer;
    private Camera mainCamera;
    private GameController gameController;

    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════
    private void Start()
    {
        spawnTimer = spawnInterval;
        mainCamera = Camera.main;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        // Check if the player is alive
        if (gameController.IsPlayerAlive() == false) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    // ════════════════════════════
    //       Spawn Methods
    // ════════════════════════════
    
    // Spawn an enemy
    private void SpawnEnemy()
    {
        // Calculate a random position outside the scene bounds
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instantiate the enemy prefab at the spawn position
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    // Calculate a random position outside the scene bounds
    private Vector3 GetRandomSpawnPosition()
    {
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float spawnX = Random.Range(-cameraWidth - spawnDistance, cameraWidth + spawnDistance);
        float spawnY = Random.Range(-cameraHeight - spawnDistance, cameraHeight + spawnDistance);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        // Ensure the spawn position is outside the scene bounds
        while (spawnPosition.x > -cameraWidth && spawnPosition.x < cameraWidth && spawnPosition.y > -cameraHeight && spawnPosition.y < cameraHeight)
        {
            spawnX = Random.Range(-cameraWidth - spawnDistance, cameraWidth + spawnDistance);
            spawnY = Random.Range(-cameraHeight - spawnDistance, cameraHeight + spawnDistance);
            spawnPosition = new Vector3(spawnX, spawnY, 0f);
        }
        return spawnPosition;
    }
}
