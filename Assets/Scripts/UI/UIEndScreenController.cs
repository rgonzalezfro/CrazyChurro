using UnityEngine;

public class UIEndScreenController : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text CongratsSPText;
    [SerializeField]
    TMPro.TMP_Text CongratsP1Text;
    [SerializeField]
    TMPro.TMP_Text CongratsP2Text;
    [SerializeField]
    TMPro.TMP_Text CongratsTieText;
    [SerializeField]
    TMPro.TMP_Text EndGameScoreText;
    [SerializeField]
    TMPro.TMP_Text NewBestScoreText;
    [SerializeField]
    TMPro.TMP_Text BestScoreInfoText;

    public void ShowEndScore(int score, Player playerId, bool isMultiplayer, bool tie = false)
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

        if (!isMultiplayer)
        {
            CongratsSPText.gameObject.SetActive(true);
        }
        else
        {
            if (tie)
            {
                CongratsTieText.gameObject.SetActive(true);
            }
            else
            {
                switch (playerId)
                {
                    case Player.One:
                        CongratsP1Text.gameObject.SetActive(true);
                        break;
                    case Player.Two:
                        CongratsP2Text.gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }
    }

}
