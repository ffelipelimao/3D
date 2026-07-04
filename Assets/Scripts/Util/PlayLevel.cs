using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    void Awake()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.FileLoaded += OnLoad;
    }

    void Start()
    {
        // Cobre revisita ao menu: o SaveManager persiste e nao dispara o evento de novo.
        if (SaveManager.Instance != null)
            OnLoad(SaveManager.Instance.Setup);
    }

    public void OnLoad(SaveSetup setup)
    {
        uiTextName.text = "Play" + (setup.lastLevel + 1);
    }

    void OnDestroy()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.FileLoaded -= OnLoad;
    }
}
