using SuperMaxim.Messaging;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    public bool dontDestroyOnLoad;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }

        Init();
    }

    private GameMode gameMode;

    [SerializeField]
    private GameObject playerPrefab;

    private int bestScore;

    private Dictionary<Player, GameObject> players = new();

    private void Init()
    {
        Application.targetFrameRate = 60;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public void ChangeLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void SetBestScore(int score)
    {
        bestScore = score;
        PlayerPrefs.SetInt("BestScore", bestScore);
    }

    public void SetGameMode(GameMode mode)
    {
        gameMode = mode;
    }

    public bool IsMultiplayer()
    {
        return gameMode == GameMode.MultiPlayer;
    }

    public GameObject GetPlayer(Player playerEnum)
    {
        GameObject player;
        if (!players.TryGetValue(playerEnum, out player))
        {
            player = Instantiate(playerPrefab);
            player.GetComponent<PlayerController>().SetId(playerEnum);
            players.Add(playerEnum, player);
        }
        else if (!player)
        {
            player = Instantiate(playerPrefab);
            player.GetComponent<PlayerController>().SetId(playerEnum);
            players[playerEnum] = player;
        }
        return player;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Messenger.Default.Publish(new EscapePayload());
        }
    }
}