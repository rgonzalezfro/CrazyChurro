using TMPro;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    private int score;

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"${score}";
    }
}
