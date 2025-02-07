using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static bool ISGAME  = false;
    [SerializeField] LevelTaskManager levelTaskManager;
    public DataManager dataManager;
    [SerializeField] AudioManager audioManager;
    [SerializeField] LevelUI levelUI;
    [SerializeField] MoveCount moveCountUI;
    MainData mainData;

    int moveCount = 0;
    private void Awake()
    {
        dataManager.Initalize();
        mainData = dataManager.GetMainData();
        audioManager.GameLoaded(mainData.sound);
        levelUI.SetLevelIndex(mainData.level);
        moveCountUI.SetMoveIndex(moveCount);

        ISGAME = true;
        SetNewGame();
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
        if (mainData.sound)
            mainData.sound = false;
        else mainData.sound = true;

        audioManager.GameLoaded(mainData.sound);
    }

    public void AddMove()
    {
        moveCount++;
        moveCountUI.SetMoveIndex(moveCount);
    }


    public void GameWin()
    {
        ISGAME = false;
        mainData.level++;

        DOVirtual.DelayedCall(1.6f, () => {
            GridManager.Instance.ResetAll();
            PuzzleManager.Instance.ResetAll();
        });

    }

    public void GameFail()
    {
        ISGAME = false;
        DOVirtual.DelayedCall(1.6f, () => {
            GridManager.Instance.ResetAll();
            PuzzleManager.Instance.ResetAll();
        });

    }

    public void SetNewGame()
    {
        levelTaskManager.SetLevelTask();
        PuzzleManager.Instance.CreatePuzzle();
        ISGAME = true;
        levelUI.SetLevelIndex(mainData.level);

        moveCount = 0;
        moveCountUI.SetMoveIndex(moveCount);
    }
}
