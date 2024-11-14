using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <Class-Purpose>
//   Class for creating game board with required card sprites and providing basic shuffling functionality, class also keep tack of the game state.
///

public class GameController : MonoBehaviour
{
    [SerializeField] private string FaceCardSpritesAddressableGroupLabel;
    public int CardToPlaceOnBoard = 8;
    public static Action < List<Sprite> > GameStartEvent;

    [SerializeField] private List<Sprite> _cardFace = new List<Sprite>();
    [SerializeField] private GameBoard _gameBoard = default;
    [SerializeField] private AddressableDownloader _addressableDownloader;

    void Start()
    {
        GameStartEvent += StartSettingGameBoard;
        _addressableDownloader.LoadSpritesByGroupLabel(FaceCardSpritesAddressableGroupLabel);
    }
    private void OnDestroy()
    {
        GameStartEvent -= StartSettingGameBoard;
    }
    public void StartSettingGameBoard(List<Sprite> loadedSprites)
    {
        _cardFace = loadedSprites;
        InitializeBoard();
    }
    //Function Shuffles the sprites so everytime new face card are spawn on to the board.
    private void InitializeBoard()
    {
        ShuffleCards(ref _cardFace);
        _gameBoard.SetBoard(GetShuffledFaceCards());
    }

    // Returns the number of cards to be placed on board from sprite list.
    private List<Sprite> GetShuffledFaceCards()
    {
        return _cardFace.Take(CardToPlaceOnBoard).ToList();
    }

    //For shuffling objects provide through List.
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
}