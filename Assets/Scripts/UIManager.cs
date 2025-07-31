using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI fpsText;

    public Button nextWaveBtn;
    public Button stopResumeBtn;
    public Button destroyWaveBtn;

    private void Start()
    {
        nextWaveBtn.onClick.AddListener(GameManager.Instance.StartNextWave);
        stopResumeBtn.onClick.AddListener(GameManager.Instance.ToggleAutoCycle);
        destroyWaveBtn.onClick.AddListener(GameManager.Instance.DestroyCurrentWave);
    }

    private void Update()
    {
        waveText.text = "Wave: " + GameManager.Instance.currentWave;
        enemyCountText.text = "Enemies: " + WaveManager.Instance.GetActiveEnemyCount();
        fpsText.text = "FPS: " + Mathf.RoundToInt(1f / Time.deltaTime);
    }
}
