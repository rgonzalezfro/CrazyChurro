using UnityEngine;

public class GameModeChanger : MonoBehaviour
{
    [SerializeField]
    private GameMode gameMode;

    public void ChangeGameMode()
    {
        GameManager.Instance.SetGameMode(gameMode);
    }
}
