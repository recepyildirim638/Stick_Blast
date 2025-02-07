using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float temp = 1f;
    private void Awake()
    {
        AdjustCameraSize();
    }
    private void Update()
    {
        AdjustCameraSize();
    }
    void AdjustCameraSize()
    {
        Camera cam = Camera.main;

        float gameWidth = Screen.width;
        float gameHeight = Screen.height;

        // Cihazýn ekran oraný (geniþlik / yükseklik)
        float screenRatio = (float)Screen.width / Screen.height;

        // Kamera için gereken geniþliði hesapla
        float requiredWidth = gameHeight * screenRatio;

        // Eðer gereken geniþlik oyun alanýndan büyükse, geniþliðe göre ayarla
        if (requiredWidth > gameWidth)
        {
            Debug.Log("aaa");
            cam.orthographicSize = (gameHeight / 2f) * temp;
        }
        else // Aksi takdirde geniþliðe göre ayarla
        {
            cam.orthographicSize = (gameWidth / (2f * screenRatio) ) * temp;
        }
    }
}
