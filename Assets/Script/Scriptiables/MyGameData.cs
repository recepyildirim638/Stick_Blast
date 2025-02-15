using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 2)]
public class MyGameData : ScriptableObject
{
    [Header("Main Data")]
    public MainData mainData = new MainData();

   


    [ContextMenu("ResetData")]
    public void ResetData()
    {
        mainData.vibration = true;
        mainData.sound = true;
        mainData.music = true;

        mainData.level = 0;
        mainData.minGridSize = 4;
        mainData.maxGridSize = 10;

        PlayerPrefs.DeleteAll();
        SaveManager.SaveGameData(this);
    }

    [ContextMenu("SaveData")]
    public void Save()
    {
        SaveManager.SaveGameData(this);
    }
}
