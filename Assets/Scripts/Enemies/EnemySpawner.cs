using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;     // Where enemies will appear
    public float spawnInterval = 5f; // Time between spawns
    public DayNightCycle dayNightCycle;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (dayNightCycle.time < 0.2f || dayNightCycle.time > 0.8f)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
