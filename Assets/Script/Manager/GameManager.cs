using DG.Tweening;
using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static bool ISGAME  = false;
    public DataManager dataManager;
    [SerializeField] AudioManager audioManager;
    MainData mainData;

    private void Awake()
    {
        dataManager.Initalize();
        mainData = dataManager.GetMainData();
        audioManager.GameLoaded(mainData.sound);

        ISGAME = true;
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

        audioManager.GameLoaded(mainData.sound);
    }

    public void AddMove()
    {
        
      //  moveCountUI.SetMoveIndex(moveCount);
    }


    public void GameWin()
    {
        ISGAME = false;
        mainData.level++;

        //DOVirtual.DelayedCall(1.6f, () => {
        //    GridManager.Instance.ResetAll();
        //    PuzzleManager.Instance.ResetAll();
        //});

    }

    public void GameFail()
    {
        ISGAME = false;
        //DOVirtual.DelayedCall(1.6f, () => {
        //    GridManager.Instance.ResetAll();
        //    PuzzleManager.Instance.ResetAll();
        //});

    }

    public void SetNewGame()
    {
       // levelTaskManager.SetLevelTask();
       // PuzzleManager.Instance.CreatePuzzle();
        ISGAME = true;
       // levelUI.SetLevelIndex(mainData.level);

        //moveCount = 0;
        //moveCountUI.SetMoveIndex(moveCount);
    }
}
