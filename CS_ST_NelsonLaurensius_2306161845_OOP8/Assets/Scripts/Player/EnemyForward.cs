using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    private float speed = 3f;
    private bool movingUp;
    private float screenBoundY;

    void Start()
    {
        Camera mainCamera = Camera.main;
        screenBoundY = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, mainCamera.transform.position.z)).y;
        RandomizeSpawn();
    }

    void Update()
    {
        Move();
        CheckBoundsAndReverse();
    }

    private void RandomizeSpawn()
    {
        float spawnY = Random.value > 0.5f ? -screenBoundY : screenBoundY;
        transform.position = new Vector3(transform.position.x, spawnY, transform.position.z);
        movingUp = spawnY < 0;
    }

    private void Move()
    {
        float moveDirection = movingUp ? 1 : -1;
        transform.Translate(Vector2.up * moveDirection * speed * Time.deltaTime);
    }

    private void CheckBoundsAndReverse()
    {
        if (transform.position.y > screenBoundY || transform.position.y < -screenBoundY)
        {
            movingUp = !movingUp;
        }
    }
}
