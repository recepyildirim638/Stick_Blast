using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private DataManager dataManager;
   
    private void Awake()
    {
        dataManager.Initalize();
        GameAudioManager.Instance.Initalize();
    }
}
