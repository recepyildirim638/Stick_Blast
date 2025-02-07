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

        // Cihaz�n ekran oran� (geni�lik / y�kseklik)
        float screenRatio = (float)Screen.width / Screen.height;

        // Kamera i�in gereken geni�li�i hesapla
        float requiredWidth = gameHeight * screenRatio;

        // E�er gereken geni�lik oyun alan�ndan b�y�kse, geni�li�e g�re ayarla
        if (requiredWidth > gameWidth)
        {
            Debug.Log("aaa");
            cam.orthographicSize = (gameHeight / 2f) * temp;
        }
        else // Aksi takdirde geni�li�e g�re ayarla
        {
            cam.orthographicSize = (gameWidth / (2f * screenRatio) ) * temp;
        }
    }
}
