using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1;
    public EnemySpawner spawner;
    public CombatManager combatManager;

    private GameUIController uiController;

    void Start()
    {
        uiController = FindObjectOfType<GameUIController>();
    }

    void OnDestroy()
    {
        if (combatManager != null)
        {
            combatManager.OnEnemyKilled(level); 
        }

        if (spawner != null)
        {
            spawner.OnEnemyKilled();
        }
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
