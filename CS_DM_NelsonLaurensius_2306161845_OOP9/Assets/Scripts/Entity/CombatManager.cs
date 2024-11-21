using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    public EnemySpawner[] enemySpawners;

    [Header("Wave Settings")]
    public float timer = 0f;
    public float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;
    private bool isWaveInProgress = false;

    void Start()
    {
        waveNumber = 1;
        totalEnemies = 0;
        StartWaveCountdown();
    }

    void Update()
    {
        if (!isWaveInProgress)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                StartWave();
            }
        }
        else
        {
            CheckWaveCompletion();
        }
    }

    private void StartWaveCountdown()
    {
        Debug.Log($"Wave {waveNumber} will start in {waveInterval} seconds...");
        timer = 0f;
        isWaveInProgress = false;
    }

    private void StartWave()
    {
        if (waveNumber > 3)
        {
            Debug.Log("Congratulations! You have cleared all waves! Game Over.");
            return;
        }

        Debug.Log($"Starting Wave {waveNumber}");

        totalEnemies = 0;

        foreach (var spawner in enemySpawners)
        {
            if (spawner.level <= waveNumber)
            {
                spawner.SetSpawnCountForWave(waveNumber);
                spawner.StartSpawning();
                totalEnemies += spawner.spawnCount;
            }
        }

        Debug.Log($"Wave {waveNumber} started with {totalEnemies} total enemies.");
        isWaveInProgress = true;
    }

    private void CheckWaveCompletion()
    {
        if (totalEnemies <= 0)
        {
            Debug.Log($"Wave {waveNumber} completed!");

            waveNumber++;

            if (waveNumber > 3)
            {
                Debug.Log("All waves cleared! Congratulations on your victory!");
                isWaveInProgress = false;
                return;
            }

            StartWaveCountdown();
        }
    }

    public void OnEnemyKilled()
    {
        totalEnemies--;

        if (totalEnemies < 0) totalEnemies = 0;

        Debug.Log($"Enemy killed! Remaining enemies: {totalEnemies}");

        if (totalEnemies == 0)
        {
            Debug.Log($"Wave {waveNumber} completed!");
            isWaveInProgress = false;
        }
    }
}