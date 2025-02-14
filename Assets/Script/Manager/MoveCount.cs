using TMPro;
using UnityEngine;

public class MoveCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    int moveCount = 0;
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
        moveCount = 0;
        SetMoveIndex();
    }

    public void SetMoveIndex() => levelText.text = "Move: " + (moveCount).ToString();
}
