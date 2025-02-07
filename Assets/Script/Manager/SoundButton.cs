using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] Button button;

    [SerializeField] GameObject deActiveImage;


    private void Start()
    {
        button.onClick.AddListener(() => { SetSoundButtonClick(); });
    }

    public void SetSound(bool sound)
    {
        if (sound)  deActiveImage.SetActive(false); 
        else deActiveImage.SetActive(true);

    }

    public void SetSoundButtonClick()
    {
        ActionManager.ChangeSound?.Invoke();
    }
}
