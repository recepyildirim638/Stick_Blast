using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameWinPanelUI : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] GameObject panel;
    [SerializeField] RectTransform menu;
    void Start()
    {
        newGameButton.onClick.AddListener(() => {
            NewGameButtonOnClick();
        });
    }
    private void OnEnable()
    {
        ActionManager.GameEndWin += GameEndWinFunc;
    }

    private void OnDisable()
    {
        ActionManager.GameEndWin -= GameEndWinFunc;
    }

    private void GameEndWinFunc()
    {
        panel.SetActive(true);
        menu.gameObject.SetActive(true);
        menu.anchoredPosition = new Vector3(0, 2000f, 0);
        menu.DOAnchorPosY(0f, 0.6f).SetDelay(1f);
       
    }

    private void NewGameButtonOnClick()
    {
        GameManager.Instance.SetNewGame();
        panel.SetActive(false);
    }
}
