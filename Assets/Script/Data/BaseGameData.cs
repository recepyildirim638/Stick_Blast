using System.Collections.Generic;
using UnityEngine;

public class BaseGameData : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] protected MyGameData gameData;

    [Header("Behaviour")]
    protected MainData mainData;

    protected void LoadGameData()
    {
        SaveManager.LoadGameData(gameData);
        mainData = gameData.mainData;
    }
    protected void SaveGameData()
    {
        gameData.mainData = mainData;
        SaveManager.SaveGameData(gameData);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveGameData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}