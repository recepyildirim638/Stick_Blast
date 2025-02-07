using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHorizontalManager : MonoBehaviour
{
    [SerializeField] BaseGrid[] horizontallLines;
    List<BaseGrid> horizontalLineList = new List<BaseGrid>();

    GridVerticalManager gridVerticalManager;

    private void Start()
    {
        gridVerticalManager = GetComponent<GridVerticalManager>();
    }
    public BaseGrid[] GetHorizontallLines() => horizontallLines;

    int width = 5;
    int height = 5;

    public void ResetAll()
    {
        horizontalLineList.Clear();
        for (int i = 0; i < horizontallLines.Length; i++)
        {
            horizontallLines[i].ClearGrid();
        }
    }

    public void ClearHorizontal(int line)
    {
        byte X = 0;
        byte Y = (byte)line;

        Vector2Byte vert = new Vector2Byte(X, Y);

        for (int i = 0; i < width ; i++)
        {
            Vector2Byte p = vert - new Vector2Byte(0, 1);
            Vector2Byte p2 = p + new Vector2Byte(1, 0);

            if (!ControlNeigborg(p) || !ControlNeigborg(p2))
            {
                GetHorizontalLineGrid(vert).ClearGrid();
            }
            
            X++;
            vert.x = X;
        }

        X = 0;
        Y++;

        vert.x = X;
        vert.y = Y;

        for (int i = 0; i < width ; i++)
        {
            Vector2Byte p2 = vert + new Vector2Byte(1, 0);

            if (!ControlNeigborg(vert) || !ControlNeigborg(p2))
            {
                GetHorizontalLineGrid(vert).ClearGrid();

            }
            X++;
            vert.x = X;
        }
    }

    private bool ControlNeigborg(Vector2Byte point)
    {
       return gridVerticalManager.PointIsFill(point);
    }

    public void ClearVertical(int line)
    {
        byte index = 0;
        byte row = (byte) line;
        Vector2Byte vert = new Vector2Byte(row, index);

        for (int i = 0; i < height + 1; i++)
        {
            GetHorizontalLineGrid(vert).ClearGrid();
            index++;
            vert.y = index;
        }
    }

    public void Placement()
    {
        for (int i = 0; i < horizontalLineList.Count; i++)
            horizontalLineList[i].Placemnt();

        horizontalLineList.Clear();
    }

    public void Hover()
    {
        for (int i = 0; i < horizontalLineList.Count; i++)
            horizontalLineList[i].Hover();
    }

    public void UnHoverPuzzle()
    {
        for (int i = 0; i < horizontalLineList.Count; i++)
            horizontalLineList[i].UnHover();

        horizontalLineList.Clear();
    }

    public bool SuccessStatus(PuzzleData puzzleData, Vector2Byte point)
    {
        for (int i = 0; i < puzzleData.Horizontal.Length; i += 2)
        {
            Vector2Byte addVectorByte = puzzleData.Horizontal[i + 1] - puzzleData.Horizontal[i];
            Vector2Byte pointVector = point + puzzleData.Horizontal[i] + addVectorByte;
            pointVector -= new Vector2Byte(1, 0);
            BaseGrid grid = GetHorizontalLineGrid(pointVector);

            if(grid == null)
                return false;

            if (grid.GetFill())
                return false;
        }
        return true;
    }

    public bool HoverPuzzle(PuzzleData puzzleData, BaseGrid baseGrid)
    {
        for (int i = 0; i < puzzleData.Horizontal.Length; i += 2)
        {
            Vector2Byte addVectorByte = puzzleData.Horizontal[i + 1] - puzzleData.Horizontal[i];
            Vector2Byte pointVector = baseGrid.GetPoint() + puzzleData.Horizontal[i] + addVectorByte;
            pointVector -= new Vector2Byte(1, 0);
            BaseGrid grid = GetHorizontalLineGrid(pointVector);

            if (grid.GetFill())
                return false;

            horizontalLineList.Add(grid);
        }
        return true;
    }

    public BaseGrid GetHorizontalLineGrid(Vector2Byte point)
    {
        for (int i = 0; i < horizontallLines.Length; i++)
        {
            if (horizontallLines[i].GetPoint() == point)
                return horizontallLines[i];
        }
        return null;
    }

    public bool PointIsFill(Vector2Byte point)
    {
        BaseGrid grid = GetHorizontalLineGrid(point);

        if(grid == null) return false;

        return grid.GetFill();
    }
}
