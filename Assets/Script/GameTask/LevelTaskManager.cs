using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LevelTaskManager : MonoBehaviour
{
    [SerializeField] TaskUI  diamontTaskUI;
    [SerializeField] TaskUI starTaskUI;
    [SerializeField] GridFillManager gridFillManager;
    List<TaskItem> tasks = new List<TaskItem>();

    int diamontTask = 0;
    int starTask = 0;

    [Inject] DataManager dataManager;
    private void OnEnable()
    {
        ActionManager.GridAreaReady += GridAreaReadyFunc;

    }
    private void OnDisable()
    {
        ActionManager.GridAreaReady -= GridAreaReadyFunc;

    }

    private void GridAreaReadyFunc()
    {
        SetLevelTask();
    }


    public void CollectDiamont()
    {
        diamontTask--;

        if(diamontTask == 0)
        {
            diamontTaskUI.ComplateTask();
            ControlGameWin();
        }
        else
        {
            diamontTaskUI.SetTask(diamontTask);
           
        }
    }

    public void CollectStar()
    {
        starTask--;

        if (starTask == 0)
        {
            starTaskUI.ComplateTask();
            ControlGameWin();
        }
        else
        {
            starTaskUI.SetTask(starTask);
           
        }


    }

    private void ControlGameWin()
    {
        if(diamontTask == 0 && starTask == 0)
        {
            MainData mainData = dataManager.GetMainData();
            mainData.level++;
            ActionManager.GameEndWin?.Invoke();
        }
    }

    public void SetLevelTask()
    {
        for (int i = 0; i < tasks.Count; i++)
            tasks[i].gameObject.SetActive(false);

        tasks.Clear();

        diamontTask = Random.Range(1, 4);
        diamontTaskUI.SetTask(diamontTask);

        starTask = Random.Range(1, 4);
        starTaskUI.SetTask(starTask);

        StartCoroutine(CreateEnumerator());
    }


    IEnumerator CreateEnumerator()
    {
        List<int> addedList = new List<int>();
        Vector2Int gameSize = GridManager.Instance.GetGameSize();
        int size = gameSize.x * gameSize.y ;

        while (true)
        {
            if (addedList.Count == diamontTask)
                break;
            
            int rnd = Random.Range(0, size);

            if (!addedList.Contains(rnd))
            {
                GameObject diamont = PoolManager.Instance.GetPoolItem(POOL_TYPE.DIAMNONT);
                tasks.Add(diamont.GetComponent<TaskItem>());
                diamont.GetComponent<TaskItem>().SetIndex(rnd);
                diamont.transform.position = gridFillManager.GetFillPos(rnd).With(z: -1f);
                addedList.Add(rnd);
            }
            yield return null;
        }

        while (true)
        {
            if (addedList.Count == diamontTask + starTask)
                break;

            int rnd = Random.Range(0, size);

            if (!addedList.Contains(rnd))
            {
                GameObject star = PoolManager.Instance.GetPoolItem(POOL_TYPE.STAR);
                tasks.Add(star.GetComponent<TaskItem>());
                star.GetComponent<TaskItem>().SetIndex(rnd);
                star.transform.position = gridFillManager.GetFillPos(rnd).With(z: -1f);
                addedList.Add(rnd);
            }
            yield return null;
        }
   

    }
}
