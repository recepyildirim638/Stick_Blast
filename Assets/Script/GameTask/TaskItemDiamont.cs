using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskItemDiamont : TaskItem
{
   

    public override void ClearGridFunc(int val)
    {
        if(val == index) 
        {
            ActionManager.CollectDiamaont?.Invoke(transform.position);
            gameObject.SetActive(false);
        }
    }
}
