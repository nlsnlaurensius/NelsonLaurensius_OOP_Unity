using UnityEngine;
using UnityEngine.Assertions;

public class EnemyClickSpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyVariants;
    [SerializeField] private int selectedVariant = 0;

    void Start()
    {
       Assert.IsTrue(enemyVariants.Length > 0, "Tambahkan setidaknya 1 Prefab Enemy terlebih dahulu!");
    }

    private void Update()
    {
        for (int i = 1; i <= enemyVariants.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                selectedVariant = i - 1;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnEnemyAtRandomLocation();
        }
    }

    private void SpawnEnemyAtRandomLocation()
    {
        if (selectedVariant < enemyVariants.Length)
        {
            Vector3 spawnPosition = GetRandomPosition();
            Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);
            Enemy spawnedEnemy = Instantiate(enemyVariants[selectedVariant], spawnPosition, spawnRotation);

            if (spawnedEnemy.GetComponent<InvincibilityComponent>() == null)
            {
                spawnedEnemy.gameObject.AddComponent<InvincibilityComponent>();
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        Camera mainCamera = Camera.main;
        float screenX = Random.Range(0.1f, 0.9f);
        float screenY = Random.Range(0.1f, 0.9f);
        Vector3 randomScreenPosition = new Vector3(screenX, screenY, mainCamera.nearClipPlane);
        Vector3 worldPosition = mainCamera.ViewportToWorldPoint(randomScreenPosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}
