using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] public int damage = 10;

    private void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            return;
        }

        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            if (bullet != null)
            {
                hitbox.Damage(bullet);
            }
            else
            {
                hitbox.Damage(damage);
            }
            InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
            if (invincibility != null && !invincibility.isInvincible)
            {
                invincibility.TriggerInvincibility();
            }

        }
    }
}
