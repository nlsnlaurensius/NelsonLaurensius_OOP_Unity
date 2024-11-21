using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;
    public EnemySpawner spawner;
    public CombatManager combatManager;

    void OnDestroy()
    {
        if (combatManager != null)
        {
            combatManager.OnEnemyKilled(); // Mengurangi totalEnemies di CombatManager
        }
        else
        {
            Debug.LogError($"CombatManager not assigned for {gameObject.name}.");
        }

        if (spawner != null)
        {
            spawner.OnEnemyKilled(); // Mengelola statistik spawner
        }
        else
        {
            Debug.LogError($"Spawner not assigned for {gameObject.name}.");
        }

        Debug.Log($"{gameObject.name} destroyed.");
    }

    void Awake()
    {
        InvincibilityComponent invincibilityComponent = GetComponent<InvincibilityComponent>();
        if (invincibilityComponent == null)
        {
            invincibilityComponent = gameObject.AddComponent<InvincibilityComponent>();
        }

        Material enemyFlashMaterial = Resources.Load<Material>("EnemyFlashMaterial");
        if (enemyFlashMaterial != null)
        {
            invincibilityComponent.SetBlinkMaterial(enemyFlashMaterial);
        }
    }
}
