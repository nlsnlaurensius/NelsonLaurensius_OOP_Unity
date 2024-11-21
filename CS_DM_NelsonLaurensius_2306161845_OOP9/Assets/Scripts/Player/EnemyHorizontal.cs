using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private float speed = 3f;
    private float screenBoundX;

    void Start()
    {
        level = 1; // Set level untuk EnemyHorizontal
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
        float spawnX = screenBoundX;
        transform.position = new Vector3(spawnX, transform.position.y, transform.position.z);
    }

    private void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void CheckBoundsAndTeleport()
    {
        if (transform.position.x < -screenBoundX)
        {
            transform.position = new Vector3(screenBoundX, transform.position.y, transform.position.z);
        }
    }
}
