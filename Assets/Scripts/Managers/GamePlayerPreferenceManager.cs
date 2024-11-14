using UnityEngine;

public class GamePlayerPreferenceManager : MonoBehaviour
{

    private const string GameSound = "Game_Sound";
    private const string GameLevel = "GameLevel";
    private const string TotalMatches = "Game_TotalMatches";
    private const string TotalTurns = "Game_TotalTurns";
    private const string TotalCombo = "Game_TotalCombo";
    private const string FirstTimeSet = "Game_FirstTimeSet";


    #region Sound

    public static void SetSound(int i)
    {
        PlayerPrefs.SetInt(GameSound, i);
    }

    public static int GetSound()
    {
        return PlayerPrefs.GetInt(GameSound);
    }

    #endregion

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

    #region Set Default Game Playerpref Values

    public static void SetFirstTimeGameOpen()
    {
        if (PlayerPrefs.GetInt(FirstTimeSet).Equals(0))
            PlayerPrefs.SetInt(FirstTimeSet, 1);
    }

    public static int GetFirstTimeGameOpenSet()
    {
        return PlayerPrefs.GetInt(FirstTimeSet);
    }

    public static void SetInitialGameStats()
    {
        if (GetFirstTimeGameOpenSet().Equals(0))
        {
            SetDefaultGameValues();
        }
    }

    public static void SetDefaultGameValues()
    {
        if (GetFirstTimeGameOpenSet().Equals(0))
        {
            SetGameLevel(1);
            SetFirstTimeGameOpen();
        }
    }

    #endregion

}