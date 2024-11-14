using UnityEngine;

public class EnemyBoss : Enemy
{
    private float speed = 1f;
    private bool movingRight;
    private float screenBoundX;

    [SerializeField] private float shootIntervalInSeconds = 2f;
    private Weapon playerWeapon;
    private float timer;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180);

        Camera mainCamera = Camera.main;
        screenBoundX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.transform.position.z)).x;

        RandomizeSpawn();

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject dengan tag 'Player' tidak ditemukan di scene.");
            return;
        }

        playerWeapon = player.GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        Move();
        CheckBoundsAndReverse();
        AutoFireWeapon();
    }

    private void RandomizeSpawn()
    {
        float spawnX = Random.value > 0.5f ? -screenBoundX : screenBoundX;
        transform.position = new Vector3(spawnX, transform.position.y, transform.position.z);
        movingRight = spawnX < 0;
    }

    private void Move()
    {
        float moveDirection = movingRight ? -1 : 1;
        transform.Translate(Vector2.left * moveDirection * speed * Time.deltaTime);
    }

    private void CheckBoundsAndReverse()
    {
        if (transform.position.x > screenBoundX || transform.position.x < -screenBoundX)
        {
            movingRight = !movingRight;
        }
    }

    private void AutoFireWeapon()
    {
        if (playerWeapon == null) return;

        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            playerWeapon.Fire();
            timer = 0f;
        }
    }
}
