using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public bool autoCycle = true;
    [SerializeField]public int currentWave = 0;
    [SerializeField]public bool isSpawning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        currentWave++;
        StartCoroutine(WaveManager.Instance.SpawnWave(currentWave));
    }

    public void OnWaveCleared()
    {
        if (autoCycle)
            Invoke(nameof(StartNextWave), 5f); // wait 5 seconds
    }

    public void DestroyCurrentWave()
    {
        WaveManager.Instance.DestroyCurrentWave();
    }

 public void ToggleAutoCycle()
{
    autoCycle = !autoCycle;

   // If the wave is over and nothing is spawning, resume immediately
        if (!isSpawning && WaveManager.Instance.GetActiveEnemyCount() == 0)
        {
            Invoke(nameof(StartNextWave), 1f); // short delay to feel responsive
        }
    
}


}
