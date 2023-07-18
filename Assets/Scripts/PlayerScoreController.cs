using SuperMaxim.Messaging;
//using TMPro;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    //[SerializeField]
    //private TMP_Text scoreText;

    private int score;
    private PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        UpdateUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        UpdateUI();
    }

    private void UpdateUI()
    {
        Messenger.Default.Publish(new PlayerScorePayload(score, controller.Id));
        //scoreText.text = $"{score}";
    }
}
