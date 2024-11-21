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
            combatManager.OnEnemyKilled();
        }
        else
        {
            Debug.LogError($"CombatManager tidak ditetapkan untuk {gameObject.name}.");
        }

        if (spawner != null)
        {
            spawner.OnEnemyKilled();
        }
        else
        {
            Debug.LogError($"Spawner tidak ditetapkan untuk {gameObject.name}.");
        }

        Debug.Log($"{gameObject.name} dihancurkan.");
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
