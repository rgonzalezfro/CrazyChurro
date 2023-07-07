using SuperMaxim.Messaging;
using UnityEngine;

public class UIManagerIngame : MonoBehaviour
{
    private bool endGame = false;
    private bool paused = false;

    [Header("Sources")]
    [SerializeField]
    PlayerScoreController player;

    [Header("UI Panels")]

    [SerializeField]
    GameObject HUD;
    [SerializeField]
    GameObject Menu;
    [SerializeField]
    GameObject EndGame;

    [Header("Controllers")]
    [SerializeField]
    UIHornController UIHornController;
    [SerializeField]
    UIHPController UIHpController;
    
    [SerializeField]
    UIEndScreenController UIEndScreenController;

    void Start()
    {
        Messenger.Default.Subscribe<EscapePayload>(TogglePause);
        Messenger.Default.Subscribe<EndGamePayload>(ShowEndScreen);
        Messenger.Default.Subscribe<HornCooldownStartPayload>(HandleHornCooldown);
        Messenger.Default.Subscribe<SetHPPayload>(HandleSetHP);

        if (player is null)
        {
            Debug.LogWarning("No hay referencia al player en el UI Manager");
            player = FindObjectOfType<PlayerScoreController>();
        }
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
        UIEndScreenController.ShowEndScore(player.GetScore());
    }

    private void HandleHornCooldown(HornCooldownStartPayload payload)
    {
        UIHornController.StartCoodown(payload.DurationSeconds);
    }

    private void HandleSetHP(SetHPPayload payload)
    {
        UIHpController.SetHP(payload.HP);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
        Messenger.Default.Unsubscribe<EscapePayload>(TogglePause);
        Messenger.Default.Unsubscribe<EndGamePayload>(ShowEndScreen);
        Messenger.Default.Unsubscribe<HornCooldownStartPayload>(HandleHornCooldown);
        Messenger.Default.Unsubscribe<SetHPPayload>(HandleSetHP);
    }
}
