using TMPro;
using UnityEngine;

public class MoveCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;

    public void SetMoveIndex(int val) => levelText.text = "Move: " + (val ).ToString();
}
