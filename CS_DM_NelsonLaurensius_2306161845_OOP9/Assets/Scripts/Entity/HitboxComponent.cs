using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    private InvincibilityComponent invincibility;

    void Awake()
    {
        health = GetComponent<HealthComponent>();
        invincibility = GetComponent<InvincibilityComponent>();
    }

    public void Damage(int damageAmount)
    {
        if (health != null && (invincibility == null || !invincibility.isInvincible))
        {
            health.Subtract(damageAmount);

            if (invincibility != null)
            {
                invincibility.TriggerInvincibility();
            }
        }
    }

    public void Damage(Bullet bullet)
    {
        if (bullet != null)
        {
            Damage(bullet.damage);
        }
    }
}
