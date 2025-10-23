using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // Assign your enemy prefab here
    public Transform spawnPoint;     // Where enemies will appear
    public float spawnInterval = 5f; // Time between spawns

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Spawn the enemy at the spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
