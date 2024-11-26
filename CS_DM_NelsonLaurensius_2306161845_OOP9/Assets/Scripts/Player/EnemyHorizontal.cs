using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private float speed = 3f;
    private float screenBoundX;
    private float screenBoundY;

    void Start()
    {
        level = 1;
        Camera mainCamera = Camera.main;
        screenBoundX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.transform.position.z)).x;
        screenBoundY = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, mainCamera.transform.position.z)).y;

        RandomizeSpawn();
    }

    void Update()
    {
        Move();
        CheckBoundsAndTeleport();
    }

    private void RandomizeSpawn()
    {
        float randomY = Random.Range(-screenBoundY, screenBoundY);
        transform.position = new Vector3(screenBoundX, randomY, transform.position.z);
    }

    private void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void CheckBoundsAndTeleport()
    {
        if (transform.position.x < -screenBoundX)
        {
            RandomizeSpawn();
        }
    }
}
