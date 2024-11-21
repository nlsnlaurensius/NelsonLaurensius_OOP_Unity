using System.Collections;
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

    private bool isSpawning = false;
    private int activeEnemies = 0;

    public void StartSpawning()
    {
        if (!isSpawning && spawnedEnemy != null && level <= combatManager.waveNumber) 
        {
            isSpawning = true;
            Debug.Log($"Spawner {gameObject.name} sedang memunculkan musuh level {level} untuk gelombang {combatManager.waveNumber}");
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            Debug.Log($"Spawner {gameObject.name} dilewati. Level spawner: {level}, Gelombang: {combatManager.waveNumber}");
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            Enemy enemyInstance = Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
            enemyInstance.spawner = this;
            enemyInstance.combatManager = combatManager;
            activeEnemies++;
        }
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;
        activeEnemies--;

        Debug.Log($"{spawnedEnemy.name} dikalahkan. Total kill untuk spawner ini: {totalKill}");

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            spawnCount = defaultSpawnCount + (spawnCountMultiplier * multiplierIncreaseCount);
            multiplierIncreaseCount++;
        }
    }

    public bool AreAllEnemiesDefeated()
    {
        return activeEnemies <= 0;
    }
}
