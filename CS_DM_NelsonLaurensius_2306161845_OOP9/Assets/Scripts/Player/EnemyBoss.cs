using UnityEngine;

public class EnemyBoss : Enemy
{
    private float speed = 1f;
    private float screenBoundX;

    [SerializeField] private float shootIntervalInSeconds = 2f;
    private Weapon weaponInstance;
    private float timer;

    private Vector3 initialPosition;

    void Start()
    {
        level = 3; // Set level untuk EnemyBoss
        Camera mainCamera = Camera.main;
        screenBoundX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.transform.position.z)).x;

        SetInitialPosition();

        if (weaponInstance == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                weaponInstance = player.GetComponentInChildren<Weapon>();
            }
            else
            {
                Debug.LogError("Player GameObject dengan tag 'Player' tidak ditemukan di scene.");
            }
        }
    }

    void Update()
    {
        Move();
        CheckBoundsAndTeleport();
        AutoFireWeapon();
    }

    private void SetInitialPosition()
    {
        initialPosition = new Vector3(screenBoundX, transform.position.y, transform.position.z);
        transform.position = initialPosition;

        transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    private void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void CheckBoundsAndTeleport()
    {
        if (transform.position.x > screenBoundX)
        {
            transform.position = new Vector3(-screenBoundX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -screenBoundX)
        {
            transform.position = initialPosition;
        }
    }

    private void AutoFireWeapon()
    {
        if (weaponInstance == null) return;

        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            weaponInstance.Fire();
            timer = 0f;
        }
    }
}
