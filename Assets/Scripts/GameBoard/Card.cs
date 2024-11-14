using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <Class-Purpose>
/// Class for managing state of cards as well as handeling card functionality
/// </Class-Purpose>

public class Card : MonoBehaviour
{
    public int CardID { private set; get; }
    public enum CardSides { Front, Back }

    [Header("- Front and Back Side Card Images -")]
    [SerializeField] private Transform _cardFront, _cardBack;

    private Button _cardInteractionBtn;
    private Action<Card> _callbackSelectedCardToGameBoard;
    private float _cardRotationVelocity;
    private bool flipAnimFlag = true;

    /// <summary>
    /// Initializes the card with the given ID and face sprite, and sets up the button click listener.
    /// </summary>
    public void Init(int cardId, Sprite cardFace, Action<Card> callbackSelectedCard)
    {
        _callbackSelectedCardToGameBoard = callbackSelectedCard;
        CardID = cardId;
        _cardFront.GetComponent<Image>().sprite = cardFace;
        _cardInteractionBtn = transform.gameObject.AddComponent<Button>();
        _cardInteractionBtn.onClick.AddListener(CardInteraction);
        ShowCardSide(CardSides.Front);
    }

    /// <summary> 
    // for calling card side functionality
    /// </summary> 
    public void ShowCardSide(CardSides cardSide)
    {
        if (!flipAnimFlag)
        {
            return;
        }

        switch (cardSide)
        {
            case CardSides.Front:
                {
                    StartCoroutine(CardAnimationRotateAnimation(_cardBack, _cardFront));
                    break;
                }
            case CardSides.Back:
                {
                    StartCoroutine(CardAnimationRotateAnimation(_cardFront, _cardBack));
                    break;
                }
        }

        GameSoundManager.Instance.PlaySoundOneShot(GameSoundManager.SoundType.CardFlip);
    }

    /// <summary> 
    /// Handles user interaction with the card.
    /// </summary>
    public void CardInteraction()
    {
        _cardInteractionBtn.interactable = false;
        _callbackSelectedCardToGameBoard.Invoke(this);
        ShowCardSide(CardSides.Front);
    }

    public void ResetCard()
    {
        ShowCardSide(CardSides.Back);
        _cardInteractionBtn.interactable = true;
    }

    public void ResetCard(int cardId, Sprite cardFace)
    {
        CardID = cardId;
        _cardFront.GetComponent<Image>().sprite = cardFace;
        ShowCardSide(CardSides.Front);
        _cardInteractionBtn.interactable = true;
    }

    public void DeactivateCard()
    {
        _cardFront.gameObject.SetActive(false);
        _cardBack.gameObject.SetActive(false);
        _cardInteractionBtn.interactable = false;
        StopAllCoroutines();
    }

    public void DeactivateCardAnimated()
    {
        AnimateMatchCard();
        _cardInteractionBtn.interactable = false;
    }

    private void AnimateMatchCard()
    {
        StartCoroutine(ScaleOverTime(_cardFront, Vector3.zero, 0.25f));
        StartCoroutine(ScaleOverTime(_cardBack, Vector3.zero, 0.25f));
    }

    private IEnumerator CardAnimationRotateAnimation(Transform cardFront, Transform cardBack)
    {
        flipAnimFlag = false;

        SetActiveState(cardFront, true);
        SetActiveState(cardBack, false);

        ResetRotation(cardFront);
        SetRotation(cardBack, new Vector3(0.0f, 90.0f, 0.0f));

        yield return Rotate(cardFront, 90f, 89.0f, () =>
        {
            SetActiveState(cardFront, false);
            SetActiveState(cardBack, true);
        });

        yield return Rotate(cardBack, 0f, 0.1f);

        ResetRotation(cardFront);
        ResetRotation(cardBack);
        flipAnimFlag = true;
    }

    private void SetActiveState(Transform obj, bool state)
    {
        obj.gameObject.SetActive(state);
    }

    private void ResetRotation(Transform obj)
    {
        obj.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void SetRotation(Transform obj, Vector3 rotation)
    {
        obj.rotation = Quaternion.Euler(rotation);
    }

    private IEnumerator Rotate(Transform obj, float targetAngle, float threshold, System.Action onCompletion = null)
    {
        while (true)
        {
            float angle = Mathf.SmoothDampAngle(obj.eulerAngles.y, targetAngle, ref _cardRotationVelocity, 0.1f);
            obj.rotation = Quaternion.Euler(0, angle, 0);

            if (Mathf.Abs(obj.eulerAngles.y - targetAngle) <= threshold)
            {
                onCompletion?.Invoke();
                break;
            }
            yield return null;
        }
    }

    private IEnumerator ScaleOverTime(Transform objectToScale, Vector3 toScale, float duration)
    {
        float elapsed = 0f; Vector3 initialScale = objectToScale.localScale;

        while (elapsed < duration) 
        { 
            elapsed += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(initialScale, toScale, elapsed / duration); 
            yield return null;
        }

        objectToScale.localScale = toScale; // Ensure it reaches the exact final scale
        objectToScale.gameObject.SetActive(false); // Hide the object
        objectToScale.localScale = Vector3.one; // Reset scale if necessary
    }
}