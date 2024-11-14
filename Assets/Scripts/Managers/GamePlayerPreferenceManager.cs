using UnityEngine;

public class GamePlayerPreferenceManager : MonoBehaviour
{

    private const string GameSound = "Game_Sound";
    private const string GameLevel = "GameLevel";
    private const string TotalMatches = "Game_TotalMatches";
    private const string TotalTurns = "Game_TotalTurns";
    private const string TotalCombo = "Game_TotalCombo";

    #region GameScoreKeeping

    public static void SetGameLevel(int i)
    {
        PlayerPrefs.SetInt(GameLevel, i);
    }

    public static int GetGameLevel()
    {
        return PlayerPrefs.GetInt(GameLevel);
    }

    public static void SetTotalMatches(int i)
    {
        PlayerPrefs.SetInt(TotalMatches, i);
    }

    public static int GetTotalMatches()
    {
        return PlayerPrefs.GetInt(TotalMatches);
    }

    public static void SetTotalTurns(int i)
    {
        PlayerPrefs.SetInt(TotalTurns, i);
    }

    public static int GetTotalTurns()
    {
        return PlayerPrefs.GetInt(TotalTurns);
    }

    public static void SetTotalCombo(int i)
    {
        PlayerPrefs.SetInt(TotalCombo, i);
    }

    public static int GetTotalCombo()
    {
        return PlayerPrefs.GetInt(TotalCombo);
    }

    #endregion

}