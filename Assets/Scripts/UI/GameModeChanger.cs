using UnityEngine;
using UnityEngine.UI;

public class GameModeChanger : MonoBehaviour
{
    [SerializeField]
    private GameMode gameMode;

    public void ChangeGameMode()
    {
        GameManager.Instance.SetGameMode(gameMode);
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeGameMode);
    }
}
