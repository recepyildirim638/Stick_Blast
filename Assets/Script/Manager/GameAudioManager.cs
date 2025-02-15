using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GameAudioManager : MonoSingleton<GameAudioManager>
{
    [Inject] DataManager dataManager;
    [SerializeField] AudioMixer audioMixer;

    public const string MASTER = "Master";
    public const string MUSIC_VOLUME = "musicVolume";
    public const string SFX_VOLUME = "sfxVolume";

    [SerializeField] SoundButton soundButton;

    [SerializeField] AudioSource puzzlePlacement;
    [SerializeField] AudioSource puzzleCreated;
    [SerializeField] AudioSource fillSFX;
    [SerializeField] AudioSource clearLineSFX;

    public void Initalize()
    {
        GameLoaded(dataManager.GetMainData().sound);
    }

    private void OnEnable()
    {
        ActionManager.ChangeSound += ChangeSoundFunc;
    }
    private void OnDisable()
    {
        ActionManager.ChangeSound -= ChangeSoundFunc;
    }

    private void ChangeSoundFunc()
    {
        MainData mainData = dataManager.GetMainData();

        if (mainData.sound)
            mainData.sound = false;

        else mainData.sound = true;

        GameLoaded(mainData.sound);
    }

    public void GameLoaded(bool sound)
    {
        SetSound(sound);
    }

   
    private void SetSound(bool sound)
    {

        if (sound)
        {
            audioMixer.SetFloat(SFX_VOLUME, 0f);
            soundButton.SetSound(true);

        }
        else
        {
            audioMixer.SetFloat(SFX_VOLUME, -80f);
            soundButton.SetSound(false);
            DOVirtual.DelayedCall(.2f, () => audioMixer.SetFloat(SFX_VOLUME, -80f));
        }
    }

    public void PlaySound(AUDIO_TYPE audioType)
    {
        switch(audioType)
        {
            case AUDIO_TYPE.PLACEMENT_PUZZLE:
                puzzlePlacement.Play();
                break;
            case AUDIO_TYPE.CREATED_PUZZLE:
                puzzleCreated.Play();
                break;
            case AUDIO_TYPE.FILL:
                fillSFX.Play();
                break;
            case AUDIO_TYPE.CLEAR_LINE:
                clearLineSFX.Play();
                break;

            default: break;
        }
    }
}
