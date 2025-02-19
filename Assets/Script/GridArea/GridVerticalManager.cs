using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVerticalManager : MonoBehaviour
{
    BaseGrid[] verticalLines;
    List<BaseGrid> verticalLineList = new List<BaseGrid>();
    GridHorizontalManager gridHorizontalManager;

    [SerializeField] Transform baseParent;
    BaseGridPool pool;
    int width = 5;
    int height = 5;
    private void Start()
    {
        gridHorizontalManager = GetComponent<GridHorizontalManager>();
        pool = GetComponent<BaseGridPool>();
    }

    public void Create(Vector2Int size)
    {
        width = size.x;
        height = size.y;

        int index = 0;
        verticalLines = new BaseGrid[(size.x + 1) * (size.y)];

        for (int i = 0; i < size.x + 1; i++)
        {
            for (int j = 0; j < size.y ; j++)
            {
                // GameObject obj = Instantiate(baseGrid, baseParent);
                GameObject obj = pool.GetPoolItem(BASEGRID_TYPE.VERTICLE);
                obj.transform.parent = baseParent;
                obj.transform.localPosition = new Vector2(i * 6f, j * (-6f));

                BaseGrid baseGrid = obj.GetComponent<BaseGrid>();
                baseGrid.SetPoint(new Vector2Byte((byte)i, (byte)j));
                verticalLines[index] = baseGrid;
                index++;
            }
        }
    }

    public void ResetAll()
    {
        verticalLineList.Clear();
        for (int i = 0; i < verticalLines.Length; i++)
        {
            verticalLines[i].ClearGrid();
            verticalLines[i].gameObject.SetActive(false);
        }
    }

    public void ClearHorizontal(int line)
    {
        byte X = 0;
        byte Y = (byte)line;

        Vector2Byte vert = new Vector2Byte(X, Y);

        for (int i = 0; i < width + 1; i++)
        {
            GetVerticalLineGrid(vert).ClearGrid();
            X++;
            vert.x = X;
        }
    }


    public void ClearVertical(int line)
    {
        byte index = 0;
        byte row = (byte)line; 

        Vector2Byte vert = new Vector2Byte(row, index);

        for (int i = 0; i < height ; i++)
        {
            Vector2Byte p = vert - new Vector2Byte(1, 0);
            Vector2Byte p2 = p + new Vector2Byte(0, 1);

            if (!ControlNeigborg(p) || !ControlNeigborg(p2))
            {
                GetVerticalLineGrid(vert).ClearGrid();
            }
            
            index++;
            vert.y = index;
        }

        index = 0;
        row++;

        vert.x = row;
        vert.y = index;

        for (int i = 0; i < height ; i++)
        {
           
            Vector2Byte p2 = vert + new Vector2Byte(0, 1);

            if (!ControlNeigborg(vert) || !ControlNeigborg(p2))
            {
                GetVerticalLineGrid(vert).ClearGrid();
            }
            index++;
            vert.y = index;
        }
    }

    private bool ControlNeigborg(Vector2Byte point)
    {
        bool result = gridHorizontalManager.PointIsFill(point);

        if (result)
            return true;

        return false;
    }

    public void Placement()
    {
        for (int i = 0; i < verticalLineList.Count; i++)
            verticalLineList[i].Placemnt();

        verticalLineList.Clear();
    }

    public void Hover()
    {
        for (int i = 0; i < verticalLineList.Count; i++)
            verticalLineList[i].Hover();
    }

    public void UnHoverPuzzle()
    {
        for (int i = 0; i < verticalLineList.Count; i++)
            verticalLineList[i].UnHover();

        verticalLineList.Clear();
    }

    public bool SuccessStatus(PuzzleData puzzleData, Vector2Byte point)
    {
        for (int i = 0; i < puzzleData.Vertival.Length; i += 2)
        {
            Vector2Byte addVectorByte = puzzleData.Vertival[i + 1] - puzzleData.Vertival[i];
            Vector2Byte pointVector = point + puzzleData.Vertival[i] + addVectorByte;
            pointVector -= new Vector2Byte(0, 1);
            BaseGrid grid = GetVerticalLineGrid(pointVector);

            if (grid == null)
                return false;

            if (grid.GetFill())
                return false;
        }
        return true;
    }


    public bool HoverPuzzle(PuzzleData puzzleData, BaseGrid baseGrid)
    {
        for (int i = 0; i < puzzleData.Vertival.Length; i += 2)
        {
            Vector2Byte addVectorByte = puzzleData.Vertival[i + 1] - puzzleData.Vertival[i];
            Vector2Byte pointVector = baseGrid.GetPoint() + puzzleData.Vertival[i] + addVectorByte;
            pointVector -= new Vector2Byte(0, 1);
            BaseGrid grid = GetVerticalLineGrid(pointVector);

            if (grid.GetFill())
                return false;

            verticalLineList.Add(grid);
        }
        return true;
    }

    public BaseGrid GetVerticalLineGrid(Vector2Byte point)
    {
        for (int i = 0; i < verticalLines.Length; i++)
        {
            if (verticalLines[i].GetPoint() == point)
                return verticalLines[i];
        }
        return null;
    }

    public bool PointIsFill(Vector2Byte point)
    {
        BaseGrid grid = GetVerticalLineGrid(point);

        if (grid == null) return false;

        return grid.GetFill();
    }
}
