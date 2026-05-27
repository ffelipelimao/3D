using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    public Image uiImage;
    private Tween _currentTween;


    void OnValidate()
    {
        if (uiImage == null) uiImage = GetComponent<Image>();
    }

    public void UpdateValue(float f)
    {
        uiImage.fillAmount = f;
    }

    public void UpdateValue(float max, float current)
    {
        if (_currentTween != null) _currentTween.Kill();
        _currentTween = uiImage.DOFillAmount(1 - (current / max), 0.1f).SetEase(Ease.Linear);
    }
}
