using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    List<int> fillVerticleList = new List<int>();
    List<int> fillHorizantalList = new List<int>();

    GridFillManager fillManager;
    GridVerticalManager gridVerticalManager;
    GridHorizontalManager gridHorizontalManager;
    GridCornerManager gridCornerManager;
    GridCreator gridCreator;

    private bool isOk = false;

    public void CreatNewGame(Vector2Int size)
    {
        gridCreator.Create(size);
    }

    private void Start()
    {
        gridCreator = GetComponent<GridCreator>();
        fillManager = GetComponent<GridFillManager>();
        gridVerticalManager = GetComponent<GridVerticalManager>();
        gridHorizontalManager = GetComponent<GridHorizontalManager>();
        gridCornerManager = GetComponent<GridCornerManager>();
    }

    public void ResetAll()
    {
        fillVerticleList.Clear();
        fillHorizantalList.Clear();
        gridVerticalManager.ResetAll();
        gridHorizontalManager.ResetAll();
        gridCornerManager.ResetAll();
        fillManager.ResetAll();

    }

    public void AddFillVerticleList(int i) => fillVerticleList.Add(i);
    public void AddFillHorzontalList(int i) => fillHorizantalList.Add(i);
    public void ComplatePlacement()
    {
        for (int i = 0; i < fillVerticleList.Count; i++)
        {
            fillManager.ClearVertical(fillVerticleList[i]);
            gridVerticalManager.ClearVertical(fillVerticleList[i]);
            gridHorizontalManager.ClearVertical(fillVerticleList[i]);
            gridCornerManager.ClearVertical(fillVerticleList[i]);
        }

        fillVerticleList.Clear();

        for (int i = 0; i < fillHorizantalList.Count; i++)
        {
            fillManager.ClearHorizontal(fillHorizantalList[i]);
            gridVerticalManager.ClearHorizontal(fillHorizantalList[i]);
            gridHorizontalManager.ClearHorizontal(fillHorizantalList[i]);
            gridCornerManager.ClearHorizontal(fillHorizantalList[i]);
        }

        fillHorizantalList.Clear();
    }

    public bool IsPlacement() => isOk;

    public void Placement()
    {
        if (isOk)
        {
            gridCornerManager.Placement();
            gridVerticalManager.Placement();
            gridHorizontalManager.Placement();

            isOk = false;
            ComplateQuad();
        }
    }

  


    private void ComplateQuad()
    {
        for(int i = 0;i < gridHorizontalManager.GetHorizontallLines().Length - gridCreator.GetGridSize().x; i++) //width 
        {
            if (ControlQuad(gridHorizontalManager.GetHorizontallLines()[i].GetPoint()))
            {
                fillManager.OpenGrid(i);
            }
        }
        fillManager.ControlComplate();
    }

  
   

    private bool ControlQuad(Vector2Byte point)
    {
        bool resut = true;

        Vector2Byte p = point;
        resut = gridHorizontalManager.GetHorizontalLineGrid(p).GetFill();
        if (resut == false) return false;

        p = point + new Vector2Byte(0, 1);
        Debug.Log(p);
        resut = gridHorizontalManager.GetHorizontalLineGrid(p).GetFill();

        if(resut == false) return false;

        p = point;
        resut = gridVerticalManager.GetVerticalLineGrid(p).GetFill();
        if (resut == false) return false;

        p = point + new Vector2Byte(1, 0);
        resut = gridVerticalManager.GetVerticalLineGrid(p).GetFill();
        if (resut == false) return false;

        return resut;

    }
    public void HoverPuzzle(PuzzleData puzzleData, BaseGrid baseGrid)
    {
        isOk = false;
        UnHoverPuzzle();

        if (gridCornerManager.HoverPuzzleHorizontal(puzzleData, baseGrid) == false) return;
 
        if (gridCornerManager.HoverPuzzleVertical(puzzleData, baseGrid) == false) return;
       
        if (gridVerticalManager.HoverPuzzle(puzzleData, baseGrid) == false) return;

        if (gridHorizontalManager.HoverPuzzle(puzzleData, baseGrid) == false) return;

        gridCornerManager.Hover();
        gridVerticalManager.Hover();
        gridHorizontalManager.Hover();

        isOk = true;
    }
    public void UnHoverPuzzle()
    {
        isOk = false;
        gridCornerManager.UnHoverPuzzle();
        gridVerticalManager.UnHoverPuzzle();
        gridHorizontalManager.UnHoverPuzzle();
    }

    public bool ControlPlacement(PuzzleData puzzleData)
    {
        BaseGrid[] corners = gridCornerManager.GetAllCorners();

        for (int i = 0; i < corners.Length; i++)
        {
            bool v = gridVerticalManager.SuccessStatus(puzzleData, corners[i].GetPoint());
            bool h = gridHorizontalManager.SuccessStatus(puzzleData, corners[i].GetPoint());

            if (v == true && h == true)
            {
                return true;
            }
        }
        return false;
    }
   
}
