using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReGameButton : MonoBehaviour
{
    [SerializeField] Button button;
    private void Start()
    {
      
        button.onClick.AddListener(() => ButtonOnClick());
    }

    private void ButtonOnClick()
    {
        ActionManager.ResetGameArea?.Invoke();
    }
}
