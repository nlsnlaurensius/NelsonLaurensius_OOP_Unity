using UnityEngine;

public class Enemy : MonoBehaviour
{
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
