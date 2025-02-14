using UnityEngine;
using UnityEngine.UI;

public class CreateNewGamePanel : MonoBehaviour
{
    [SerializeField] RowButtonUI rowButton;
    [SerializeField] ColButtonUI colButton;
    [SerializeField] GameObject panel;
    [SerializeField] Button button;

    private void Start()
    {
        button.onClick.AddListener(() => ButtonOnClick());
    }

    private void ButtonOnClick()
    {
        Vector2Int size = new Vector2Int(rowButton.GetVal(), colButton.GetVal());
        AccsessManager.Instance.gridManager.CreatNewGame(size);
        panel.SetActive(false);
    }
}