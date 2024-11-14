using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;

    void Start()
    {
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void Subtract(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} : Health : {health}");

        if (health <= 0)
        {
            health = 0;
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Debug.Log($"{gameObject.name} Mati cuyy NT");
        Destroy(gameObject);
    }
}
