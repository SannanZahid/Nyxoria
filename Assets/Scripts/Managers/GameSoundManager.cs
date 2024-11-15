using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : Singleton<GameSoundManager>
{
    public enum SoundType { CardMatch, CardMisMatch, CardFlip, GameFail }

    [SerializeField] private int _soundPercentage;
    [SerializeField] private AudioClip _matchClip;
    [SerializeField] private AudioClip _misMatchClip;
    [SerializeField] private AudioClip _cardFlipSoundClip;
    [SerializeField] private AudioClip _gameFailClip;
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    public int GetSoundVolume()
    {
        return _soundPercentage;
    }

    public void SetSoundVolume(int volume)
    {
        _soundPercentage = volume;
        GamePlayerPreferenceManager.SetSound(volume);
    }

    public void PlaySoundOneShot(SoundType playSound)
    {
        switch (playSound)
        {
            case SoundType.CardMatch:
                {
                    _audioSource.PlayOneShot(_matchClip);
                    break;
                }
            case SoundType.CardMisMatch:
                {
                    _audioSource.PlayOneShot(_misMatchClip);
                    break;
                }
            case SoundType.CardFlip:
                {
                    _audioSource.PlayOneShot(_cardFlipSoundClip);
                    break;
                }
            case SoundType.GameFail:
                {
                    _audioSource.PlayOneShot(_gameFailClip);
                    break;
                }
        }
    }
}
