using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]public GameObject[] enemyPrefabs; // assign 3 types in inspector
    private Queue<GameObject> pool = new Queue<GameObject>();

    public static EnemyPool Instance { get; private set; }


    private void Awake()
{
    if (Instance != null && Instance != this)
        Destroy(gameObject);
    else
        Instance = this;
}
private void Start()
{
    for (int i = 0; i < 100; i++) // increase if needed
    {
        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = Instantiate(enemyPrefabs[rand]);
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}


    public GameObject GetEnemy()
    {
        GameObject enemy;
        if (pool.Count > 0)
        {
            enemy = pool.Dequeue();
            enemy.SetActive(true);
        }
        else
        {
            int rand = Random.Range(0, enemyPrefabs.Length);
            enemy = Instantiate(enemyPrefabs[rand]);
        }
        return enemy;
    }

    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}
