using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("- Pass Text References -")]
    [Space(10)]
    [SerializeField] private TMP_Text _totalMatches;
    [SerializeField] private TMP_Text _totalTurns;
    [SerializeField] private TMP_Text _totalCombo;
    [SerializeField] private TMP_Text _gameLevel;

    [Header("-- Main Menu Buttons --")]
    [Space(10)]
    [SerializeField] private Transform _LevelStartButton;
    [SerializeField] private Transform _LevelResumeButton;

    [Header("- Loading Screen UI Settings -")]
    [Space(10)]
    [SerializeField] private Transform _loagingScreen;
    [SerializeField] private string _gameSceneName = "GameScene";


    public void Start()
    {
        GamePlayerPreferenceManager.SetInitialGameStats();

        if (GamePlayerPreferenceManager.GetBoardData().Equals(string.Empty))
        {
            _LevelStartButton.gameObject.SetActive(true);
        }
        else
        {
            _LevelResumeButton.gameObject.SetActive(true);
        }

        SetMenuScoreBoard();
    }

    public void SetTotalComboText(string messageText)
    {
        _totalCombo.text = messageText;
    }

    public void SetTotalMatchesText(string messageText)
    {
        _totalMatches.text = messageText;
    }

    public void SetTotalTurnsText(string messageText)
    {
        _totalTurns.text = messageText;
    }

    public void SetGameLevelText(string messageText)
    {
        _gameLevel.text = messageText;
    }

    private void SetMenuScoreBoard()
    {
        SetTotalComboText("" + GamePlayerPreferenceManager.GetTotalCombo());
        SetTotalMatchesText("" + GamePlayerPreferenceManager.GetTotalMatches());
        SetTotalTurnsText("" + GamePlayerPreferenceManager.GetTotalTurns());
        SetGameLevelText("LEVEL " + GamePlayerPreferenceManager.GetGameLevel());
    }

    public void StartGameScene()
    {
        _loagingScreen.gameObject.SetActive(true);
        _loagingScreen.gameObject.GetComponent<LoadingScreenAsyncHandler>().StartLoading(_gameSceneName);
    }
}