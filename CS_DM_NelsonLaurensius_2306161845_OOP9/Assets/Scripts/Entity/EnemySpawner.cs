using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [Header("Spawner Settings")]
    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;
    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;
    public int level = 1;
    public bool isSpawning = false;

    private float spawnTimer = 0f;

    private void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    private void Update()
    {
        if (isSpawning && spawnCount > 0)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
    }

    public void StartSpawning()
    {
        if (spawnedEnemy == null)
        {
            return;
        }

        isSpawning = true;
        spawnTimer = 0f;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void SetSpawnCountForWave(int waveNumber)
    {
        spawnCount = defaultSpawnCount;
    }

    private void SpawnEnemy()
    {
        if (spawnCount <= 0 || spawnedEnemy == null) return;

        Enemy enemyInstance = Instantiate(spawnedEnemy, transform.position, Quaternion.identity);

        if (enemyInstance != null)
        {
            enemyInstance.spawner = this;
            enemyInstance.combatManager = combatManager;
        }

        spawnCount--;

        if (spawnCount <= 0) StopSpawning();
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            multiplierIncreaseCount++;

            defaultSpawnCount = defaultSpawnCount + (spawnCountMultiplier * multiplierIncreaseCount);
        }
    }
}
