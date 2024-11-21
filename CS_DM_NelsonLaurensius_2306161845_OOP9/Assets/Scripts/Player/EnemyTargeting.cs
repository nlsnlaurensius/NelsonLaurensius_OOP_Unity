using UnityEngine;

public class EnemyTargeting : Enemy
{
    private float speed = 3f;
    private Transform playerTransform;
    
    void Start()
    {
        level = 2; // Set level untuk EnemyTargeting
        if (Player.Instance != null)
        {
            playerTransform = Player.Instance.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
