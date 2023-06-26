using SuperMaxim.Messaging;
using UnityEngine;

public class UIManagerIngame : MonoBehaviour
{
    private bool endGame = false;
    private bool paused = false;

    [SerializeField]
    GameObject HUD;

    [SerializeField]
    GameObject Menu;

    [SerializeField]
    GameObject EndGame;

    [Header("End Game Screen")]
    [SerializeField]
    TMPro.TMP_Text EndGameScoreText;

    [SerializeField]
    TMPro.TMP_Text NewBestScoreText;

    [SerializeField]
    TMPro.TMP_Text BestScoreInfoText;

    [Header("Horn")]
    [SerializeField]
    UIHornController UIHornController;

    [Header("Sources")]
    [SerializeField]
    PlayerScoreController player;

    void Start()
    {
        Messenger.Default.Subscribe<EscapePayload>(TogglePause);
        Messenger.Default.Subscribe<EndGamePayload>(ShowEndScreen);
        Messenger.Default.Subscribe<HornCooldownStartPayload>(HandleHornCooldown);
    }

    public void TogglePause(EscapePayload payload)
    {
        if (!endGame)
        {
            paused = !paused;

            HUD.SetActive(!paused);
            Menu.SetActive(paused);

            Time.timeScale = paused ? 0 : 1 ;
        }
    }

    public void TogglePause()
    {
        TogglePause(null);
    }

    private void ShowEndScreen(EndGamePayload payload)
    {
        endGame = true;
        Time.timeScale = 0;
        EndGame.SetActive(true);

        int score = player.GetScore();

        EndGameScoreText.text = $"${score}";

        if(score > GameManager.Instance.GetBestScore())
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

    private void HandleHornCooldown(HornCooldownStartPayload payload)
    {
        UIHornController.StartCoodown(payload.DurationSeconds);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
        Messenger.Default.Unsubscribe<EscapePayload>(TogglePause);
        Messenger.Default.Unsubscribe<EndGamePayload>(ShowEndScreen);
        Messenger.Default.Unsubscribe<HornCooldownStartPayload>(HandleHornCooldown);
    }
}
