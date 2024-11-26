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
    private bool isWaveIntervalActive = false;
    
    private GameUIController uiController;

    void Start()
    {
        waveNumber = 1;
        totalEnemies = 0;

        uiController = FindObjectOfType<GameUIController>();
        if (uiController != null)
        {
            uiController.SetWave(waveNumber);
            uiController.SetEnemiesLeft(totalEnemies);
        }

        foreach (var spawner in enemySpawners)
        {
            spawner.combatManager = this;
        }

        StartWaveCountdown();
    }

    void Update()
    {
        if (isWaveIntervalActive)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                isWaveIntervalActive = false;
                StartWave();
            }
        }
        else if (!isWaveInProgress)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                StartWave();
            }
        }
    }

    private void StartWaveCountdown()
    {
        timer = 0f;
        isWaveInProgress = false;
    }

    private void StartWave()
    {
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

        if (uiController != null)
        {
            uiController.SetWave(waveNumber);
            uiController.SetEnemiesLeft(totalEnemies);
        }

        isWaveInProgress = true;
    }

    public void OnEnemyKilled(int enemyLevel)
    {
        totalEnemies--;

        if (uiController != null)
        {
            uiController.SetEnemiesLeft(totalEnemies);
            uiController.AddPoints(enemyLevel); 
        }

        if (totalEnemies <= 0)
        {
            waveNumber++;
            isWaveIntervalActive = true;
            timer = 0f;

            if (uiController != null)
            {
                uiController.SetWave(waveNumber);
            }
        }
    }
}
