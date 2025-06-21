using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }
    public int HighScore { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
        Score = 0;
    }

    public static void EnsureExists()
    {
        if (Instance != null) return;

        GameObject prefab = Resources.Load<GameObject>("Prefabs/GameManager");
        if (prefab != null)
        {
            Instantiate(prefab);
        }
        else
        {
            Debug.LogError("GameManager prefab tidak ketemu di Resources/Prefabs.");
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;

        if (Score > HighScore)
        {
            HighScore = Score;
            SaveHighScore();
        }

        Debug.Log($"Score: {Score} | Highscore: {HighScore}");
    }

    private void LoadHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.Save();
    }

    public void ResetScore()
    {
        Score = 0;
    }
}