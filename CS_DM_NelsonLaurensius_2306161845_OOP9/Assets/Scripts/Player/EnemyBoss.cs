using UnityEngine;

public class EnemyBoss : Enemy
{
    private float speed = 1f;
    private float screenBoundX;
    private float screenBoundY;

    [SerializeField] private float shootIntervalInSeconds = 2f;
    private Weapon weaponInstance;
    private float timer;

    private Vector3 initialPosition;

    void Start()
    {
        level = 3; 
        Camera mainCamera = Camera.main;
        screenBoundX = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.transform.position.z)).x;
        screenBoundY = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, mainCamera.transform.position.z)).y;

        SetInitialPosition();

        if (weaponInstance == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                weaponInstance = player.GetComponentInChildren<Weapon>();
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
        initialPosition = new Vector3(screenBoundX, Random.Range(-screenBoundY, screenBoundY), transform.position.z);
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
            transform.position = new Vector3(-screenBoundX, Random.Range(-screenBoundY, screenBoundY), transform.position.z);
        }
        else if (transform.position.x < -screenBoundX)
        {
            transform.position = new Vector3(screenBoundX, Random.Range(-screenBoundY, screenBoundY), transform.position.z);
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
