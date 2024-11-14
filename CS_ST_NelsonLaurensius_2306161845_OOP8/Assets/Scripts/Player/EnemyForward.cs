using UnityEngine;

public class EnemyForward : Enemy
{
    private float speed = 3f;
    private float screenBoundY;

    void Start()
    {
        Camera mainCamera = Camera.main;
        screenBoundY = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, mainCamera.transform.position.z)).y;
        ResetPositionToTop();
    }

    void Update()
    {
        MoveDownward();
        CheckBoundsAndTeleport();
    }

    private void MoveDownward()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void CheckBoundsAndTeleport()
    {
        if (transform.position.y < -screenBoundY)
        {
            ResetPositionToTop();
        }
    }

    private void ResetPositionToTop()
    {
        float randomX = Random.Range(-screenBoundY, screenBoundY);
        transform.position = new Vector3(randomX, screenBoundY, transform.position.z);
    }
}
