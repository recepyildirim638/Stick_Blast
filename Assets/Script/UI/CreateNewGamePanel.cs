using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CreateNewGamePanel : MonoBehaviour
{
    [SerializeField] RowButtonUI rowButton;
    [SerializeField] ColButtonUI colButton;
    [SerializeField] GameObject panel;
    [SerializeField] Button button;

    private void OnEnable()
    {
        ActionManager.ResetGameArea += ResetAll;
    }
    private void OnDisable()
    {
        ActionManager.ResetGameArea -= ResetAll;
    }

    public void ResetAll()
    {
        panel.SetActive(true);

    }

    private void Start()
    {
        button.onClick.AddListener(() => ButtonOnClick());
    }

    private void ButtonOnClick()
    {
        Vector2Int size = new Vector2Int(rowButton.GetVal(), colButton.GetVal());
       
        GridManager.Instance.CreatNewGame(size);
        panel.SetActive(false);
    }
}