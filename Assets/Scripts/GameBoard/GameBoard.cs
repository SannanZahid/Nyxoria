using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <Class-Purpose>  
/// Class for managing state of cards on board as well as checking cards interation
/// 

public class GameBoard : MonoBehaviour
{
    [Header("- Time Cards are shown at Start of game -")]
    [Space(10)]
    [SerializeField] private float StartGameAfterTimer = 1f;

    [Header("- Card Settings -")]
    [Space(10)]
    [SerializeField] private Transform _cardPrefab = default;
    [SerializeField] private Transform _boardWidgetHolder = default;
    [SerializeField] private float _cellSpacing = 5f;

    private List<Transform> _spawnCards = new List<Transform>();
    private Transform _tempCard = default;
    private Card _previousCard;
    private enum CardValicationstate { FirstCardCheck, SecondCardCheck };
    private CardValicationstate _cardCheckState = CardValicationstate.FirstCardCheck;
    private ScoreSystem _scoreSystem;
    /// Takes face card sprites and pass it to card creation  
    public void SetBoard(List<Sprite> selectedCardFace)
    {
        _scoreSystem = new ScoreSystem();
        ScaleCardToFitContainer(selectedCardFace[0], (float)selectedCardFace.Count / 2);

        for (int i = 0; i < selectedCardFace.Count; i++)
        {
            CreateCard(i, selectedCardFace[i]);
            CreateCard(i, selectedCardFace[i]);
        }

        ShuffleAndSetToBoard();
        StartCoroutine(StartGame());
    }
    public void ValidateCardCombination(Card currentCard)
    {
        switch (_cardCheckState)
        {
            case CardValicationstate.FirstCardCheck:
                {
                    _previousCard = currentCard;
                    _cardCheckState = CardValicationstate.SecondCardCheck;

                    break;

                }
            case CardValicationstate.SecondCardCheck:
                {
                    if (_previousCard.CardID.Equals(currentCard.CardID))
                    {
                        StartCoroutine(DeactivateMatchingCards(_previousCard, currentCard));
                        _scoreSystem.CardsMatched_Score();
                    }
                    else
                    {
                        StartCoroutine(ResetCardsSelected(_previousCard, currentCard));
                        _scoreSystem.CardsMisMatchedScore();
                    }

                    _cardCheckState = CardValicationstate.FirstCardCheck;

                    break;
                }
        }
    }

    // Shuffles the created card and sets the Cards in to the UI Canvas containor
    private void ShuffleAndSetToBoard()
    {
        GameController.ShuffleCards(ref _spawnCards);
        foreach (Transform card in _spawnCards)
        {
            card.SetParent(_boardWidgetHolder);
            card.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    private void CreateCard(int id, Sprite cardFront)
    {
        _tempCard = Instantiate(_cardPrefab.gameObject).transform;
        _tempCard.gameObject.SetActive(true);
        _tempCard.GetComponent<Card>().Init(id, cardFront, ValidateCardCombination);
        _spawnCards.Add(_tempCard);
    }


    private IEnumerator ResetCardsSelected(Card card1, Card card2)
    {
        yield return new WaitForSeconds(1f);
        card1.ResetCard();
        card2.ResetCard();
    }


    private IEnumerator DeactivateMatchingCards(Card card1, Card card2)
    {
        yield return new WaitForSeconds(1f);
        card1.DeactivateCardAnimated();
        card2.DeactivateCardAnimated();
        _spawnCards.Remove(card1.transform);
        _spawnCards.Remove(card2.transform);
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(StartGameAfterTimer);
        foreach (Transform card in _spawnCards)
        {
            card.GetComponent<Card>().ShowCardSide(Card.CardSides.Back);
        }
    }

    //scalling cards according to containor widget
    private void ScaleCardToFitContainer(Sprite referenceCardSize, float gridSize)
    {
        var boardContainer = _boardWidgetHolder.GetComponent<GridLayoutGroup>();
        var rectTransform = _boardWidgetHolder.GetComponent<RectTransform>();

        float containerWidth = rectTransform.rect.width;
        float containerHeight = rectTransform.rect.height;
        float spriteWidth = referenceCardSize.rect.width;
        float spriteHeight = referenceCardSize.rect.height;

        float forecastCellWidth = (containerWidth - (_cellSpacing * gridSize)) / gridSize;
        float forecastCellHeight = (containerHeight - (_cellSpacing * gridSize)) / gridSize;

        float widthRatio = forecastCellWidth / spriteWidth;
        float heightRatio = forecastCellHeight / spriteHeight;

        Vector2 newSize = CalculateNewSize(spriteWidth, spriteHeight, widthRatio, heightRatio);
        boardContainer.cellSize = newSize;
    }

    private Vector2 CalculateNewSize(float spriteWidth, float spriteHeight, float widthRatio, float heightRatio)
    {
        if (heightRatio > 1 && widthRatio > 1)
        {
            return new Vector2(spriteWidth, spriteHeight);
        }

        float optimalRatio = Mathf.Min(widthRatio, heightRatio);
        return new Vector2(spriteWidth * optimalRatio, spriteHeight * optimalRatio);
    }

}