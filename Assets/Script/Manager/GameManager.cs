using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoSingleton<GameManager>
{
    [Inject] private DataManager dataManager;
   

    MainData mainData;

    private void Awake()
    {
        dataManager.Initalize();
        mainData = dataManager.GetMainData();
        GameAudioManager.Instance.GameLoaded(mainData.sound);

        SetNewGame();
    }
    private void OnEnable()
    {
        ActionManager.GridAreaReady += GridAreaReadyFunc;
        ActionManager.ChangeSound += ChangeSoundFunc;
    }
    private void OnDisable()
    {
        ActionManager.GridAreaReady -= GridAreaReadyFunc;
        ActionManager.ChangeSound -= ChangeSoundFunc;
    }

    private void GridAreaReadyFunc()
    {
      
    }

    private void ChangeSoundFunc()
    {
        if (mainData.sound)
            mainData.sound = false;
        else mainData.sound = true;

        GameAudioManager.Instance.GameLoaded(mainData.sound);
    }

    public void GameWin()
    {
        mainData.level++;
    }

    public void GameFail()
    {
      
    }

    public void SetNewGame()
    {
    }
}
