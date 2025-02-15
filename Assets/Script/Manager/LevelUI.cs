using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;

    [Inject]
    DataManager dataManager;


    private void OnEnable()
    {
        ActionManager.GridAreaReady += GridAreaReadyFunc;

    }
    private void OnDisable()
    {
        ActionManager.GridAreaReady -= GridAreaReadyFunc;

    }

    private void GridAreaReadyFunc()
    {
        int level = dataManager.GetMainData().level;
        SetLevelIndex(level);
    }
    public void SetLevelIndex(int val) => levelText.text =  "Level: " + (val + 1).ToString();
}
