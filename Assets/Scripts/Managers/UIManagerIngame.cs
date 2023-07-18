using SuperMaxim.Messaging;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManagerIngame : MonoBehaviour
{
    private bool endGame = false;
    private bool paused = false;

    public bool isMobile;

    Dictionary<Player, PlayerScoreController> players = new();

    [Header("UI Panels")]

    [SerializeField]
    GameObject HUDParent;
    [SerializeField]
    GameObject HUDPlayer1;
    [SerializeField]
    GameObject HUDPlayer2;

    [SerializeField]
    GameObject ScreenDivision;

    [SerializeField]
    GameObject UIControls;
    [SerializeField]
    GameObject Menu;
    [SerializeField]
    GameObject EndGame;

    [Header("General Controllers")]
    [SerializeField]
    UIEndScreenController UIEndScreenController;

    [Header("Controllers P1")]
    [SerializeField]
    UIHornController UIHornController;
    [SerializeField]
    UIHPController UIHpController;
    [SerializeField]
    TMP_Text scoreText;

    [Header("Controllers P2")]
    [SerializeField]
    UIHornController UIHornControllerPlayer2;
    [SerializeField]
    UIHPController UIHpControllerPlayer2;
    [SerializeField]
    TMP_Text scoreTextPlayer2;

    void Start()
    {
        if (!isMobile)
        {
            isMobile = Application.isMobilePlatform;
        }

        ScreenDivision.SetActive(GameManager.Instance.IsMultiplayer());
        HUDPlayer2.SetActive(GameManager.Instance.IsMultiplayer());

        UIControls.SetActive(isMobile);

        Messenger.Default.Subscribe<EscapePayload>(TogglePause);
        Messenger.Default.Subscribe<EndGamePayload>(ShowEndScreen);
        Messenger.Default.Subscribe<HornCooldownStartPayload>(HandleHornCooldown);
        Messenger.Default.Subscribe<SetHPPayload>(HandleSetHP);
        Messenger.Default.Subscribe<PlayerScorePayload>(HandleScore);

        players.Add(Player.One, GameManager.Instance.GetPlayer(Player.One).GetComponent<PlayerScoreController>());
        if (GameManager.Instance.IsMultiplayer())
        {
            players.Add(Player.Two, GameManager.Instance.GetPlayer(Player.Two).GetComponent<PlayerScoreController>());
        }
    }

    public void TogglePause(EscapePayload payload)
    {
        if (!endGame)
        {
            paused = !paused;

            HUDParent.SetActive(!paused);
            Menu.SetActive(paused);

            Time.timeScale = paused ? 0 : 1;
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
        UIEndScreenController.ShowEndScore(players[Player.One].GetScore());
    }

    private void HandleHornCooldown(HornCooldownStartPayload payload)
    {
        if (payload.Id == Player.One)
            UIHornController.StartCoodown(payload.DurationSeconds);
        else if (payload.Id == Player.Two)
            UIHornControllerPlayer2.StartCoodown(payload.DurationSeconds);
    }

    private void HandleSetHP(SetHPPayload payload)
    {
        if (payload.Id == Player.One)
            UIHpController.SetHP(payload.HP);
        else if (payload.Id == Player.Two)
            UIHpControllerPlayer2.SetHP(payload.HP);
    }

    private void HandleScore(PlayerScorePayload payload)
    {
        if (payload.Id == Player.One)
            scoreText.text = payload.Score.ToString();
        else if (payload.Id == Player.Two)
            scoreTextPlayer2.text = payload.Score.ToString();
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
        Messenger.Default.Unsubscribe<EscapePayload>(TogglePause);
        Messenger.Default.Unsubscribe<EndGamePayload>(ShowEndScreen);
        Messenger.Default.Unsubscribe<HornCooldownStartPayload>(HandleHornCooldown);
        Messenger.Default.Unsubscribe<SetHPPayload>(HandleSetHP);
        Messenger.Default.Unsubscribe<PlayerScorePayload>(HandleScore);
    }
}
