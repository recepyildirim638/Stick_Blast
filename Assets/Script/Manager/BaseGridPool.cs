using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGridPool : MonoBehaviour
{
    [SerializeField] private PoolBaseGrid[] pools;

    public GameObject GetPoolItem(BASEGRID_TYPE items)
    {
        PoolBaseGrid pool = GetPoolCell(items);
        GameObject resultObject = null;

        for (int i = 0; i < pool.poolObjects.Count; i++)
        {
            if (pool.poolObjects[i].gameObject.activeSelf == false)
            {
                resultObject = pool.poolObjects[i].gameObject;
                resultObject.SetActive(true);
                return resultObject;
            }
        }

        resultObject = Instantiate(pool.poolObjectPrefab, transform);
        resultObject.SetActive(true);
        pool.poolObjects.Add(resultObject);
        return resultObject;
    }

    PoolBaseGrid GetPoolCell(BASEGRID_TYPE items)
    {
        if ((int)items > pools.Length)
            return pools[0];
        else
            return pools[(int)items];
    }

    


    [Serializable]
    struct PoolBaseGrid
    {
        public BASEGRID_TYPE type;
        public GameObject poolObjectPrefab;
        public List<GameObject> poolObjects;
    }
}


