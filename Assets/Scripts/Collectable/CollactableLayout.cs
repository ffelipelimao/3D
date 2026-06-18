using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollactableLayout : MonoBehaviour
{
    private CollectableSetup _currSetup;
    public Image uiIcon;
    public TextMeshProUGUI uiValue;
    public void Load(CollectableSetup setup)
    {
        _currSetup = setup;
        UpdateUI();
    }

    void UpdateUI()
    {
        uiIcon.sprite = _currSetup.Icon;
    }

    void Update()
    {
        uiValue.text = _currSetup.coins.value.ToString();
    }
}
