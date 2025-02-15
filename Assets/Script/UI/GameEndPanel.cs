using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPanel : MonoBehaviour
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
        ActionManager.GameEndFail += GameEndFailFunc;
    }

    private void OnDisable()
    {
        ActionManager.GameEndFail -= GameEndFailFunc;
    }

    private void GameEndFailFunc()
    {
        panel.SetActive(true);
        menu.gameObject.SetActive(true);
        menu.anchoredPosition = new Vector3(0, 2000f, 0);
        menu.DOAnchorPosY(0f, 0.6f).SetDelay(1f);
    }

    private void NewGameButtonOnClick()
    {
        menu.gameObject.SetActive(false);
        panel.SetActive(false);
        ActionManager.ResetGameArea?.Invoke();
    }
  
}
