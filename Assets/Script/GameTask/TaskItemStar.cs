using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class TaskItemStar : TaskItem
{
    
    public override void ClearGridFunc(int val)
    {
        if (val == index)
        {
            ActionManager.CollectStar?.Invoke(transform.position);
            gameObject.SetActive(false);
        }
    }
}
