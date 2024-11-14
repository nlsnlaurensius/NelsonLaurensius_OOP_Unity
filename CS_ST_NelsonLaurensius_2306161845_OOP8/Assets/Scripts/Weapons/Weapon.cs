using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    void Awake()
    {
        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = transform.Find("BulletSpawnPoint");
        }
    }

    void Start()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, collectionCheck, defaultCapacity, maxSize);

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Fire();
            timer = 0f;
        }
    }

    public void Fire()
    {
        if (bulletSpawnPoint == null || objectPool == null) return;

        Bullet newBullet = objectPool.Get();
        if (newBullet == null) return;

        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.rotation = bulletSpawnPoint.rotation;
        newBullet.gameObject.tag = gameObject.tag;
        newBullet.gameObject.SetActive(true);
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet);
        newBullet.SetPool(objectPool);
        newBullet.gameObject.SetActive(false);
        return newBullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            bullet.transform.rotation = bulletSpawnPoint.rotation;
            bullet.gameObject.SetActive(true);
        }
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            bullet.transform.rotation = Quaternion.identity;
            bullet.gameObject.SetActive(false);
        }
    }

    private void OnDestroyBullet(Bullet bullet)
    {
    }
}
