using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    float temp = 5f;
    private Camera cam;

    [SerializeField] float puzzleSpaceScale = 6;


    [ContextMenu("hesapla")]
    public void AdjustCameraSizeToRectangle(Vector2 size)
    {
        Camera cam = Camera.main;
        
        Vector3 scale = new Vector3();
        scale.x = size.x * puzzleSpaceScale;
        scale.y = size.y * puzzleSpaceScale;
        scale.z = 1f;
       

        float rectWidth = scale.x + 8f;
        float rectHeight = scale.y + 34f;

        float aspectRatio = (float)Screen.width / Screen.height;

        float cameraHeight = (rectHeight / 2f);
        float widthBasedSize = (rectWidth / 2f) / aspectRatio;

        cam.orthographicSize = Mathf.Max(cameraHeight, widthBasedSize);
    }
}
