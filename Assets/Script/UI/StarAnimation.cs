using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    List<GameObject> moneyCollectList = new List<GameObject>();
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject itemEndPos;

    [SerializeField] LevelTaskManager levelTaskManager;

    Camera cam;

    private void OnEnable()
    {
        ActionManager.CollectStar += StartAnimation;
    }

    private void OnDisable()
    {
        ActionManager.CollectStar -= StartAnimation;
    }

    private void Start()
    {
        cam = Camera.main;
    }



    public void StartAnimation(Vector3 createPos)
    {
        GameObject obj = GetItem();
        obj.transform.position = cam.WorldToScreenPoint(createPos);
        Animation(obj);

    }
    private void Animation(GameObject obj)
    {

        obj.SetActive(true);
        obj.transform
            .DOMove(itemEndPos.transform.position, 1200f)
            .SetSpeedBased()
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                levelTaskManager.CollectStar();
                obj.gameObject.SetActive(false);
            });
    }

    GameObject GetItem()
    {
        for (int i = 0; i < moneyCollectList.Count; i++)
        {
            if (moneyCollectList[i].activeSelf == false)
            {
                return moneyCollectList[i];
            }
        }

        return CreateItem();

    }
    GameObject CreateItem()
    {
        GameObject obj = Instantiate(itemPrefab, transform);
        moneyCollectList.Add(obj);
        return obj;
    }
}
