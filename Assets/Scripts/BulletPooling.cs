using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 50;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // Optional: grow pool if needed
            GameObject bullet = Instantiate(bulletPrefab);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
