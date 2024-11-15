using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <Class-Purpose>
//   Class for creating game board with required card sprites and providing basic shuffling functionality, class also keep tack of the game state.
/// </Class-Purpose>

public class GameController : MonoBehaviour
{
    [SerializeField] private string FaceCardSpritesAddressableGroupLabel;
    public int CardToPlaceOnBoard = 8;
    public static Action LevelCompleteEventListner;
    public static Action < List<Sprite> > GameStartEvent;

    [SerializeField] private List<Sprite> _cardFace = new List<Sprite>();
    [SerializeField] private GameBoard _gameBoard = default;
    [SerializeField] private AddressableDownloader _addressableDownloader;
    private bool _resetFailScreenflag = true;

    private void Start()
    {
        GameStartEvent += StartSettingGameBoard;
        _addressableDownloader.LoadSpritesByGroupLabel(FaceCardSpritesAddressableGroupLabel);
    }

    private void OnEnable()
    {
        LevelCompleteEventListner += LevelComplete;
    }

    private void OnDisable()
    {
        LevelCompleteEventListner += LevelComplete;
        GameStartEvent -= StartSettingGameBoard;
    }

    public void StartNextLevel()
    {
        GameUIMnager.Instance.ToggleActivateLevelCompleteScreen(false);
        GameUIMnager.Instance.ToggleActivateLevelFailScreen(false);
        StartCoroutine(StartNextGame());
    }

    public void ResetLevelOnFail()
    {
        if (_resetFailScreenflag)
        {
            _resetFailScreenflag = false;
            StartCoroutine(ResetGameBoard());
        }
    }

    public void LevelComplete()
    {
        GameUIMnager.Instance.ToggleActivateLevelCompleteScreen(true);
    }
 
    public void StartSettingGameBoard(List<Sprite> loadedSprites)
    {
        _cardFace = loadedSprites;
        InitializeBoard();
    }
    private void ShuffleAndResetBoard()
    {
        ShuffleCards(ref _cardFace);
        _gameBoard.ResetBoard(GetShuffledFaceCards());
    }

    /// <summary> 
    //Function Shuffles the sprites so everytime new face card are spawn on to the board.
    /// </summary> 
    private void InitializeBoard()
    {
        if (GamePlayerPreferenceManager.GetBoardData().Equals(string.Empty))
        {
            ShuffleCards(ref _cardFace);
            _gameBoard.SetBoard(GetShuffledFaceCards());
        }
        else
        {
            _gameBoard.LoadLastSaveLevel(_cardFace);
        }
    }

    /// <summary> 
    // Returns the number of cards to be placed on board from sprite list.
    /// </summary> 
    private List<Sprite> GetShuffledFaceCards()
    {
        return _cardFace.Take(CardToPlaceOnBoard).ToList();
    }

    /// <summary> 
    //For shuffling objects provide through List.
    /// </summary> 
    public static void ShuffleCards<T>(ref List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private IEnumerator StartNextGame()
    {
        yield return new WaitForSeconds(1.5f);
        ShuffleCards(ref _cardFace);
        _gameBoard.ResetBoard(GetShuffledFaceCards());
    }

    private IEnumerator ResetGameBoard()
    {
        yield return new WaitForSeconds(1f);
        _resetFailScreenflag = true;
        GameUIMnager.Instance.ToggleActivateLevelFailScreen(false);
        ShuffleAndResetBoard();
    }
}