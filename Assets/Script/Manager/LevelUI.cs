using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    

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
        int level = AccsessManager.Instance.dataManager.GetMainData().level;
        SetLevelIndex(level);
    }
    public void SetLevelIndex(int val) => levelText.text =  "Level: " + (val + 1).ToString();
}
