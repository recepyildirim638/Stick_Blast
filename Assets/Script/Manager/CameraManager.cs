using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float temp = 1f;
    private void Start()
    {
        AdjustCameraSize();
    }
   
    void AdjustCameraSize()
    {
        Camera cam = Camera.main;

        float gameWidth = Screen.width;
        float gameHeight = Screen.height;

        float screenRatio = (float)Screen.width / Screen.height;

        float requiredWidth = gameHeight * screenRatio;


        if (requiredWidth > gameWidth)
        {
            cam.orthographicSize = (gameHeight / 2f) * temp;
        }
        else
        {
            cam.orthographicSize = (gameWidth / (2f * screenRatio) ) * temp;
        }
    }
}
