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
    private bool isSpawning = false;
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
            Debug.LogError($"Spawner {gameObject.name} does not have an assigned enemy prefab.");
            return;
        }

        isSpawning = true;
        spawnTimer = 0f;
        Debug.Log($"Spawner {gameObject.name} starting to spawn {spawnCount} enemies.");
    }

    public void StopSpawning()
    {
        isSpawning = false;
        Debug.Log($"Spawner {gameObject.name} stopped spawning.");
    }

    public void SetSpawnCountForWave(int waveNumber)
    {
        spawnCount = defaultSpawnCount + ((waveNumber - level) * spawnCountMultiplier);
        Debug.Log($"Spawner {gameObject.name} prepared {spawnCount} enemies for wave {waveNumber}.");
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
        else
        {
            Debug.LogError($"Spawned object {spawnedEnemy.name} does not have an Enemy script attached.");
        }

        spawnCount--;
        Debug.Log($"Spawned {spawnedEnemy.name}. Remaining in this spawner: {spawnCount}");
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;
        Debug.Log($"{spawnedEnemy.name} defeated. Total kills for this spawner: {totalKill}");

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            spawnCount += spawnCountMultiplier;
            multiplierIncreaseCount++;
            Debug.Log($"Spawner {gameObject.name} increased spawn count to {spawnCount}.");
        }
    }
}