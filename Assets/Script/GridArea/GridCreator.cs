using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField] GameObject backGround;

    [HideInInspector] Vector2Int gridSize;

    public Vector2Int GetGridSize() => gridSize;
   
    public void Create(Vector2Int gridSize)
    {
        this.gridSize = gridSize;

        GetComponent<GridCornerManager>().Create(gridSize);
        GetComponent<GridVerticalManager>().Create(gridSize);
        GetComponent<GridHorizontalManager>().Create(gridSize);
        GetComponent<GridFillManager>().Create(gridSize);

        Vector3 size = new Vector3();
        size.x = (gridSize.x + 1) * 6f;
        size.y = (gridSize.y + 1) * 6f;
        size.z = 1f;

        backGround.transform.localScale = size;

        size.x = gridSize.x * 3f;
        size.y = (gridSize.y - 1) * -3f;

        backGround.transform.localPosition = size;

        transform.position = -size;

        AccsessManager.Instance.cameraManager.AdjustCameraSizeToRectangle(gridSize);
        ActionManager.GridAreaReady?.Invoke();  
    }
}
