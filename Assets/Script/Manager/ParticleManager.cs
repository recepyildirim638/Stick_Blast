using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager ins;

    public List<PoolPreticele> poolPreticelesList = new List<PoolPreticele>();

   

    private void Awake()
    {
        ins = this;
    }

    public void SetParticle(PoolItems items, Vector3 pos)
    {
        PoolPreticele poolPreticele = GetPoolParticle(items);
        bool result = false;
        for (int i = 0; i < poolPreticele.fxList.Count; i++)
        {
            if (poolPreticele.fxList[i].activeSelf == false)
            {
                result = true;
                poolPreticele.fxList[i].transform.position = pos;
                poolPreticele.fxList[i].SetActive(true);

                StartCoroutine(DelayVisiable(poolPreticele.fxList[i], poolPreticele.fxLifeTime));
                break;
            }
        }
        if (result == false)
        {
            GameObject temp = Instantiate(poolPreticele.fxPrefabs, transform);
            temp.transform.position = pos;
            poolPreticele.fxList.Add(temp);
            
            StartCoroutine(DelayVisiable(temp, 2f));
        }
    }

    IEnumerator DelayVisiable(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

    PoolPreticele GetPoolParticle(PoolItems items)
    {
        if ((int)items >= poolPreticelesList.Count)
            return poolPreticelesList[0];
        else
            return poolPreticelesList[(int)items];
    }

    
}
[System.Serializable]
public class PoolPreticele
{
    public PoolItems poolItems;
    public GameObject fxPrefabs;
    public List<GameObject> fxList = new List<GameObject>();
    public float fxLifeTime = 2f;
}
public enum PoolItems
{
    CORNER_PLACEMENT = 0,
    FILL_SQUARE_CLEAR = 1
   
}