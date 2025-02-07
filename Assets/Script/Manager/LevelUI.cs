using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    
    public void SetLevelIndex(int val) => levelText.text =  "Level: " + (val + 1).ToString();
}
