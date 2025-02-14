using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    float temp = 5f;
    private Camera cam;

 
 
    [SerializeField] float oran;
    [SerializeField] float space;

    [ContextMenu("hesapla")]
    public void AdjustCameraSizeToRectangle(Vector2 size)
    {
        Vector3 scale = new Vector3();
        scale.x = size.x * oran;
        scale.y = size.y * oran;
        scale.z = 1f;
       


        Camera cam = Camera.main;
        //Vector3 bounds = new Vector3();
        //bounds.x = (size.x * 1f);
        //bounds.y = (size.y * 1f);

        float rectWidth = scale.x + 8f;
        float rectHeight = scale.y;

        float aspectRatio = (float)Screen.width / Screen.height;

        float cameraHeight = (rectHeight / 2f) + space;
        float widthBasedSize = (rectWidth / 2f) / aspectRatio;

       // cam.orthographicSize = Mathf.Max(cameraHeight, widthBasedSize);
        cam.orthographicSize = widthBasedSize;


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
