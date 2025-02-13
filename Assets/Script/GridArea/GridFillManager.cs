using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridFillManager : MonoBehaviour
{
    [SerializeField] GameObject[] fillSquareList;

    int width = 5;
    int height = 5;
    
    public void OpenGrid(int value)
    {
        fillSquareList[value].SetActive(true);
    }

    public Vector3 GetFillPos(int value) => fillSquareList[value].transform.position;

    public void ResetAll()
    {

        for (int i = 0; i < fillSquareList.Length; i++)
        {
            fillSquareList[i].SetActive(false);
        }
    }
    public void ClearVertical(int line)
    {
        for (int i = 0; i < height; i++)
        {
            fillSquareList[(line + (i * width))].GetComponent<FillSquare>().ClearGrid();
            ActionManager.ClearGrid?.Invoke((line + (i * width)));
        }
        AudioManager.Instance.PlaySound(AUDIO_TYPE.CLEAR_LINE);
    }
    public void ClearHorizontal(int line)
    {
        for (int i = 0; i < height; i++)
        {
            fillSquareList[i + (line * width)].GetComponent<FillSquare>().ClearGrid();
            ActionManager.ClearGrid?.Invoke(i + (line * width));
        }
        AudioManager.Instance.PlaySound(AUDIO_TYPE.CLEAR_LINE);
    }

    public void ControlComplate()
    {
        if (GetComplateCount() >= height || GetComplateCount() >= width)
        {
            ControlHorizontal();
            ControlVertical();
        }
        GridManager.Instance.ComplatePlacement();
    }

    private int GetComplateCount() => fillSquareList.Count(v => v.activeSelf);

    private void ControlVertical()
    {
        List<int> list = new List<int>();

        for (int i = 0; i < width; i++)
        {
            if(ControlVerticallLine(i))
                GridManager.Instance.AddFillVerticleList(i);
        }
    }

    private bool ControlVerticallLine(int line)
    {
        bool result = true;

        for (int i = 0; i < height; i++)
        {
            if (!fillSquareList[(line + (i * width))].activeSelf)
            {
                result = false;
                break;
            }
        }
        return result;
    }


    private void ControlHorizontal()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < height; i++)
        {
            if(ControlHorizontalLine(i))
                GridManager.Instance.AddFillHorzontalList(i);

        }
    }

    private bool ControlHorizontalLine(int line)
    {
        bool result = true;

        for (int i = 0; i < width; i++)
        {
            if (!fillSquareList[i + (line * width)].activeSelf)
            {
                result = false;
                break;
            }
        }
        return result;
    }

}
