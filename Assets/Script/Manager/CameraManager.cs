using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float temp = 5f;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
        AdjustCameraSize();
    }

   

    void AdjustCameraSize()
    {

        float screenRatio = (float)Screen.width / Screen.height;

       
        float gameWidth = 10f; 
        float gameHeight = 5f;  


        float newSize = (gameHeight / 2f) * temp;


        if (gameWidth / gameHeight > screenRatio)
        {
            newSize = (gameWidth / (2f * screenRatio)) * temp;
        }


        cam.orthographicSize = newSize;
    }
}
