using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridCornerManager : MonoBehaviour
{
    BaseGrid[] corners;
    List<BaseGrid> cornersHoverList = new List<BaseGrid>();

    GridVerticalManager gridVerticalManager;
    GridHorizontalManager gridHorizontalManager;
    BaseGridPool pool;

    int width = 5;
    int height = 5;

   
    [SerializeField] Transform baseParent;

    private void Start()
    {
        gridVerticalManager = GetComponent<GridVerticalManager>();
        gridHorizontalManager = GetComponent<GridHorizontalManager>();
        pool = GetComponent<BaseGridPool>();
    }

    public void Create(Vector2Int size)
    {
        width = size.x;
        height = size.y;
        int index = 0;
        corners = new BaseGrid[(size.x + 1) * (size.y + 1)];

        for (int i = 0; i < size.y + 1; i++)
        {
            for(int j = 0; j < size.x + 1; j++)
            {
                GameObject obj = pool.GetPoolItem(BASEGRID_TYPE.CORNER);
                obj.transform.parent = baseParent;
                obj.transform.localPosition = new Vector2(j * 6f, i * (-6f));
                BaseGrid baseGrid = obj.GetComponent<BaseGrid>();
                baseGrid.SetPoint(new Vector2Byte((byte) j, (byte) i));

                corners[index] = baseGrid;
                index++;
            }
        }
    }
    

    public BaseGrid[] GetAllCorners() => corners;

    public void ResetAll()
    {
        cornersHoverList.Clear();
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i].ClearGrid();
            corners[i].gameObject.SetActive(false);
        }
    }

    public void ClearHorizontal(int line)
    {
        byte X = 0;
        byte Y = (byte)line;

        Vector2Byte vert = new Vector2Byte(X, Y);

        for (int i = 0; i < width + 1; i++)
        {
            Vector2Byte p = vert - new Vector2Byte(0, 1);

            if (!ControlNeigborgVerticle(p))
            {
                GetGrid(vert).ClearGrid();
            }
           
            X++;
            vert.x = X;
        }

        X = 0;
        Y++;

        vert.x = X;
        vert.y = Y;

        for (int i = 0; i < width + 1; i++)
        {
            if (!ControlNeigborgVerticle(vert))
            {
                GetGrid(vert).ClearGrid();
            }

   
            X++;
            vert.x = X;
        }
    }

    private bool ControlNeigborgVerticle(Vector2Byte point)
    {
        return gridVerticalManager.PointIsFill(point);
    }


    public void ClearVertical(int line)
    {
        byte row = (byte) line;
        byte index = 0;

        Vector2Byte vert = new Vector2Byte(row, index);

        for (int i = 0; i < height + 1; i++)
        {
            if(ControlNeigborg(vert) == false)
            {
                GetGrid(vert).ClearGrid();
               
            }
            index++;
            vert.y = index;

        }

        index = 0;
        row++;
        vert.x = row;
        vert.y = index;

        for (int i = 0; i < height + 1; i++)
        {
           if(ControlNeigborg2(vert) == false)
           {
                GetGrid(vert).ClearGrid();
                
           }
           index++;
           vert.y = index;
        }
    }

    private bool ControlNeigborg(Vector2Byte point)
    {
        bool result = gridHorizontalManager.PointIsFill(point - new Vector2Byte(1,0));

        if (result) 
            return true;

         return false;
    }
    private bool ControlNeigborg2(Vector2Byte point)
    {
        bool result = false;

        result = gridHorizontalManager.PointIsFill(point);

        if (result)
            return true;

        return false;
    }

    public void Placement()
    {
        for (int i = 0; i < cornersHoverList.Count; i++)
            cornersHoverList[i].Placemnt();

        cornersHoverList.Clear();
    }

    public void Hover()
    {
        for (int i = 0; i < cornersHoverList.Count; i++)
            cornersHoverList[i].Hover();
    }

    public void UnHoverPuzzle()
    {
        for (int i = 0; i < cornersHoverList.Count; i++)
            cornersHoverList[i].UnHover();

        cornersHoverList.Clear();
    }
    public bool HoverPuzzleHorizontal(PuzzleData puzzleData, BaseGrid baseGrid)
    {
        for (int i = 0; i < puzzleData.Horizontal.Length; i++)
        {
            Vector2Byte pointVector = baseGrid.GetPoint() + puzzleData.Horizontal[i];
            BaseGrid grid = GetGrid(pointVector);

            if (grid != null)
                AddGrid(grid);
            else
                return false;
        }
        return true;
    }

    public bool HoverPuzzleVertical(PuzzleData puzzleData, BaseGrid baseGrid)
    {
        for (int i = 0; i < puzzleData.Vertival.Length; i++)
        {
            Vector2Byte pointVector = baseGrid.GetPoint() + puzzleData.Vertival[i];
            BaseGrid grid = GetGrid(pointVector);

            if (grid != null)
                AddGrid(grid);
            else
                return false;
        }
        return true;
    }

    private void AddGrid(BaseGrid grid)
    {
        if (!cornersHoverList.Any(p => p.GetHashCode() == grid.GetHashCode()))
            cornersHoverList.Add(grid);
    }

    private BaseGrid GetGrid(Vector2Byte point)
    {
        for (int i = 0; i < corners.Length; i++)
        {
            if (corners[i].GetPoint() == point)
                return corners[i];
        }
        return null;
    }
}
