using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private float speed = 3f;
    private bool movingRight;
    private float screenBoundX;

    void Start()
    {
        Camera mainCamera = Camera.main;
        screenBoundX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.transform.position.z)).x;
        RandomizeSpawn();
    }

    void Update()
    {
        Move();
        CheckBoundsAndTeleport();
    }

    private void RandomizeSpawn()
    {
        float spawnX = Random.value > 0.5f ? -screenBoundX : screenBoundX;
        transform.position = new Vector3(spawnX, transform.position.y, transform.position.z);
        movingRight = spawnX < 0;
    }

    private void Move()
    {
        float moveDirection = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * moveDirection * speed * Time.deltaTime);
    }

    private void CheckBoundsAndTeleport()
    {
        if (movingRight && transform.position.x > screenBoundX)
        {
            transform.position = new Vector3(-screenBoundX, transform.position.y, transform.position.z);
        }
        else if (!movingRight && transform.position.x < -screenBoundX)
        {
            transform.position = new Vector3(screenBoundX, transform.position.y, transform.position.z);
        }
    }
}
