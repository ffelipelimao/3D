using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuButtonsManager : MonoBehaviour
{
    public List<GameObject> buttons;

    [Header("Animation")]
    public float animationDuration = 0.2f;
    public float animationDelay = 0.2f;
    public Ease ease = Ease.OutBack;

    void OnEnable()
    {
        HideAllButtons();
        ShowButtons();
    }

    private void HideAllButtons()
    {
        foreach (var b in buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }

    private void ShowButtons()
    {
        for(int i = 0; i < buttons.Count; i++ )
            {
                var b = buttons[i];
                b.SetActive(true);
                b.transform.DOScale(1,animationDuration).SetDelay(i*animationDelay).SetEase(ease);
            }
    }
}
