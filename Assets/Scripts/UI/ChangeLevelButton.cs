using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeLevelButton : MonoBehaviour
{
    public enum Levels
    {
        MainMenu,
        Tutorial,
        Level_01,
        Level_02,
        Level_03
    }

    [SerializeField]
    Levels targetLevel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoToLevel);
    }

    public void GoToLevel()
    {
        string level = "";

        switch (targetLevel)
        {
            case Levels.MainMenu: level = "00.MainMenu"; break;
            case Levels.Tutorial: level = "01.Tutorial"; break;
            case Levels.Level_01: level = "02.Level_01"; break;
            case Levels.Level_02: level = "02.Level_01"; break;
            case Levels.Level_03: level = "02.Level_01"; break;
            default:
                return;
        }

        GameManager.Instance.ChangeLevel(level);
    }
}
