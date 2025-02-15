using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridFillManager : MonoBehaviour
{
    GameObject[] fillSquareList;

    int width = 5;
    int height = 5;

   
    [SerializeField] Transform baseParent;

    BaseGridPool pool;

    private void Start()
    {
        pool = GetComponent<BaseGridPool>();
    }

    public void Create(Vector2Int size)
    {
        width = size.x;
        height = size.y;

        fillSquareList = new GameObject[(size.x) * (size.y)];
        int index = 0;

        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                GameObject obj = pool.GetPoolItem(BASEGRID_TYPE.FILL);
                obj.transform.parent = baseParent;
               
                obj.transform.localPosition = new Vector3(j * 6f, i * (-6f),1f);
                fillSquareList[index] = obj;
                index++;

            }
        }

        for (int i = 0;i < fillSquareList.Length; i++) 
        {
            fillSquareList[i].SetActive(false);
        }
    }

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
        for (int i = 0; i < width; i++)
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
