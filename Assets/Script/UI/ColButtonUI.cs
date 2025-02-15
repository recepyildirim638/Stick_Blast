
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ColButtonUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI valueText;
    Button button;

    int val = 5;
    [SerializeField] int maxValue = 10;
    [SerializeField] int minValue = 4;

    [Inject] DataManager dataManager;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => ButtonOnClick());
    }

    public int GetVal() => val;

    private void OnEnable()
    {
        valueText.text = val.ToString();
    }

    private void ButtonOnClick()
    {
        val++;

        if (val > dataManager.GetMainData().maxGridSize)
            val = dataManager.GetMainData().minGridSize;

        valueText.text = val.ToString();
    }
}
