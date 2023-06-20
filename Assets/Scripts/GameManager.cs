using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        Init();
    }

    private void Init()
    {
        Application.targetFrameRate = 60;
    }

    public void ChangeLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Messenger.Default.Publish(new EscapePayload());
        }
    }
}