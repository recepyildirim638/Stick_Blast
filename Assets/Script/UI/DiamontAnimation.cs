using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamontAnimation : MonoBehaviour
{
    List<GameObject> moneyCollectList = new List<GameObject>();
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject itemEndPos;

    [SerializeField] LevelTaskManager levelTaskManager;
    Vector3 endPosRect;
    Camera cam;

    private void OnEnable()
    {
        ActionManager.CollectDiamaont += StartAnimation;
    }

    private void OnDisable()
    {
        ActionManager.CollectDiamaont -= StartAnimation;
    }

    private void Start()
    {
        cam = Camera.main;
       // endPosRect = itemEndPos.gameObject.GetComponent<RectTransform>().position;
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
                levelTaskManager.CollectDiamont();
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
