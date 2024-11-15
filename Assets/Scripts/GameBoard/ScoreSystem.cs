using UnityEngine;

/// <Class-Purpose>
/// - Class for managing score during game play 
/// - Score calculated is populated to scored board in game screen UI
/// </Class Purpose>


public class ScoreSystem
{
    public int CardsMatchScore { private set; get; }
    public int TurnsScore { private set; get; }
    public int CardComboScore { private set; get; }

    private int _totalMatchScore = 0;
    private int _totalTurnsScore = 0;
    private int _totalCombo = 0;
    private bool _match = false;

    public ScoreSystem()
    {
        _totalMatchScore = GamePlayerPreferenceManager.GetTotalMatches();
        SetTotalMatchScoreDashboard(_totalMatchScore);
        _totalTurnsScore = GamePlayerPreferenceManager.GetTotalTurns();
        SetTotalTurnScoreDashboard(_totalTurnsScore);
        _totalCombo = GamePlayerPreferenceManager.GetTotalCombo();
        SetTotalComboScoreDashboard(_totalCombo);
    }
    public void ResetScoreForNewLevel()
    {
        CardsMatchScore = 0;
        SetMatchScoreDashboard(0);
        TurnsScore = 0;
        SetTurnScoreDashboard(0);
        CardComboScore = 0;
        SetComboScoreDashboard(0);
    }

    public void SetLastLevelSavedScores(int matchScore,int turnScore, int comboScore)
    {
        CardsMatchScore = matchScore;
        TurnsScore = turnScore;
        CardComboScore = comboScore;
    }

    public void CardsMatched_Score()
    {
        if (_match)
        {
            CardsComboScore();
        }
        else
        {
            _match = true;
        }

        IncrementCardsMatch();
        IncrementTurn();
    }

    public void CardsMisMatchedScore()
    {
        _match = false;
        IncrementTurn();
        CardComboScore = 0;
        SetComboScoreDashboard(CardComboScore);
    }

    private void IncrementCardsMatch()
    {
        CardsMatchScore++;
        SetMatchScoreDashboard(CardsMatchScore);
        SetCardsTotalMatchScore();
    }

    private void IncrementTurn()
    {
        TurnsScore++;
        SetTurnScoreDashboard(TurnsScore);
        SetCardsTotalTurnsScore();
    }

    private void CardsComboScore()
    {
        CardComboScore++;
        SetComboScoreDashboard(CardComboScore);
        SetCardsTotalComboScore();
    }

    private void SetCardsTotalMatchScore()
    {
        _totalMatchScore++;
        SetTotalMatchScoreDashboard(_totalMatchScore);
        GamePlayerPreferenceManager.SetTotalMatches(_totalMatchScore);
    }

    private void SetCardsTotalTurnsScore()
    {
        _totalTurnsScore++;
        SetTotalTurnScoreDashboard(_totalTurnsScore);
        GamePlayerPreferenceManager.SetTotalTurns(_totalTurnsScore);
    }

    private void SetCardsTotalComboScore()
    {
        _totalCombo++;
        SetTotalComboScoreDashboard(_totalCombo);
        GamePlayerPreferenceManager.SetTotalCombo(_totalCombo);
    }

    private void SetComboScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetComboText("" + value);
    }

    private void SetMatchScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetMatchesText("" + value);
    }

    private void SetTurnScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetTurnsText("" + value);
    }

    private void SetTotalComboScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetTotalComboText("" + value);
    }

    private void SetTotalMatchScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetTotalMatchesText("" + value);
    }

    private void SetTotalTurnScoreDashboard(int value)
    {
        GameUIMnager.Instance.SetTotalTurnsText("" + value);
    }
}