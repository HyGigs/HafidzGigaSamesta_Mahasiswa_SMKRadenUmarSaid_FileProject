using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (ScoreManager.Instance == null) return;

        int score = ScoreManager.Instance.Score;
        int highScore = ScoreManager.Instance.HighScore;

        scoreText.text = $"Score: {score}";
        highScoreText.text = $"High Score: {highScore}";
    }
}
