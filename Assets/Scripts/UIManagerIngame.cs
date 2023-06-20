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
    [SerializeField]
    TMPro.TMP_Text EndGameScoreText;

    [Header("Sources")]
    [SerializeField]
    PlayerScoreController player;

    void Start()
    {
        Messenger.Default.Subscribe<EscapePayload>(TogglePause);
        Messenger.Default.Subscribe<EndGamePayload>(ShowEndScreen);
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
        EndGameScoreText.text = $"${player.GetScore()}";
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
        Messenger.Default.Unsubscribe<EscapePayload>(TogglePause);
        Messenger.Default.Unsubscribe<EndGamePayload>(ShowEndScreen);
    }
}
