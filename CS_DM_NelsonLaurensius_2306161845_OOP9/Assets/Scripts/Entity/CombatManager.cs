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
        Debug.Log($"Gelombang {waveNumber} akan dimulai dalam {waveInterval} detik...");
        timer = 0f;
        isWaveInProgress = false;
    }

    private void StartWave()
    {
        if (waveNumber > 3)
        {
            Debug.Log("Selamat! Anda telah menyelesaikan semua gelombang! Permainan Selesai.");
            timer = 0f;
            return;
        }

        Debug.Log($"Memulai Gelombang {waveNumber}");

        totalEnemies = 0;

        foreach (var spawner in enemySpawners)
        {
            if (spawner.level <= waveNumber)
            {
                Debug.Log($"Spawner {spawner.gameObject.name} diaktifkan untuk gelombang {waveNumber}");
                spawner.SetSpawnCountForWave(waveNumber);
                spawner.StartSpawning();
                totalEnemies += spawner.spawnCount;
            }
        }

        Debug.Log($"Gelombang {waveNumber} dimulai dengan total {totalEnemies} musuh.");
        isWaveInProgress = true;
    }

    private void CheckWaveCompletion()
    {
        bool allSpawnersFinished = true;

        foreach (var spawner in enemySpawners)
        {
            if (!spawner.AreAllEnemiesDefeated())
            {
                allSpawnersFinished = false;
                break;
            }
        }

        if (allSpawnersFinished)
        {
            Debug.Log($"Gelombang {waveNumber} selesai!");
            waveNumber++;

            if (waveNumber > 3)
            {
                Debug.Log("Semua gelombang selesai! Selamat atas kemenangan Anda!");
                isWaveInProgress = false;
                return;
            }

            StartWaveCountdown();
        }
    }

    public void OnEnemyKilled()
    {
        totalEnemies--;

        Debug.Log($"Musuh terbunuh! Sisa musuh: {totalEnemies}");
    }
}
