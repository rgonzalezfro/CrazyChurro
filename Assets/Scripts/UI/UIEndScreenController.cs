using UnityEngine;

public class UIEndScreenController : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text EndGameScoreText;
    [SerializeField]
    TMPro.TMP_Text NewBestScoreText;
    [SerializeField]
    TMPro.TMP_Text BestScoreInfoText;

    public void ShowEndScore(int score)
    {
        EndGameScoreText.text = $"${score}";

        if (score > GameManager.Instance.GetBestScore())
        {
            NewBestScoreText.gameObject.SetActive(true);
            GameManager.Instance.SetBestScore(score);
        }
        else
        {
            BestScoreInfoText.gameObject.SetActive(true);
            BestScoreInfoText.text = BestScoreInfoText.text.Replace("{0}", $"${GameManager.Instance.GetBestScore()}");
        }
    }

}
