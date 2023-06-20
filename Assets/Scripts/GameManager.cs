using SuperMaxim.Messaging;
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

    private int bestScore;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Messenger.Default.Publish(new EscapePayload());
        }
    }
}