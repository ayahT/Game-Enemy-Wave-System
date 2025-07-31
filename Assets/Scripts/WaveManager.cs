using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    public Transform[] spawnPoints;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Awake() => Instance = this;

    public IEnumerator SpawnWave(int waveNumber)
    {
        GameManager.Instance.isSpawning = true;
        int enemyCount = waveNumber switch
{
    1 => 30,
    2 => 50,
    3 => 70,
    _ => 70 + ((waveNumber - 3) * 10)
};


        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = EnemyPool.Instance.GetEnemy();
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            enemy.transform.position = spawn.position;
            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(1f); // spread spawn
        }

        GameManager.Instance.isSpawning = false;
    }

    public void OnEnemyKilled(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        EnemyPool.Instance.ReturnToPool(enemy);

        if (activeEnemies.Count == 0 && !GameManager.Instance.isSpawning)
        {
            GameManager.Instance.OnWaveCleared();
        }
    }

    public void DestroyCurrentWave()
    {
        foreach (var enemy in activeEnemies)
            EnemyPool.Instance.ReturnToPool(enemy);
        activeEnemies.Clear();

        if (GameManager.Instance.autoCycle)
            GameManager.Instance.OnWaveCleared();
    }

    public int GetActiveEnemyCount() => activeEnemies.Count;
}
