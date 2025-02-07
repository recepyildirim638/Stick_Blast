using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI taskCount;
    [SerializeField] GameObject complateImage;

    public void SetTask(int val)
    {
        taskCount.text = val.ToString();
        complateImage.SetActive(false);
    }

    public void ComplateTask()
    {
        taskCount.text = 0.ToString();
        complateImage.SetActive(true);
    }
    
}
