using System;
using UnityEngine;

public abstract class TaskItem : MonoBehaviour
{
    public int index;
    public void SetIndex(int index)
    {
        this.index = index;
    }

    private void OnEnable()
    {
        ActionManager.ClearGrid += ClearGridFunc;
    }

   

    private void OnDisable()
    {
        ActionManager.ClearGrid -= ClearGridFunc;
    }

    public abstract void ClearGridFunc(int val);

}