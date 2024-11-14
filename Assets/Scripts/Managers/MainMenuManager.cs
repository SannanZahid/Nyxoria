using System.Collections;
using System.Collections.Generic;
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

    public void Start()
    {
        if (GamePlayerPreferenceManager.GetFirstTimeGameOpenSet().Equals(0))
        {
            GamePlayerPreferenceManager.SetDefaultGameValues();
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
}
